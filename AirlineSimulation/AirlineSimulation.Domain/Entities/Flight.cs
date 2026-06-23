using AirlineSimulation.Domain.Enums;

namespace AirlineSimulation.Domain.Entities;

public class Flight
{
    public int FlightId { get; set; }
    public string FlightNumber { get; set; } = string.Empty;
    public string DepartureAirport { get; set; } = string.Empty;
    public string ArrivalAirport { get; set; } = string.Empty;
    public DateTime DepartureTimeUtc { get; set; }
    public DateTime ArrivalTimeUtc { get; set; }
    public string? Terminal { get; set; }
    public string? Gate { get; set; }
    public string OriginCity { get; set; } = string.Empty;
    public string DestinationCity { get; set; } = string.Empty;
    public DateTime? ScheduledBoardingTimeUtc { get; set; }
    public FlightStatus FlightStatus { get; set; } = FlightStatus.Scheduled;
    public int DelayMinutes { get; set; } = 0;
    
    // Airline Information
    public string AirlineName { get; set; } = "Egypt Air";
    public string AirlineIcaoCode { get; set; } = "MS";
    public string AirlineIataCode { get; set; } = string.Empty;
    public string DepartureIataCode { get; set; } = string.Empty;
    public string ArrivalIataCode { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
