using Microsoft.EntityFrameworkCore;
using AirlineSimulation.Application.DTOs;
using AirlineSimulation.Application.Interfaces;
using AirlineSimulation.Domain.Entities;
using AirlineSimulation.Domain.Enums;
using AirlineSimulation.Infrastructure.Data;

namespace AirlineSimulation.Infrastructure.Services;

public class ValidationService : IValidationService
{
    private readonly AirlineDbContext _context;

    public ValidationService(AirlineDbContext context)
    {
        _context = context;
    }

    public async Task<ValidationResponseDto> ValidateTicketAsync(ValidationRequestDto request)
    {
        var response = new ValidationResponseDto();
        var errors = new List<string>();

        // Get ticket with related data
        var ticket = await _context.Tickets
            .Include(t => t.Flight)
            .Include(t => t.Passenger)
            .Include(t => t.BaggageTags)
            .FirstOrDefaultAsync(t => t.TicketNumber == request.TicketNumber);

        // 1. Validate Ticket Existence
        if (ticket == null)
        {
            errors.Add("Ticket not found. Please verify the Ticket Number.");
            response.IsValid = false;
            response.Errors = errors;
            return response;
        }

        // 2. Validate Flight Number
        if (ticket.Flight.FlightNumber != request.FlightNumber)
        {
            errors.Add("The flight number provided does not match the flight on this ticket.");
            response.IsValid = false;
            response.Errors = errors;
            return response;
        }

        // 3. Validate Flight Date if provided
        if (!string.IsNullOrEmpty(request.FlightDate))
        {
            if (DateTime.TryParse(request.FlightDate, out DateTime parsedDate))
            {
                if (parsedDate.Date != ticket.Flight.DepartureTimeUtc.Date)
                {
                    errors.Add("The flight date provided does not match the flight date on this ticket.");
                    response.IsValid = false;
                    response.Errors = errors;
                    return response;
                }
            }
            else
            {
                errors.Add("INVALID_FLIGHT_DATE_FORMAT");
                response.IsValid = false;
                response.Errors = errors;
                return response;
            }
        }

        // 4. Validate Passport Number (Ownership)
        if (ticket.Passenger.PassportNumber != request.PassportNumber)
        {
            errors.Add("This ticket belongs to a passenger with a different passport number. Please check your Ticket Number.");
            response.IsValid = false;
            response.Errors = errors;
            return response;
        }

        // 5. Check Passport Expiry
        if (ticket.Passenger.PassportExpiryDate < DateTime.UtcNow)
        {
            errors.Add("The passenger's passport has expired.");
            response.IsValid = false;
            response.Errors = errors;
            return response;
        }

        response.IsValid = true;
        response.Errors = errors;

        if (response.IsValid)
        {
            response.Passenger = MapPassengerToDto(ticket.Passenger);
            response.Flight = MapFlightToDto(ticket.Flight);
            response.Flight.AirlineIataCode = null; // Clear from public validate-ticket response
            response.Flight.DelayMinutes = null; // Clear from public validate-ticket response
            
            // Map ticket and populate its nested objects
            var ticketDto = MapTicketToDto(ticket);
            ticketDto.Flight = response.Flight;
            ticketDto.Passenger = response.Passenger;
            ticketDto.FlightDate = ticket.Flight.DepartureTimeUtc.ToString("dd MMM yyyy").ToUpper();
            ticketDto.FlightDuration = CalculateFlightDuration(ticket.Flight.DepartureTimeUtc, ticket.Flight.ArrivalTimeUtc);
            response.Ticket = ticketDto;
            
            // Add computed fields for complete boarding pass
            response.FlightDate = ticketDto.FlightDate;
            response.FlightDuration = ticketDto.FlightDuration;
            
            // Fallback for Boarding Time: Ticket > Scheduled > (Departure - 45m)
            response.BoardingTimeUtc = ticket.BoardingTimeUtc 
                                    ?? ticket.Flight.ScheduledBoardingTimeUtc 
                                    ?? ticket.Flight.DepartureTimeUtc.AddMinutes(-45);
                                    
            response.Terminal = ticket.Terminal ?? ticket.Flight.Terminal;
            response.Gate = ticket.Gate ?? ticket.Flight.Gate;

            // Populate same computed fallbacks into the nested ticket object so they aren't null
            ticketDto.BoardingTimeUtc = response.BoardingTimeUtc;
            ticketDto.Terminal = response.Terminal;
            ticketDto.Gate = response.Gate;
            ticketDto.BarcodeData = ticket.BarcodeData ?? $"BARCODE_{ticket.TicketNumber}_{ticket.Flight.FlightNumber}";
        }

        return response;
    }

    private string CalculateFlightDuration(DateTime departure, DateTime arrival)
    {
        var duration = arrival - departure;
        return $"{(int)duration.TotalHours}h {duration.Minutes}m";
    }

    private PassengerDto MapPassengerToDto(Passenger passenger) => new()
    {
        PassengerId = passenger.PassengerId,
        FirstName = passenger.FirstName,
        LastName = passenger.LastName,
        PassportNumber = passenger.PassportNumber,
        Nationality = passenger.Nationality,
        DateOfBirth = passenger.DateOfBirth,
        PassportExpiryDate = passenger.PassportExpiryDate,
        Gender = passenger.Gender
    };

    private FlightDto MapFlightToDto(Flight flight)
    {
        // Helper to infer city if missing (for legacy data)
        string InferCity(string code) => code switch
        {
            "CAI" => "Cairo",
            "DXB" => "Dubai",
            "JFK" => "New York",
            "LHR" => "London",
            "CDG" => "Paris",
            "HBE" => "Alexandria",
            "ASW" => "Aswan",
            "LXR" => "Luxor",
            "SSH" => "Sharm El Sheikh",
            "HRG" => "Hurghada",
            "RUH" => "Riyadh",
            "JED" => "Jeddah",
            "KWI" => "Kuwait",
            "DOH" => "Doha",
            _ => ""
        };

        return new FlightDto
        {
            FlightId = flight.FlightId,
            FlightNumber = flight.FlightNumber,
            DepartureAirport = flight.DepartureAirport,
            ArrivalAirport = flight.ArrivalAirport,
            DepartureTimeUtc = flight.DepartureTimeUtc,
            ArrivalTimeUtc = flight.ArrivalTimeUtc,
            Terminal = flight.Terminal,
            Gate = flight.Gate,
            OriginCity = string.IsNullOrEmpty(flight.OriginCity) ? InferCity(flight.DepartureAirport) : flight.OriginCity,
            DestinationCity = string.IsNullOrEmpty(flight.DestinationCity) ? InferCity(flight.ArrivalAirport) : flight.DestinationCity,
            ScheduledBoardingTimeUtc = flight.ScheduledBoardingTimeUtc,
            FlightStatus = flight.FlightStatus.ToString(),
            DelayMinutes = flight.DelayMinutes,
            AirlineName = flight.AirlineName,
            AirlineIcaoCode = flight.AirlineIcaoCode,
            AirlineIataCode = flight.AirlineIataCode,
            DepartureIataCode = flight.DepartureIataCode,
            ArrivalIataCode = flight.ArrivalIataCode
        };
    }

    private TicketDto MapTicketToDto(Ticket ticket) => new()
    {
        TicketId = ticket.TicketId,
        TicketNumber = ticket.TicketNumber,
        FlightId = ticket.FlightId,
        PassengerId = ticket.PassengerId,
        SeatNumber = ticket.SeatNumber,
        TravelClass = ticket.TravelClass.ToString(),
        BoardingTimeUtc = ticket.BoardingTimeUtc,
        Terminal = ticket.Terminal,
        Gate = ticket.Gate,
        BarcodeData = ticket.BarcodeData,
        BoardingStatus = ticket.BoardingStatus.ToString(),
        AllowedBaggageCount = ticket.AllowedBaggageCount,
        MaxAllowedWeight = ticket.MaxAllowedWeight,
        BaggageCount = ticket.BaggageTags?.Count ?? 0,
        TotalBaggageWeight = ticket.BaggageTags?.Sum(b => b.WeightKg ?? 0) ?? 0m
    };
}

public class BaggageTagService : IBaggageTagService
{
    private readonly AirlineDbContext _context;

    public BaggageTagService(AirlineDbContext context)
    {
        _context = context;
    }

    public async Task<List<BaggageTagDto>> GenerateBaggageTagsAsync(string ticketNumber, int count)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);
        if (ticket == null) throw new Exception("Ticket not found");

        var tags = new List<BaggageTag>();
        var random = new Random();

        for (int i = 0; i < count; i++)
        {
            var tagNumber = $"BAG{DateTime.UtcNow.Ticks}{i}";
            tags.Add(new BaggageTag
            {
                TagNumber = tagNumber,
                TicketId = ticket.TicketId,
                WeightKg = 15 + (decimal)(random.NextDouble() * 15)
            });
        }

        _context.BaggageTags.AddRange(tags);
        await _context.SaveChangesAsync();

        return tags.Select(t => new BaggageTagDto
        {
            BaggageTagId = t.BaggageTagId,
            TagNumber = t.TagNumber,
            TicketId = t.TicketId,
            WeightKg = t.WeightKg,
            CreatedAt = t.CreatedAt
        }).ToList();
    }

    public async Task<List<BaggageTagDto>> GetBaggageTagsByTicketAsync(int ticketId)
    {
        var tags = await _context.BaggageTags
            .Where(b => b.TicketId == ticketId)
            .ToListAsync();

        return tags.Select(t => new BaggageTagDto
        {
            BaggageTagId = t.BaggageTagId,
            TagNumber = t.TagNumber,
            TicketId = t.TicketId,
            WeightKg = t.WeightKg,
            CreatedAt = t.CreatedAt
        }).ToList();
    }

    public async Task<BaggageTagDto?> GetBaggageTagByNumberAsync(string tagNumber)
    {
        var tag = await _context.BaggageTags
            .Include(b => b.Ticket)
            .ThenInclude(t => t.Flight)
            .Include(b => b.Ticket)
            .ThenInclude(t => t.Passenger)
            .FirstOrDefaultAsync(b => b.TagNumber == tagNumber);

        if (tag == null) return null;

        return new BaggageTagDto
        {
            BaggageTagId = tag.BaggageTagId,
            TagNumber = tag.TagNumber,
            TicketId = tag.TicketId,
            WeightKg = tag.WeightKg,
            CreatedAt = tag.CreatedAt,
            
            // Map enriched details
            FlightNumber = tag.Ticket.Flight.FlightNumber,
            Origin = tag.Ticket.Flight.DepartureAirport,
            Destination = tag.Ticket.Flight.ArrivalAirport,
            DepartureTime = tag.Ticket.Flight.DepartureTimeUtc,
            ArrivalTime = tag.Ticket.Flight.ArrivalTimeUtc,
            Terminal = tag.Ticket.Flight.Terminal,
            Gate = tag.Ticket.Flight.Gate,
            BoardingTime = tag.Ticket.BoardingTimeUtc ?? tag.Ticket.Flight.ScheduledBoardingTimeUtc,
            PassengerName = $"{tag.Ticket.Passenger.FirstName} {tag.Ticket.Passenger.LastName}",
        };
    }

    public async Task<VerifyBaggageResponseDto?> VerifyBaggageTagAsync(string tagNumber)
    {
        var tag = await _context.BaggageTags
            .Include(b => b.Ticket)
            .ThenInclude(t => t.Flight)
            .Include(b => b.Ticket)
            .ThenInclude(t => t.Passenger)
            .FirstOrDefaultAsync(b => b.TagNumber == tagNumber);

        if (tag == null) return null;

        return new VerifyBaggageResponseDto
        {
            Valid = true,
            Passport = tag.Ticket.Passenger.PassportNumber,
            Tag = new VerifyBaggageTagDto
            {
                TagNumber = tag.TagNumber,
                WeightKg = tag.WeightKg,
                Destination = tag.Ticket.Flight.ArrivalAirport,
                FlightNumber = tag.Ticket.Flight.FlightNumber,
                Origin = tag.Ticket.Flight.DepartureAirport,
                Gate = tag.Ticket.Flight.Gate,
                Terminal = tag.Ticket.Flight.Terminal,
                PassengerName = $"{tag.Ticket.Passenger.FirstName} {tag.Ticket.Passenger.LastName}",
                DepartureTime = tag.Ticket.Flight.DepartureTimeUtc,
                BoardingTime = tag.Ticket.BoardingTimeUtc ?? tag.Ticket.Flight.ScheduledBoardingTimeUtc
            }
        };
    }

    public async Task<BaggageInfoResponseDto?> GetBaggageInfoAsync(string? ticketNumber, string? passportNumber)
    {
        IQueryable<Ticket> query = _context.Tickets
            .Include(t => t.Passenger)
            .Include(t => t.Flight)
            .Include(t => t.BaggageTags);

        if (!string.IsNullOrEmpty(ticketNumber))
        {
            query = query.Where(t => t.TicketNumber == ticketNumber);
        }
        else if (!string.IsNullOrEmpty(passportNumber))
        {
            query = query.Where(t => t.Passenger.PassportNumber == passportNumber);
        }
        else
        {
            return null;
        }

        var tickets = await query.ToListAsync();
        if (!tickets.Any()) return null;

        var passenger = tickets.First().Passenger;

        return new BaggageInfoResponseDto
        {
            PassengerName = $"{passenger.FirstName} {passenger.LastName}",
            PassportNumber = passenger.PassportNumber,
            Tickets = tickets.Select(t => new TicketBaggageDto
            {
                TicketNumber = t.TicketNumber,
                FlightNumber = t.Flight.FlightNumber,
                DepartureAirport = t.Flight.DepartureAirport,
                ArrivalAirport = t.Flight.ArrivalAirport,
                BaggageTags = t.BaggageTags.Select(b => new BaggageTagDto
                {
                    BaggageTagId = b.BaggageTagId,
                    TagNumber = b.TagNumber,
                    TicketId = b.TicketId,
                    WeightKg = b.WeightKg,
                    CreatedAt = b.CreatedAt,
                    CurrentLocation = b.CurrentLocation,
                    LastLocationUpdatedAt = b.LastLocationUpdatedAt,
                    FlightNumber = t.Flight.FlightNumber,
                    Origin = t.Flight.DepartureAirport,
                    Destination = t.Flight.ArrivalAirport,
                    PassengerName = $"{passenger.FirstName} {passenger.LastName}",
                    SeatNumber = t.SeatNumber
                }).ToList()
            }).ToList(),
            TotalBaggageCount = tickets.Sum(t => t.BaggageTags.Count)
        };
    }

    // Valid locations for baggage tracking
    private static readonly HashSet<string> ValidLocations = new(StringComparer.OrdinalIgnoreCase)
    {
        "Security Check", "Terminal", "Gate", "Loaded on Aircraft",
        "Arrived at Dest", "Customs", "Baggage Belt"
    };

    public async Task<BaggageLocationResponseDto?> UpdateBaggageLocationAsync(string tagNumber, UpdateBaggageLocationDto dto)
    {
        if (!ValidLocations.Contains(dto.Location))
            throw new ArgumentException($"Invalid location: '{dto.Location}'");

        var tag = await _context.BaggageTags.FirstOrDefaultAsync(b => b.TagNumber == tagNumber);
        if (tag == null) return null;

        var previousLocation = tag.CurrentLocation;
        tag.PreviousLocation = previousLocation;
        tag.CurrentLocation = dto.Location;
        tag.LastLocationUpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new BaggageLocationResponseDto
        {
            TagNumber = tag.TagNumber,
            PreviousLocation = previousLocation,
            CurrentLocation = tag.CurrentLocation,
            UpdatedAt = tag.LastLocationUpdatedAt.Value
        };
    }

    public async Task<BaggageLocationResponseDto?> GetBaggageLastLocationAsync(string tagNumber)
    {
        var tag = await _context.BaggageTags.FirstOrDefaultAsync(b => b.TagNumber == tagNumber);
        if (tag == null) return null;

        return new BaggageLocationResponseDto
        {
            TagNumber = tag.TagNumber,
            PreviousLocation = tag.PreviousLocation,
            CurrentLocation = tag.CurrentLocation ?? "Unknown",
            UpdatedAt = tag.LastLocationUpdatedAt ?? tag.CreatedAt
        };
    }

    public async Task<BaggagesByTicketResponseDto?> GetBaggageTagsByTicketNumberAsync(string ticketNumber)
    {
        var ticket = await _context.Tickets
            .Include(t => t.Flight)
            .Include(t => t.Passenger)
            .Include(t => t.BaggageTags)
            .FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);

        if (ticket == null) return null;

        return new BaggagesByTicketResponseDto
        {
            TicketNumber = ticket.TicketNumber,
            FlightNumber = ticket.Flight.FlightNumber,
            PassengerName = $"{ticket.Passenger.FirstName} {ticket.Passenger.LastName}",
            PassportNumber = ticket.Passenger.PassportNumber,
            Bags = ticket.BaggageTags.Select(b => new BaggageTagDto
            {
                BaggageTagId = b.BaggageTagId,
                TagNumber = b.TagNumber,
                TicketId = b.TicketId,
                WeightKg = b.WeightKg,
                CreatedAt = b.CreatedAt,
                CurrentLocation = b.CurrentLocation,
                LastLocationUpdatedAt = b.LastLocationUpdatedAt,
                FlightNumber = ticket.Flight.FlightNumber,
                Origin = ticket.Flight.DepartureAirport,
                Destination = ticket.Flight.ArrivalAirport,
                PassengerName = $"{ticket.Passenger.FirstName} {ticket.Passenger.LastName}",
                SeatNumber = ticket.SeatNumber
            }).ToList()
        };
    }

    public async Task<List<BaggageLocationResponseDto>?> UpdateMultipleBaggageLocationsAsync(string ticketNumber, UpdateBaggageLocationDto dto)
    {
        if (!ValidLocations.Contains(dto.Location))
            throw new ArgumentException($"Invalid location: '{dto.Location}'");

        var ticket = await _context.Tickets
            .Include(t => t.BaggageTags)
            .FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);

        if (ticket == null) return null;

        var results = new List<BaggageLocationResponseDto>();
        var now = DateTime.UtcNow;

        foreach (var tag in ticket.BaggageTags)
        {
            var previousLocation = tag.CurrentLocation;
            tag.CurrentLocation = dto.Location;
            tag.LastLocationUpdatedAt = now;

            results.Add(new BaggageLocationResponseDto
            {
                TagNumber = tag.TagNumber,
                PreviousLocation = previousLocation,
                CurrentLocation = tag.CurrentLocation,
                UpdatedAt = now
            });
        }

        await _context.SaveChangesAsync();
        return results;
    }

    public async Task<BaggageTagDto?> UpdateBaggageWeightAsync(string tagNumber, decimal weightKg)
    {
        var tag = await _context.BaggageTags.FirstOrDefaultAsync(b => b.TagNumber == tagNumber);
        if (tag == null) return null;

        tag.WeightKg = weightKg;
        await _context.SaveChangesAsync();

        return new BaggageTagDto
        {
            BaggageTagId = tag.BaggageTagId,
            TagNumber = tag.TagNumber,
            TicketId = tag.TicketId,
            WeightKg = tag.WeightKg,
            CreatedAt = tag.CreatedAt
        };
    }

    public async Task<bool> DeleteBaggageTagAsync(string tagNumber)
    {
        var tag = await _context.BaggageTags.FirstOrDefaultAsync(b => b.TagNumber == tagNumber);
        if (tag == null) return false;

        _context.BaggageTags.Remove(tag);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAllBaggageByTicketAsync(string ticketNumber)
    {
        var ticket = await _context.Tickets
            .Include(t => t.BaggageTags)
            .FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);
            
        if (ticket == null || !ticket.BaggageTags.Any()) return false;

        _context.BaggageTags.RemoveRange(ticket.BaggageTags);
        await _context.SaveChangesAsync();
        return true;
    }
}

public class TicketService : ITicketService
{
    private readonly AirlineDbContext _context;

    public TicketService(AirlineDbContext context)
    {
        _context = context;
    }

    public async Task<List<TicketDto>> GetAllTicketsAsync()
    {
        var tickets = await _context.Tickets
            .Include(t => t.Flight)
            .Include(t => t.Passenger)
            .Include(t => t.BaggageTags)
            .ToListAsync();

        return tickets.Select(MapToDto).ToList();
    }

    public async Task<TicketDto?> GetTicketByIdAsync(int id)
    {
        var ticket = await _context.Tickets
            .Include(t => t.Flight)
            .Include(t => t.Passenger)
            .Include(t => t.BaggageTags)
            .FirstOrDefaultAsync(t => t.TicketId == id);

        return ticket == null ? null : MapToDto(ticket);
    }

    public async Task<TicketDto?> GetTicketByNumberAsync(string ticketNumber)
    {
        var ticket = await _context.Tickets
            .Include(t => t.Flight)
            .Include(t => t.Passenger)
            .Include(t => t.BaggageTags)
            .FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);

        return ticket == null ? null : MapToDto(ticket);
    }

    public async Task<TicketDto> CreateTicketAsync(CreateTicketDto dto)
    {
        // Auto-generate ticket number if not provided
        var ticketNumber = string.IsNullOrEmpty(dto.TicketNumber) 
            ? $"TKT{DateTime.UtcNow.Ticks}" 
            : dto.TicketNumber;

        var ticket = new Ticket
        {
            TicketNumber = ticketNumber,
            FlightId = dto.FlightId,
            PassengerId = dto.PassengerId,
            SeatNumber = dto.SeatNumber,
            TravelClass = Enum.Parse<TravelClass>(dto.TravelClass),
            AllowedBaggageCount = dto.AllowedBaggageCount,
            MaxAllowedWeight = dto.MaxAllowedWeight
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return (await GetTicketByIdAsync(ticket.TicketId))!;
    }

    public async Task<TicketDto?> UpdateTicketAsync(int id, UpdateTicketDto dto)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket == null) return null;

        if (dto.SeatNumber != null) ticket.SeatNumber = dto.SeatNumber;
        if (dto.TravelClass != null) ticket.TravelClass = Enum.Parse<TravelClass>(dto.TravelClass);
        if (dto.BoardingStatus != null) ticket.BoardingStatus = Enum.Parse<BoardingStatus>(dto.BoardingStatus);
        if (dto.AllowedBaggageCount.HasValue) ticket.AllowedBaggageCount = dto.AllowedBaggageCount.Value;
        if (dto.MaxAllowedWeight.HasValue) ticket.MaxAllowedWeight = dto.MaxAllowedWeight.Value;

        await _context.SaveChangesAsync();
        return await GetTicketByIdAsync(id);
    }

    public async Task<bool> DeleteTicketAsync(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket == null) return false;

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<TicketDto?> GenerateBoardingPassAsync(string ticketNumber)
    {
        var ticket = await _context.Tickets
            .Include(t => t.Flight)
            .FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);

        if (ticket == null) return null;

        ticket.BoardingTimeUtc = ticket.Flight.ScheduledBoardingTimeUtc ?? ticket.Flight.DepartureTimeUtc.AddMinutes(-45);
        ticket.Terminal = ticket.Flight.Terminal;
        ticket.Gate = ticket.Flight.Gate;
        ticket.BarcodeData = $"BARCODE_{ticket.TicketNumber}_{ticket.Flight.FlightNumber}_{DateTime.UtcNow.Ticks}";
        ticket.BoardingStatus = BoardingStatus.NotBoarded;

        await _context.SaveChangesAsync();

        return await GetTicketByNumberAsync(ticketNumber);
    }

    public async Task<BoardingPassResponseDto?> IssueBoardingPassAsync(string ticketNumber)
    {
        var ticket = await _context.Tickets
            .Include(t => t.Flight)
            .Include(t => t.Passenger)
            .FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);

        if (ticket == null) return null;

        // Generate boarding pass data if not already generated
        if (ticket.BarcodeData == null)
        {
            ticket.BoardingTimeUtc = ticket.Flight.ScheduledBoardingTimeUtc ?? ticket.Flight.DepartureTimeUtc.AddMinutes(-45);
            ticket.Terminal = ticket.Flight.Terminal;
            ticket.Gate = ticket.Flight.Gate;
            ticket.BarcodeData = $"{ticket.Flight.AirlineIcaoCode}{ticket.Flight.DepartureAirport} {ticket.Flight.ArrivalAirport}{ticket.SeatNumber}{ticket.Flight.DepartureTimeUtc:ddMMyy}";
            ticket.BoardingStatus = BoardingStatus.NotBoarded;
            await _context.SaveChangesAsync();
        }

        var flight = ticket.Flight;
        var duration = flight.ArrivalTimeUtc - flight.DepartureTimeUtc;
        if (duration.TotalHours < 0) duration = duration.Add(TimeSpan.FromDays(1));

        var boardingPass = new BoardingPassDto
        {
            AirlineName = flight.AirlineName.ToUpper(),
            AirlineIataCode = flight.AirlineIataCode,
            FlightNumber = flight.FlightNumber,
            From = flight.DepartureAirport,
            FromCity = flight.OriginCity,
            To = flight.ArrivalAirport,
            ToCity = flight.DestinationCity,
            Duration = $"{(int)duration.TotalHours}h {duration.Minutes:D2}m",
            DepartureTime = flight.DepartureTimeUtc.ToString("h:mmtt").ToUpper(),
            ArrivalTime = flight.ArrivalTimeUtc.ToString("h:mmtt").ToUpper(),
            PassengerName = $"{ticket.Passenger.FirstName} {ticket.Passenger.LastName}",
            SeatNumber = ticket.SeatNumber,
            Terminal = ticket.Terminal ?? flight.Terminal ?? "-",
            Gate = ticket.Gate ?? flight.Gate ?? "-",
            Class = ticket.TravelClass.ToString(),
            BoardingTime = (ticket.BoardingTimeUtc ?? flight.DepartureTimeUtc.AddMinutes(-45)).ToString("h:mmtt").ToUpper(),
            FlightDate = flight.DepartureTimeUtc.ToString("dd MMM yyyy").ToUpper(),
            BarcodeData = ticket.BarcodeData ?? ""
        };

        return new BoardingPassResponseDto
        {
            BoardingPasses = new List<BoardingPassDto> { boardingPass }
        };
    }

    public async Task<TicketBaggageAllowanceDto?> GetTicketBaggageAllowanceAsync(string ticketNumber)
    {
        var ticket = await _context.Tickets
            .Select(t => new { t.TicketNumber, t.AllowedBaggageCount, t.MaxAllowedWeight })
            .FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);

        if (ticket == null) return null;

        return new TicketBaggageAllowanceDto
        {
            TicketNumber = ticket.TicketNumber,
            AllowedBaggageCount = ticket.AllowedBaggageCount,
            MaxAllowedWeight = ticket.MaxAllowedWeight
        };
    }

    private TicketDto MapToDto(Ticket ticket) => new()
    {
        TicketId = ticket.TicketId,
        TicketNumber = ticket.TicketNumber,
        FlightId = ticket.FlightId,
        PassengerId = ticket.PassengerId,
        SeatNumber = ticket.SeatNumber,
        TravelClass = ticket.TravelClass.ToString(),
        BoardingTimeUtc = ticket.BoardingTimeUtc,
        Terminal = ticket.Terminal,
        Gate = ticket.Gate,
        BarcodeData = ticket.BarcodeData,
        BoardingStatus = ticket.BoardingStatus.ToString(),
        AllowedBaggageCount = ticket.AllowedBaggageCount,
        MaxAllowedWeight = ticket.MaxAllowedWeight,
        BaggageCount = ticket.BaggageTags?.Count ?? 0,
        TotalBaggageWeight = ticket.BaggageTags?.Sum(b => b.WeightKg ?? 0) ?? 0m,
        FlightDate = ticket.Flight != null ? ticket.Flight.DepartureTimeUtc.ToString("dd MMM yyyy").ToUpper() : null,
        FlightDuration = ticket.Flight != null ? CalculateFlightDuration(ticket.Flight.DepartureTimeUtc, ticket.Flight.ArrivalTimeUtc) : null,
        Flight = ticket.Flight != null ? new FlightDto
        {
            FlightId = ticket.Flight.FlightId,
            FlightNumber = ticket.Flight.FlightNumber,
            DepartureAirport = ticket.Flight.DepartureAirport,
            ArrivalAirport = ticket.Flight.ArrivalAirport,
            DepartureTimeUtc = ticket.Flight.DepartureTimeUtc,
            ArrivalTimeUtc = ticket.Flight.ArrivalTimeUtc,
            Terminal = ticket.Flight.Terminal,
            Gate = ticket.Flight.Gate,
            OriginCity = ticket.Flight.OriginCity,
            DestinationCity = ticket.Flight.DestinationCity,
            ScheduledBoardingTimeUtc = ticket.Flight.ScheduledBoardingTimeUtc,
            FlightStatus = ticket.Flight.FlightStatus.ToString(),
            DelayMinutes = ticket.Flight.DelayMinutes,
            AirlineName = ticket.Flight.AirlineName,
            AirlineIcaoCode = ticket.Flight.AirlineIcaoCode,
            AirlineIataCode = ticket.Flight.AirlineIataCode,
            DepartureIataCode = ticket.Flight.DepartureIataCode,
            ArrivalIataCode = ticket.Flight.ArrivalIataCode
        } : null,
        Passenger = ticket.Passenger != null ? new PassengerDto
        {
            PassengerId = ticket.Passenger.PassengerId,
            FirstName = ticket.Passenger.FirstName,
            LastName = ticket.Passenger.LastName,
            PassportNumber = ticket.Passenger.PassportNumber,
            Nationality = ticket.Passenger.Nationality,
            DateOfBirth = ticket.Passenger.DateOfBirth,
            PassportExpiryDate = ticket.Passenger.PassportExpiryDate,
            Gender = ticket.Passenger.Gender
        } : null
    };
    
    private string CalculateFlightDuration(DateTime departure, DateTime arrival)
    {
        var duration = arrival - departure;
        var hours = (int)duration.TotalHours;
        var minutes = duration.Minutes;
        return $"{hours}h {minutes}m";
    }
}
