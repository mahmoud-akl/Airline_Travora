using AirlineSimulation.Domain.Enums;

namespace AirlineSimulation.Domain.Entities;

public class Ticket
{
    public int TicketId { get; set; }
    public string TicketNumber { get; set; } = string.Empty;
    public int FlightId { get; set; }
    public int PassengerId { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public TravelClass TravelClass { get; set; } = TravelClass.Economy;

    // Boarding Pass fields (embedded in ticket)
    public DateTime? BoardingTimeUtc { get; set; }
    public string? Terminal { get; set; }
    public string? Gate { get; set; }
    public string? BarcodeData { get; set; }
    public BoardingStatus BoardingStatus { get; set; } = BoardingStatus.NotBoarded;

    public int AllowedBaggageCount { get; set; }
    public decimal MaxAllowedWeight { get; set; }

    public DateTime IssuedAtUtc { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Flight Flight { get; set; } = null!;
    public Passenger Passenger { get; set; } = null!;
    public ICollection<BaggageTag> BaggageTags { get; set; } = new List<BaggageTag>();
}
