using Microsoft.AspNetCore.Mvc;
using AirlineSimulation.Application.DTOs;
using AirlineSimulation.Application.Interfaces;

namespace AirlineSimulation.API.Controllers;

[ApiController]
[Route("api/airline")]
public class AirlineController : ControllerBase
{
    private readonly IValidationService _validationService;
    private readonly IBaggageTagService _baggageTagService;
    private readonly ITicketService _ticketService;
    private readonly IFlightService _flightService;

    public AirlineController(
        IValidationService validationService, 
        IBaggageTagService baggageTagService, 
        ITicketService ticketService,
        IFlightService flightService)
    {
        _validationService = validationService;
        _baggageTagService = baggageTagService;
        _ticketService = ticketService;
        _flightService = flightService;
    }

    /// <summary>
    /// Public API: Validate ticket for Travora integration
    /// </summary>
    [HttpPost("validate-ticket")]
    public async Task<ActionResult<ValidationResponseDto>> ValidateTicket([FromBody] ValidationRequestDto request)
    {
        try
        {
            var result = await _validationService.ValidateTicketAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "INTERNAL_ERROR", message = ex.Message });
        }
    }

    /// <summary>
    /// Public API: Generate baggage tags for Travora integration
    /// </summary>
    [HttpPost("generate-baggage-tags")]
    public async Task<ActionResult<object>> GenerateBaggageTags([FromBody] GenerateBaggageTagsRequest request)
    {
        try
        {
            var tags = await _baggageTagService.GenerateBaggageTagsAsync(request.TicketNumber, request.BaggageCount);
            return Ok(new { tags });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "INTERNAL_ERROR", message = ex.Message });
        }
    }

    /// <summary>
    /// Public API: Verify baggage tag existence and details
    /// </summary>
    [HttpGet("verify-baggage/{tagNumber}")]
    public async Task<ActionResult<object>> VerifyBaggageTag(string tagNumber)
    {
        var result = await _baggageTagService.VerifyBaggageTagAsync(tagNumber);
        if (result == null)
            return Ok(new { valid = false, message = "Tag not found" });

        return Ok(result);
    }

    /// <summary>
    /// Public API: Check baggage info by ticket number or passport number
    /// </summary>
    [HttpGet("baggage-check")]
    public async Task<ActionResult<BaggageInfoResponseDto>> CheckBaggage([FromQuery] string? ticketNumber, [FromQuery] string? passportNumber)
    {
        try
        {
            if (string.IsNullOrEmpty(ticketNumber) && string.IsNullOrEmpty(passportNumber))
            {
                return BadRequest(new { error = "MISSING_PARAMETERS", message = "Either ticketNumber or passportNumber must be provided." });
            }

            var result = await _baggageTagService.GetBaggageInfoAsync(ticketNumber, passportNumber);
            
            if (result == null)
            {
                return NotFound(new { error = "NOT_FOUND", message = "Passenger or Ticket not found." });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "INTERNAL_ERROR", message = ex.Message });
        }
    }

    /// <summary>
    /// Issue boarding pass for a ticket
    /// </summary>
    [HttpPost("issue-boarding-pass")]
    public async Task<ActionResult<BoardingPassResponseDto>> IssueBoardingPass([FromBody] IssueBoardingPassRequestDto request)
    {
        try
        {
            var result = await _ticketService.IssueBoardingPassAsync(request.TicketNumber);
            if (result == null)
                return NotFound(new { error = "NOT_FOUND", message = "Ticket not found." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "INTERNAL_ERROR", message = ex.Message });
        }
    }

    /// <summary>
    /// Update baggage location manually (for testing without cameras)
    /// </summary>
    [HttpPatch("baggage/{tagNumber}/location")]
    public async Task<ActionResult<BaggageLocationResponseDto>> UpdateBaggageLocation(string tagNumber, [FromBody] UpdateBaggageLocationDto dto)
    {
        try
        {
            var result = await _baggageTagService.UpdateBaggageLocationAsync(tagNumber, dto);
            if (result == null)
                return NotFound(new { error = "NOT_FOUND", message = "Baggage tag not found." });

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = "INVALID_LOCATION", message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "INTERNAL_ERROR", message = ex.Message });
        }
    }

    /// <summary>
    /// Update baggage weight
    /// </summary>
    [HttpPatch("baggage/{tagNumber}/weight")]
    public async Task<ActionResult<BaggageTagDto>> UpdateBaggageWeight(string tagNumber, [FromBody] UpdateBaggageWeightDto dto)
    {
        try
        {
            var result = await _baggageTagService.UpdateBaggageWeightAsync(tagNumber, dto.WeightKg);
            if (result == null)
                return NotFound(new { error = "NOT_FOUND", message = "Baggage tag not found." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "INTERNAL_ERROR", message = ex.Message });
        }
    }

    /// <summary>
    /// Get last known location for a baggage tag
    /// </summary>
    [HttpGet("baggage/{tagNumber}/last-location")]
    public async Task<ActionResult<BaggageLocationResponseDto>> GetBaggageLastLocation(string tagNumber)
    {
        var result = await _baggageTagService.GetBaggageLastLocationAsync(tagNumber);
        if (result == null)
            return NotFound(new { error = "NOT_FOUND", message = "Baggage tag not found." });

        return Ok(result);
    }

    /// <summary>
    /// Get all baggage tags for a ticket
    /// </summary>
    [HttpGet("baggage/by-ticket/{ticketNumber}")]
    public async Task<ActionResult<BaggagesByTicketResponseDto>> GetBaggageByTicket(string ticketNumber)
    {
        var result = await _baggageTagService.GetBaggageTagsByTicketNumberAsync(ticketNumber);
        if (result == null)
            return NotFound(new { error = "NOT_FOUND", message = "Ticket not found." });

        return Ok(result);
    }

    /// <summary>
    /// Update location for all baggage tags of a ticket at once
    /// </summary>
    [HttpPatch("baggage/by-ticket/{ticketNumber}/location")]
    public async Task<ActionResult<List<BaggageLocationResponseDto>>> UpdateAllBaggageByTicket(string ticketNumber, [FromBody] UpdateBaggageLocationDto dto)
    {
        try
        {
            var result = await _baggageTagService.UpdateMultipleBaggageLocationsAsync(ticketNumber, dto);
            if (result == null)
                return NotFound(new { error = "NOT_FOUND", message = "Ticket not found." });

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = "INVALID_LOCATION", message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "INTERNAL_ERROR", message = ex.Message });
        }
    }

    /// <summary>
    /// Delete a single baggage tag
    /// </summary>
    [HttpDelete("baggage/{tagNumber}")]
    public async Task<ActionResult> DeleteBaggageTag(string tagNumber)
    {
        var result = await _baggageTagService.DeleteBaggageTagAsync(tagNumber);
        if (!result) return NotFound(new { error = "NOT_FOUND", message = "Baggage tag not found." });
        return NoContent();
    }

    /// <summary>
    /// Delete all baggage tags for a ticket
    /// </summary>
    [HttpDelete("baggage/by-ticket/{ticketNumber}")]
    public async Task<ActionResult> DeleteAllBaggageByTicket(string ticketNumber)
    {
        var result = await _baggageTagService.DeleteAllBaggageByTicketAsync(ticketNumber);
        if (!result) return NotFound(new { error = "NOT_FOUND", message = "Ticket not found or has no baggage." });
        return NoContent();
    }
    /// <summary>
    /// Get ONLY baggage allowance for a specific ticket
    /// </summary>
    [HttpGet("tickets/{ticketNumber}/baggage-allowance")]
    public async Task<ActionResult<TicketBaggageAllowanceDto>> GetTicketBaggageAllowance(string ticketNumber)
    {
        var allowance = await _ticketService.GetTicketBaggageAllowanceAsync(ticketNumber);
        if (allowance == null)
            return NotFound(new { error = "NOT_FOUND", message = "Ticket not found." });

        return Ok(allowance);
    }

    [HttpGet("flights/delay-prediction-features")]
    public async Task<ActionResult<DelayPredictionFeaturesResponseDto>> GetDelayPredictionFeatures(
        [FromQuery] string flightNumber,
        [FromQuery] string departureIataCode,
        [FromQuery] DateTime scheduledDepartureUtc)
    {
        try
        {
            var result = await _flightService.GetDelayPredictionFeaturesAsync(flightNumber, departureIataCode, scheduledDepartureUtc);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "INTERNAL_ERROR", message = ex.Message });
        }
    }
}

public class GenerateBaggageTagsRequest
{
    public string TicketNumber { get; set; } = string.Empty;
    public int BaggageCount { get; set; }
}

[ApiController]
[Route("api/admin/tickets")]
public class AdminTicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly IBaggageTagService _baggageTagService;

    public AdminTicketsController(ITicketService ticketService, IBaggageTagService baggageTagService)
    {
        _ticketService = ticketService;
        _baggageTagService = baggageTagService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TicketDto>>> GetAll()
    {
        var tickets = await _ticketService.GetAllTicketsAsync();
        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDto>> GetById(int id)
    {
        var ticket = await _ticketService.GetTicketByIdAsync(id);
        if (ticket == null) return NotFound();
        return Ok(ticket);
    }

    [HttpPost]
    public async Task<ActionResult<TicketDto>> Create([FromBody] CreateTicketDto dto)
    {
        var ticket = await _ticketService.CreateTicketAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = ticket.TicketId }, ticket);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TicketDto>> Update(int id, [FromBody] UpdateTicketDto dto)
    {
        var ticket = await _ticketService.UpdateTicketAsync(id, dto);
        if (ticket == null) return NotFound();
        return Ok(ticket);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _ticketService.DeleteTicketAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPost("{ticketNumber}/generate-boarding-pass")]
    public async Task<ActionResult<TicketDto>> GenerateBoardingPass(string ticketNumber)
    {
        var ticket = await _ticketService.GenerateBoardingPassAsync(ticketNumber);
        if (ticket == null) return NotFound();
        return Ok(ticket);
    }

    [HttpPost("{ticketNumber}/generate-baggage-tags")]
    public async Task<ActionResult<object>> GenerateBaggageTags(string ticketNumber, [FromQuery] int count = 1)
    {
        var tags = await _baggageTagService.GenerateBaggageTagsAsync(ticketNumber, count);
        return Ok(new { tags });
    }

    [HttpPost("seed-customs-data")]
    public async Task<ActionResult> SeedCustomsData([FromServices] AirlineSimulation.Infrastructure.Data.AirlineDbContext context)
    {
        await AirlineSimulation.Infrastructure.Data.CustomsDataSeeder.SeedCustomsDataAsync(context);
        return Ok(new { message = "Customs data seeded successfully." });
    }
}
