using Microsoft.EntityFrameworkCore;
using AirlineSimulation.Application.DTOs;
using AirlineSimulation.Application.Interfaces;
using AirlineSimulation.Domain.Entities;
using AirlineSimulation.Domain.Enums;
using AirlineSimulation.Infrastructure.Data;

namespace AirlineSimulation.Infrastructure.Services;

public class FlightService : IFlightService
{
    private readonly AirlineDbContext _context;

    public FlightService(AirlineDbContext context)
    {
        _context = context;
    }

    public async Task<List<FlightDto>> GetAllFlightsAsync()
    {
        var flights = await _context.Flights.ToListAsync();
        return flights.Select(MapToDto).ToList();
    }

    public async Task<FlightDto?> GetFlightByIdAsync(int id)
    {
        var flight = await _context.Flights.FindAsync(id);
        return flight == null ? null : MapToDto(flight);
    }

    public async Task<FlightDto?> GetFlightByNumberAsync(string flightNumber)
    {
        var flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightNumber == flightNumber);
        return flight == null ? null : MapToDto(flight);
    }

    public async Task<FlightDto> CreateFlightAsync(CreateFlightDto dto)
    {
        var flight = new Flight
        {
            FlightNumber = dto.FlightNumber,
            DepartureAirport = dto.DepartureAirport,
            ArrivalAirport = dto.ArrivalAirport,
            DepartureTimeUtc = dto.DepartureTimeUtc,
            ArrivalTimeUtc = dto.ArrivalTimeUtc,
            Terminal = dto.Terminal,
            Gate = dto.Gate,
            OriginCity = dto.OriginCity,
            DestinationCity = dto.DestinationCity,
            ScheduledBoardingTimeUtc = dto.ScheduledBoardingTimeUtc,
            FlightStatus = FlightStatus.Scheduled,
            AirlineName = dto.AirlineName,
            AirlineIcaoCode = dto.AirlineIcaoCode,
            AirlineIataCode = dto.AirlineIataCode,
            DepartureIataCode = dto.DepartureIataCode,
            ArrivalIataCode = dto.ArrivalIataCode
        };

        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();

        return MapToDto(flight);
    }

    public async Task<FlightDto?> UpdateFlightAsync(int id, UpdateFlightDto dto)
    {
        var flight = await _context.Flights.FindAsync(id);
        if (flight == null) return null;

        if (dto.FlightNumber != null) flight.FlightNumber = dto.FlightNumber;
        if (dto.DepartureAirport != null) flight.DepartureAirport = dto.DepartureAirport;
        if (dto.ArrivalAirport != null) flight.ArrivalAirport = dto.ArrivalAirport;
        if (dto.DepartureTimeUtc.HasValue) flight.DepartureTimeUtc = dto.DepartureTimeUtc.Value;
        if (dto.ArrivalTimeUtc.HasValue) flight.ArrivalTimeUtc = dto.ArrivalTimeUtc.Value;
        if (dto.AirlineName != null) flight.AirlineName = dto.AirlineName;
        if (dto.AirlineIcaoCode != null) flight.AirlineIcaoCode = dto.AirlineIcaoCode;
        if (dto.AirlineIataCode != null) flight.AirlineIataCode = dto.AirlineIataCode;
        if (dto.DepartureIataCode != null) flight.DepartureIataCode = dto.DepartureIataCode;
        if (dto.ArrivalIataCode != null) flight.ArrivalIataCode = dto.ArrivalIataCode;
        if (dto.Terminal != null) flight.Terminal = dto.Terminal;
        if (dto.Gate != null) flight.Gate = dto.Gate;
        if (dto.OriginCity != null) flight.OriginCity = dto.OriginCity;
        if (dto.DestinationCity != null) flight.DestinationCity = dto.DestinationCity;
        if (dto.ScheduledBoardingTimeUtc.HasValue) flight.ScheduledBoardingTimeUtc = dto.ScheduledBoardingTimeUtc;
        if (dto.FlightStatus != null) flight.FlightStatus = Enum.Parse<FlightStatus>(dto.FlightStatus);
        if (dto.DelayMinutes.HasValue) flight.DelayMinutes = dto.DelayMinutes.Value;
        
        flight.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return MapToDto(flight);
    }

    public async Task<bool> DeleteFlightAsync(int id)
    {
        var flight = await _context.Flights.FindAsync(id);
        if (flight == null) return false;

        _context.Flights.Remove(flight);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<FlightDto?> UpdateFlightStatusAsync(int id, string newStatus)
    {
        var flight = await _context.Flights.FindAsync(id);
        if (flight == null) return null;

        if (Enum.TryParse<FlightStatus>(newStatus, out var status))
        {
            flight.FlightStatus = status;
            await _context.SaveChangesAsync();
            return MapToDto(flight);
        }

        return null;
    }

    public async Task<DelayPredictionFeaturesResponseDto> GetDelayPredictionFeaturesAsync(
        string flightNumber, 
        string departureIataCode, 
        DateTime scheduledDepartureUtc)
    {
        // 1. حساب بداية ونهاية الساعة المحددة للرحلة
        var hourStart = new DateTime(scheduledDepartureUtc.Year, scheduledDepartureUtc.Month, scheduledDepartureUtc.Day, scheduledDepartureUtc.Hour, 0, 0, DateTimeKind.Utc);
        var hourEnd = hourStart.AddHours(1);

        // 2. حساب مجموع رحلات الإقلاع (Departing) ومجموع رحلات الهبوط (Arriving) معاً لنفس المطار وفي نفس الساعة
        var departingCount = await _context.Flights
            .Where(f => f.DepartureIataCode == departureIataCode 
                     && f.DepartureTimeUtc >= hourStart 
                     && f.DepartureTimeUtc < hourEnd)
            .CountAsync();

        var arrivingCount = await _context.Flights
            .Where(f => f.ArrivalIataCode == departureIataCode 
                     && f.ArrivalTimeUtc >= hourStart 
                     && f.ArrivalTimeUtc < hourEnd)
            .CountAsync();

        var totalTraffic = departingCount + arrivingCount;

        // 3. تحديد يوم الأسبوع (0 = الأحد) والساعة للبحث التاريخي
        int dayOfWeek = (int)scheduledDepartureUtc.DayOfWeek;
        int hour = scheduledDepartureUtc.Hour;

        // 4. جلب الزحام التاريخي للمطار (OriginHistAvgCongestion)
        var histCongestion = await _context.AirportCongestions
            .Where(c => c.AirportIataCode == departureIataCode 
                     && c.DayOfWeek == dayOfWeek 
                     && c.HourOfDay == hour)
            .Select(c => (double)c.AverageCongestion)
            .FirstOrDefaultAsync();

        return new DelayPredictionFeaturesResponseDto
        {
            FlightNumber = flightNumber,
            DepartureIataCode = departureIataCode,
            ScheduledDepartureUtc = scheduledDepartureUtc,
            OriginTotalTrafficHour = (double)totalTraffic,
            OriginHistAvgCongestion = histCongestion
        };
    }

    private FlightDto MapToDto(Flight flight)
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
}

public class PassengerService : IPassengerService
{
    private readonly AirlineDbContext _context;

    public PassengerService(AirlineDbContext context)
    {
        _context = context;
    }

    public async Task<List<PassengerDto>> GetAllPassengersAsync()
    {
        var passengers = await _context.Passengers.ToListAsync();
        return passengers.Select(MapToDto).ToList();
    }

    public async Task<PassengerDto?> GetPassengerByIdAsync(int id)
    {
        var passenger = await _context.Passengers.FindAsync(id);
        return passenger == null ? null : MapToDto(passenger);
    }

    public async Task<PassengerDto?> GetPassengerByPassportAsync(string passportNumber)
    {
        var passenger = await _context.Passengers.FirstOrDefaultAsync(p => p.PassportNumber == passportNumber);
        return passenger == null ? null : MapToDto(passenger);
    }

    public async Task<PassengerDto> CreatePassengerAsync(CreatePassengerDto dto)
    {
        var passenger = new Passenger
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PassportNumber = dto.PassportNumber,
            Nationality = dto.Nationality,
            DateOfBirth = dto.DateOfBirth,
            PassportExpiryDate = dto.PassportExpiryDate,
            Gender = dto.Gender,
            PassengerRole = PassengerRole.Main
        };

        _context.Passengers.Add(passenger);
        await _context.SaveChangesAsync();

        return MapToDto(passenger);
    }

    public async Task<PassengerDto?> UpdatePassengerAsync(int id, UpdatePassengerDto dto)
    {
        var passenger = await _context.Passengers.FindAsync(id);
        if (passenger == null) return null;

        if (dto.FirstName != null) passenger.FirstName = dto.FirstName;
        if (dto.LastName != null) passenger.LastName = dto.LastName;
        if (dto.Nationality != null) passenger.Nationality = dto.Nationality;
        if (dto.PassportExpiryDate.HasValue) passenger.PassportExpiryDate = dto.PassportExpiryDate.Value;
        if (dto.Gender != null) passenger.Gender = dto.Gender;

        await _context.SaveChangesAsync();
        return MapToDto(passenger);
    }

    public async Task<bool> DeletePassengerAsync(int id)
    {
        var passenger = await _context.Passengers.FindAsync(id);
        if (passenger == null) return false;

        _context.Passengers.Remove(passenger);
        await _context.SaveChangesAsync();
        return true;
    }

    private PassengerDto MapToDto(Passenger passenger) => new()
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
}
