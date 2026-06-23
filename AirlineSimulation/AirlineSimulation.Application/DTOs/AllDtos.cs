using System.Text.Json.Serialization;
using AirlineSimulation.Domain.Enums;

namespace AirlineSimulation.Application.DTOs;

public class FlightDto
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
    public string FlightStatus { get; set; } = string.Empty;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? DelayMinutes { get; set; }
    public string AirlineName { get; set; } = string.Empty;
    public string AirlineIcaoCode { get; set; } = string.Empty;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AirlineIataCode { get; set; }
    public string DepartureIataCode { get; set; } = string.Empty;
    public string ArrivalIataCode { get; set; } = string.Empty;
}

public class CreateFlightDto
{
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
    public string AirlineName { get; set; } = "Egypt Air";
    public string AirlineIcaoCode { get; set; } = "MS";
    public string AirlineIataCode { get; set; } = string.Empty;
    public string DepartureIataCode { get; set; } = string.Empty;
    public string ArrivalIataCode { get; set; } = string.Empty;
}

public class UpdateFlightDto
{
    public string? FlightNumber { get; set; }
    public string? DepartureAirport { get; set; }
    public string? ArrivalAirport { get; set; }
    public DateTime? DepartureTimeUtc { get; set; }
    public DateTime? ArrivalTimeUtc { get; set; }
    public string? AirlineName { get; set; }
    public string? AirlineIcaoCode { get; set; }
    public string? AirlineIataCode { get; set; }
    public string? DepartureIataCode { get; set; }
    public string? ArrivalIataCode { get; set; }
    public string? Terminal { get; set; }
    public string? Gate { get; set; }
    public string? OriginCity { get; set; }
    public string? DestinationCity { get; set; }
    public DateTime? ScheduledBoardingTimeUtc { get; set; }
    public string? FlightStatus { get; set; }
    public int? DelayMinutes { get; set; }
}

public class UpdateFlightStatusDto
{
    public string Status { get; set; } = string.Empty;
}

public class PassengerDto
{
    public int PassengerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public DateTime PassportExpiryDate { get; set; }
    public string Gender { get; set; } = string.Empty;
}

public class CreatePassengerDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public DateTime PassportExpiryDate { get; set; }
    public string Gender { get; set; } = "Male";
}

public class UpdatePassengerDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Nationality { get; set; }
    public DateTime? PassportExpiryDate { get; set; }
    public string? Gender { get; set; }
}

public class TicketDto
{
    public int TicketId { get; set; }
    public string TicketNumber { get; set; } = string.Empty;
    public int FlightId { get; set; }
    public int PassengerId { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public string TravelClass { get; set; } = string.Empty;
    public DateTime? BoardingTimeUtc { get; set; }
    public string? Terminal { get; set; }
    public string? Gate { get; set; }
    public string? BarcodeData { get; set; }
    public string BoardingStatus { get; set; } = string.Empty;
    
    // Computed fields for boarding pass
    public string? FlightDate { get; set; }
    public string? FlightDuration { get; set; }
    
    public int AllowedBaggageCount { get; set; }
    public decimal MaxAllowedWeight { get; set; }
    
    public int BaggageCount { get; set; }
    public decimal TotalBaggageWeight { get; set; }
    
    public FlightDto? Flight { get; set; }
    public PassengerDto? Passenger { get; set; }
}

public class CreateTicketDto
{
    public string TicketNumber { get; set; } = string.Empty;
    public int FlightId { get; set; }
    public int PassengerId { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public string TravelClass { get; set; } = "Economy";
    public int AllowedBaggageCount { get; set; }
    public decimal MaxAllowedWeight { get; set; }
}

public class TicketBaggageAllowanceDto
{
    public string TicketNumber { get; set; } = string.Empty;
    public int AllowedBaggageCount { get; set; }
    public decimal MaxAllowedWeight { get; set; }
}

public class UpdateTicketDto
{
    public string? SeatNumber { get; set; }
    public string? TravelClass { get; set; }
    public string? BoardingStatus { get; set; }
    public int? AllowedBaggageCount { get; set; }
    public decimal? MaxAllowedWeight { get; set; }
}

public class BaggageTagDto
{
    public int BaggageTagId { get; set; }
    public string TagNumber { get; set; } = string.Empty;
    public int TicketId { get; set; }
    public decimal? WeightKg { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Enriched details for external systems (Travora)
    public string? FlightNumber { get; set; }
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    public DateTime? DepartureTime { get; set; }
    public DateTime? ArrivalTime { get; set; }
    public string? Terminal { get; set; }
    public string? Gate { get; set; }
    public DateTime? BoardingTime { get; set; }
    public string? PassengerName { get; set; }
    public string? SeatNumber { get; set; }

    // Location tracking
    public string? CurrentLocation { get; set; }
    public DateTime? LastLocationUpdatedAt { get; set; }
}

public class VerifyBaggageResponseDto
{
    public bool Valid { get; set; }
    public string? Passport { get; set; }
    public VerifyBaggageTagDto? Tag { get; set; }
}

public class VerifyBaggageTagDto
{
    public string TagNumber { get; set; } = string.Empty;
    public decimal? WeightKg { get; set; }
    public string? Destination { get; set; }
    public string? FlightNumber { get; set; }
    public string? Origin { get; set; }
    public string? Gate { get; set; }
    public string? Terminal { get; set; }
    public string? PassengerName { get; set; }
    public DateTime? DepartureTime { get; set; }
    public DateTime? BoardingTime { get; set; }
}

public class BaggageInfoResponseDto
{
    public string PassengerName { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public List<TicketBaggageDto> Tickets { get; set; } = new();
    public int TotalBaggageCount { get; set; }
}

public class TicketBaggageDto
{
    public string TicketNumber { get; set; } = string.Empty;
    public string FlightNumber { get; set; } = string.Empty;
    public string DepartureAirport { get; set; } = string.Empty;
    public string ArrivalAirport { get; set; } = string.Empty;
    public List<BaggageTagDto> BaggageTags { get; set; } = new();
    public int BaggageCount => BaggageTags.Count;
}

public class ValidationRequestDto
{
    public string PassportNumber { get; set; } = string.Empty;
    public string TicketNumber { get; set; } = string.Empty;
    public string FlightNumber { get; set; } = string.Empty;
    public string? FlightDate { get; set; }
}


public class ValidationResponseDto
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; } = new();
    public TicketDto? Ticket { get; set; }
    public FlightDto? Flight { get; set; }
    public PassengerDto? Passenger { get; set; }
    
    // Computed fields for complete boarding pass
    public string? FlightDate { get; set; }
    public string? FlightDuration { get; set; }
    public DateTime? BoardingTimeUtc { get; set; }
    public string? Terminal { get; set; }
    public string? Gate { get; set; }
}

// Customs System DTOs
public class ProductLookupDto
{
    public bool Found { get; set; }
    public ProductDetailsDto? Product { get; set; }
}

public class ProductDetailsDto
{
    public string Name { get; set; } = string.Empty;
    public decimal CustomsRate { get; set; }
    public string Category { get; set; } = string.Empty;
    public string SubCategory { get; set; } = string.Empty;
}

public class CategoryDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SubCategoriesCount { get; set; }
}

public class SubCategoryDto
{
    public int SubCategoryId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public int ProductsCount { get; set; }
}

public class ProductDto
{
    public int ProductId { get; set; }
    public int SubCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal CustomsRate { get; set; }
    public string SubCategoryName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
}

public class CreateCategoryDto
{
    public string Name { get; set; } = string.Empty;
}

public class CreateSubCategoryDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateProductDto
{
    public int SubCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal CustomsRate { get; set; }
}

public class UpdateProductDto
{
    public string? Name { get; set; }
    public decimal? CustomsRate { get; set; }
}

// Baggage Location Tracking DTOs
public class UpdateBaggageLocationDto
{
    public string Location { get; set; } = string.Empty;
    public string? Notes { get; set; }
}

public class UpdateBaggageWeightDto
{
    public decimal WeightKg { get; set; }
}

public class BaggageLocationResponseDto
{
    public string TagNumber { get; set; } = string.Empty;
    public string? PreviousLocation { get; set; }
    public string CurrentLocation { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
}

public class BaggagesByTicketResponseDto
{
    public string TicketNumber { get; set; } = string.Empty;
    public string FlightNumber { get; set; } = string.Empty;
    public string PassengerName { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public List<BaggageTagDto> Bags { get; set; } = new();
}

// Boarding Pass DTOs
public class IssueBoardingPassRequestDto
{
    public string TicketNumber { get; set; } = string.Empty;
}

public class BoardingPassDto
{
    public string AirlineName { get; set; } = string.Empty;
    public string AirlineIataCode { get; set; } = string.Empty;
    public string FlightNumber { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string FromCity { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string ToCity { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string DepartureTime { get; set; } = string.Empty;
    public string ArrivalTime { get; set; } = string.Empty;
    public string PassengerName { get; set; } = string.Empty;
    public string SeatNumber { get; set; } = string.Empty;
    public string Terminal { get; set; } = string.Empty;
    public string Gate { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public string BoardingTime { get; set; } = string.Empty;
    public string FlightDate { get; set; } = string.Empty;
    public string BarcodeData { get; set; } = string.Empty;
}

public class BoardingPassResponseDto
{
    public List<BoardingPassDto> BoardingPasses { get; set; } = new();
}

