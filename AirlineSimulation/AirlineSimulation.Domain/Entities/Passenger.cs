using AirlineSimulation.Domain.Enums;

namespace AirlineSimulation.Domain.Entities;

public class Passenger
{
    public int PassengerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public DateTime PassportExpiryDate { get; set; }
    public string Gender { get; set; } = "Male"; // Default or required
    public PassengerRole PassengerRole { get; set; } = PassengerRole.Main;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
