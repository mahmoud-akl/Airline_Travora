namespace AirlineSimulation.Domain.Entities;

public class BaggageTag
{
    public int BaggageTagId { get; set; }
    public string TagNumber { get; set; } = string.Empty;
    public int TicketId { get; set; }
    public decimal? WeightKg { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Location tracking
    public string? PreviousLocation { get; set; }
    public string? CurrentLocation { get; set; }
    public DateTime? LastLocationUpdatedAt { get; set; }

    // Navigation properties
    public Ticket Ticket { get; set; } = null!;
}
