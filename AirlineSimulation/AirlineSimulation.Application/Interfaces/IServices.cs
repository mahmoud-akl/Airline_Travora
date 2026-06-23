using AirlineSimulation.Application.DTOs;

namespace AirlineSimulation.Application.Interfaces;

public interface IFlightService
{
    Task<List<FlightDto>> GetAllFlightsAsync();
    Task<FlightDto?> GetFlightByIdAsync(int id);
    Task<FlightDto?> GetFlightByNumberAsync(string flightNumber);
    Task<FlightDto> CreateFlightAsync(CreateFlightDto dto);
    Task<FlightDto?> UpdateFlightAsync(int id, UpdateFlightDto dto);
    Task<bool> DeleteFlightAsync(int id);
    Task<FlightDto?> UpdateFlightStatusAsync(int id, string newStatus);
    Task<DelayPredictionFeaturesResponseDto> GetDelayPredictionFeaturesAsync(string flightNumber, string departureIataCode, DateTime scheduledDepartureUtc);
}

public interface IPassengerService
{
    Task<List<PassengerDto>> GetAllPassengersAsync();
    Task<PassengerDto?> GetPassengerByIdAsync(int id);
    Task<PassengerDto?> GetPassengerByPassportAsync(string passportNumber);
    Task<PassengerDto> CreatePassengerAsync(CreatePassengerDto dto);
    Task<PassengerDto?> UpdatePassengerAsync(int id, UpdatePassengerDto dto);
    Task<bool> DeletePassengerAsync(int id);
}

public interface ITicketService
{
    Task<List<TicketDto>> GetAllTicketsAsync();
    Task<TicketDto?> GetTicketByIdAsync(int id);
    Task<TicketDto?> GetTicketByNumberAsync(string ticketNumber);
    Task<TicketDto> CreateTicketAsync(CreateTicketDto dto);
    Task<TicketDto?> UpdateTicketAsync(int id, UpdateTicketDto dto);
    Task<bool> DeleteTicketAsync(int id);
    Task<TicketDto?> GenerateBoardingPassAsync(string ticketNumber);
    Task<BoardingPassResponseDto?> IssueBoardingPassAsync(string ticketNumber);
    Task<TicketBaggageAllowanceDto?> GetTicketBaggageAllowanceAsync(string ticketNumber);
}

public interface IBaggageTagService
{
    Task<List<BaggageTagDto>> GenerateBaggageTagsAsync(string ticketNumber, int count);
    Task<List<BaggageTagDto>> GetBaggageTagsByTicketAsync(int ticketId);
    Task<BaggageTagDto?> GetBaggageTagByNumberAsync(string tagNumber);
    Task<VerifyBaggageResponseDto?> VerifyBaggageTagAsync(string tagNumber);
    Task<BaggageInfoResponseDto?> GetBaggageInfoAsync(string? ticketNumber, string? passportNumber);

    // Location tracking
    Task<BaggageLocationResponseDto?> UpdateBaggageLocationAsync(string tagNumber, UpdateBaggageLocationDto dto);
    Task<BaggageLocationResponseDto?> GetBaggageLastLocationAsync(string tagNumber);
    Task<BaggagesByTicketResponseDto?> GetBaggageTagsByTicketNumberAsync(string ticketNumber);
    Task<List<BaggageLocationResponseDto>?> UpdateMultipleBaggageLocationsAsync(string ticketNumber, UpdateBaggageLocationDto dto);
    Task<BaggageTagDto?> UpdateBaggageWeightAsync(string tagNumber, decimal weightKg);
    Task<bool> DeleteBaggageTagAsync(string tagNumber);
    Task<bool> DeleteAllBaggageByTicketAsync(string ticketNumber);
}

public interface IValidationService
{
    Task<ValidationResponseDto> ValidateTicketAsync(ValidationRequestDto request);
}
