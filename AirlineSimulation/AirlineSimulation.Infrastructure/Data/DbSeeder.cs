using AirlineSimulation.Domain.Entities;
using AirlineSimulation.Domain.Enums;

namespace AirlineSimulation.Infrastructure.Data;

public static class DbSeeder
{
    public static void SeedData(AirlineDbContext context)
    {
        // Check if data already exists
        if (context.Flights.Any())
        {
            return; // Database has been seeded
        }

        // Seed Flights (20 flights)
        var flights = new List<Flight>
        {
            new Flight { FlightNumber = "AA101", DepartureAirport = "JFK", ArrivalAirport = "LAX", OriginCity = "New York", DestinationCity = "Los Angeles", DepartureTimeUtc = DateTime.UtcNow.AddHours(2), ArrivalTimeUtc = DateTime.UtcNow.AddHours(8), Terminal = "T1", Gate = "A12", FlightStatus = FlightStatus.Scheduled, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(2).AddMinutes(-45) },
            new Flight { FlightNumber = "BA202", DepartureAirport = "LHR", ArrivalAirport = "CDG", OriginCity = "London", DestinationCity = "Paris", DepartureTimeUtc = DateTime.UtcNow.AddHours(3), ArrivalTimeUtc = DateTime.UtcNow.AddHours(5), Terminal = "T2", Gate = "B5", FlightStatus = FlightStatus.Active, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(3).AddMinutes(-45) },
            new Flight { FlightNumber = "EK303", DepartureAirport = "DXB", ArrivalAirport = "CAI", OriginCity = "Dubai", DestinationCity = "Cairo", DepartureTimeUtc = DateTime.UtcNow.AddHours(1), ArrivalTimeUtc = DateTime.UtcNow.AddHours(4), Terminal = "T3", Gate = "C8", FlightStatus = FlightStatus.Delayed, DelayMinutes = 30, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(1).AddMinutes(-45) },
            new Flight { FlightNumber = "LH404", DepartureAirport = "FRA", ArrivalAirport = "MUC", OriginCity = "Frankfurt", DestinationCity = "Munich", DepartureTimeUtc = DateTime.UtcNow.AddHours(4), ArrivalTimeUtc = DateTime.UtcNow.AddHours(5), Terminal = "T1", Gate = "D3", FlightStatus = FlightStatus.Scheduled, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(4).AddMinutes(-45) },
            new Flight { FlightNumber = "AF505", DepartureAirport = "CDG", ArrivalAirport = "JFK", OriginCity = "Paris", DestinationCity = "New York", DepartureTimeUtc = DateTime.UtcNow.AddHours(6), ArrivalTimeUtc = DateTime.UtcNow.AddHours(14), Terminal = "T2", Gate = "E7", FlightStatus = FlightStatus.Scheduled, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(6).AddMinutes(-45) },
            new Flight { FlightNumber = "QR606", DepartureAirport = "DOH", ArrivalAirport = "LHR", OriginCity = "Doha", DestinationCity = "London", DepartureTimeUtc = DateTime.UtcNow.AddHours(5), ArrivalTimeUtc = DateTime.UtcNow.AddHours(12), Terminal = "T4", Gate = "F9", FlightStatus = FlightStatus.Active, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(5).AddMinutes(-45) },
            new Flight { FlightNumber = "SV707", DepartureAirport = "RUH", ArrivalAirport = "CAI", OriginCity = "Riyadh", DestinationCity = "Cairo", DepartureTimeUtc = DateTime.UtcNow.AddHours(2), ArrivalTimeUtc = DateTime.UtcNow.AddHours(4), Terminal = "T1", Gate = "G2", FlightStatus = FlightStatus.Scheduled, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(2).AddMinutes(-45) },
            new Flight { FlightNumber = "MS808", DepartureAirport = "CAI", ArrivalAirport = "DXB", OriginCity = "Cairo", DestinationCity = "Dubai", DepartureTimeUtc = DateTime.UtcNow.AddHours(3), ArrivalTimeUtc = DateTime.UtcNow.AddHours(6), Terminal = "T3", Gate = "H5", FlightStatus = FlightStatus.Landed, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(3).AddMinutes(-45) },
            new Flight { FlightNumber = "TK909", DepartureAirport = "IST", ArrivalAirport = "ATH", OriginCity = "Istanbul", DestinationCity = "Athens", DepartureTimeUtc = DateTime.UtcNow.AddHours(1), ArrivalTimeUtc = DateTime.UtcNow.AddHours(3), Terminal = "T2", Gate = "I4", FlightStatus = FlightStatus.Scheduled, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(1).AddMinutes(-45) },
            new Flight { FlightNumber = "UA1010", DepartureAirport = "ORD", ArrivalAirport = "SFO", OriginCity = "Chicago", DestinationCity = "San Francisco", DepartureTimeUtc = DateTime.UtcNow.AddHours(4), ArrivalTimeUtc = DateTime.UtcNow.AddHours(8), Terminal = "T1", Gate = "J6", FlightStatus = FlightStatus.Active, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(4).AddMinutes(-45) },
            new Flight { FlightNumber = "DL1111", DepartureAirport = "ATL", ArrivalAirport = "MIA", OriginCity = "Atlanta", DestinationCity = "Miami", DepartureTimeUtc = DateTime.UtcNow.AddHours(2), ArrivalTimeUtc = DateTime.UtcNow.AddHours(4), Terminal = "T3", Gate = "K1", FlightStatus = FlightStatus.Scheduled, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(2).AddMinutes(-45) },
            new Flight { FlightNumber = "NH1212", DepartureAirport = "NRT", ArrivalAirport = "HND", OriginCity = "Tokyo", DestinationCity = "Tokyo", DepartureTimeUtc = DateTime.UtcNow.AddHours(1), ArrivalTimeUtc = DateTime.UtcNow.AddHours(2), Terminal = "T1", Gate = "L3", FlightStatus = FlightStatus.Cancelled, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(1).AddMinutes(-45) },
            new Flight { FlightNumber = "SQ1313", DepartureAirport = "SIN", ArrivalAirport = "BKK", OriginCity = "Singapore", DestinationCity = "Bangkok", DepartureTimeUtc = DateTime.UtcNow.AddHours(3), ArrivalTimeUtc = DateTime.UtcNow.AddHours(5), Terminal = "T2", Gate = "M7", FlightStatus = FlightStatus.Scheduled, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(3).AddMinutes(-45) },
            new Flight { FlightNumber = "CX1414", DepartureAirport = "HKG", ArrivalAirport = "TPE", OriginCity = "Hong Kong", DestinationCity = "Taipei", DepartureTimeUtc = DateTime.UtcNow.AddHours(2), ArrivalTimeUtc = DateTime.UtcNow.AddHours(4), Terminal = "T1", Gate = "N2", FlightStatus = FlightStatus.Active, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(2).AddMinutes(-45) },
            new Flight { FlightNumber = "KE1515", DepartureAirport = "ICN", ArrivalAirport = "NRT", OriginCity = "Seoul", DestinationCity = "Tokyo", DepartureTimeUtc = DateTime.UtcNow.AddHours(4), ArrivalTimeUtc = DateTime.UtcNow.AddHours(6), Terminal = "T2", Gate = "O8", FlightStatus = FlightStatus.Scheduled, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(4).AddMinutes(-45) },
            new Flight { FlightNumber = "AC1616", DepartureAirport = "YYZ", ArrivalAirport = "YVR", OriginCity = "Toronto", DestinationCity = "Vancouver", DepartureTimeUtc = DateTime.UtcNow.AddHours(5), ArrivalTimeUtc = DateTime.UtcNow.AddHours(10), Terminal = "T1", Gate = "P4", FlightStatus = FlightStatus.Delayed, DelayMinutes = 45, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(5).AddMinutes(-45) },
            new Flight { FlightNumber = "VA1717", DepartureAirport = "SYD", ArrivalAirport = "MEL", OriginCity = "Sydney", DestinationCity = "Melbourne", DepartureTimeUtc = DateTime.UtcNow.AddHours(1), ArrivalTimeUtc = DateTime.UtcNow.AddHours(3), Terminal = "T3", Gate = "Q6", FlightStatus = FlightStatus.Scheduled, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(1).AddMinutes(-45) },
            new Flight { FlightNumber = "NZ1818", DepartureAirport = "AKL", ArrivalAirport = "WLG", OriginCity = "Auckland", DestinationCity = "Wellington", DepartureTimeUtc = DateTime.UtcNow.AddHours(2), ArrivalTimeUtc = DateTime.UtcNow.AddHours(3), Terminal = "T1", Gate = "R1", FlightStatus = FlightStatus.Active, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(2).AddMinutes(-45) },
            new Flight { FlightNumber = "SA1919", DepartureAirport = "JNB", ArrivalAirport = "CPT", OriginCity = "Johannesburg", DestinationCity = "Cape Town", DepartureTimeUtc = DateTime.UtcNow.AddHours(3), ArrivalTimeUtc = DateTime.UtcNow.AddHours(5), Terminal = "T2", Gate = "S5", FlightStatus = FlightStatus.Scheduled, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(3).AddMinutes(-45) },
            new Flight { FlightNumber = "LA2020", DepartureAirport = "GRU", ArrivalAirport = "EZE", OriginCity = "Sao Paulo", DestinationCity = "Buenos Aires", DepartureTimeUtc = DateTime.UtcNow.AddHours(4), ArrivalTimeUtc = DateTime.UtcNow.AddHours(7), Terminal = "T3", Gate = "T9", FlightStatus = FlightStatus.Scheduled, ScheduledBoardingTimeUtc = DateTime.UtcNow.AddHours(4).AddMinutes(-45) }
        };
        context.Flights.AddRange(flights);
        context.SaveChanges();

        // Seed Passengers (100 passengers - mix of main and companion)
        var passengers = new List<Passenger>();
        var firstNames = new[] { "Ahmed", "Mohamed", "Sara", "Fatima", "Ali", "Nour", "Youssef", "Layla", "Omar", "Mona", "John", "Emma", "Michael", "Sophia", "David", "Olivia", "James", "Ava", "Robert", "Isabella" };
        var lastNames = new[] { "Hassan", "Ibrahim", "Mahmoud", "Khalil", "Saleh", "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez" };
        var nationalities = new[] { "Egyptian", "American", "British", "French", "German", "Canadian", "Australian", "Japanese", "Chinese", "Indian" };

        for (int i = 1; i <= 100; i++)
        {
            passengers.Add(new Passenger
            {
                FirstName = firstNames[i % firstNames.Length],
                LastName = lastNames[i % lastNames.Length],
                PassportNumber = $"P{1000000 + i}",
                Nationality = nationalities[i % nationalities.Length],
                DateOfBirth = DateTime.UtcNow.AddYears(-30 - (i % 40)),
                PassportExpiryDate = DateTime.UtcNow.AddYears(2 + (i % 5)),
                PassengerRole = i % 4 == 0 ? PassengerRole.Companion : PassengerRole.Main
            });
        }
        context.Passengers.AddRange(passengers);
        context.SaveChanges();

        // Seed Tickets (150 tickets)
        var tickets = new List<Ticket>();
        var random = new Random();
        var travelClasses = new[] { TravelClass.Economy, TravelClass.Business, TravelClass.FirstClass };

        for (int i = 1; i <= 150; i++)
        {
            var flight = flights[i % flights.Count];
            var passenger = passengers[i % passengers.Count];
            var seatRow = 1 + (i % 30);
            var seatLetter = (char)('A' + (i % 6));

            var ticket = new Ticket
            {
                TicketNumber = $"TKT{100000 + i}",
                FlightId = flight.FlightId,
                PassengerId = passenger.PassengerId,
                SeatNumber = $"{seatRow}{seatLetter}",
                TravelClass = travelClasses[i % travelClasses.Length],
                BoardingStatus = BoardingStatus.NotBoarded
            };

            // Generate boarding pass for some tickets (about 40%)
            if (i % 5 < 2)
            {
                ticket.BoardingTimeUtc = flight.DepartureTimeUtc.AddMinutes(-45);
                ticket.Terminal = flight.Terminal;
                ticket.Gate = flight.Gate;
                ticket.BarcodeData = $"BARCODE_{ticket.TicketNumber}_{flight.FlightNumber}";
                ticket.BoardingStatus = i % 10 == 0 ? BoardingStatus.Boarded : BoardingStatus.NotBoarded;
            }

            tickets.Add(ticket);
        }
        context.Tickets.AddRange(tickets);
        context.SaveChanges();

        // Seed Baggage Tags (50 tags for selected tickets)
        var baggageTags = new List<BaggageTag>();
        for (int i = 0; i < 50; i++)
        {
            var ticket = tickets[i * 3]; // Select every 3rd ticket
            var tagCount = 1 + (i % 2); // 1 or 2 bags per ticket

            for (int j = 0; j < tagCount; j++)
            {
                baggageTags.Add(new BaggageTag
                {
                    TagNumber = $"BAG{100000 + (i * 10) + j}",
                    TicketId = ticket.TicketId,
                    WeightKg = 15 + (decimal)(random.NextDouble() * 15) // 15-30 kg
                });
            }
        }
        context.BaggageTags.AddRange(baggageTags);
        context.SaveChanges();
    }
}
