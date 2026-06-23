using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AirlineSimulation.Application.DTOs;
using AirlineSimulation.Application.Interfaces;

namespace AirlineSimulation.API.Controllers;

[ApiController]
[Route("api/admin/flights")]
// [Authorize(Roles = "Admin")] // Uncomment when JWT is fully configured
public class AdminFlightsController : ControllerBase
{
    private readonly IFlightService _flightService;

    public AdminFlightsController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    [HttpGet]
    public async Task<ActionResult<List<FlightDto>>> GetAll()
    {
        var flights = await _flightService.GetAllFlightsAsync();
        return Ok(flights);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FlightDto>> GetById(int id)
    {
        var flight = await _flightService.GetFlightByIdAsync(id);
        if (flight == null) return NotFound();
        return Ok(flight);
    }

    [HttpPost]
    public async Task<ActionResult<FlightDto>> Create([FromBody] CreateFlightDto dto)
    {
        var flight = await _flightService.CreateFlightAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = flight.FlightId }, flight);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<FlightDto>> Update(int id, [FromBody] UpdateFlightDto dto)
    {
        var flight = await _flightService.UpdateFlightAsync(id, dto);
        if (flight == null) return NotFound();
        return Ok(flight);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _flightService.DeleteFlightAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPatch("{id}/status")]
    public async Task<ActionResult<FlightDto>> UpdateStatus(int id, [FromBody] UpdateFlightStatusDto request)
    {
        if (string.IsNullOrEmpty(request.Status)) return BadRequest("Status is required");
        
        var flight = await _flightService.UpdateFlightStatusAsync(id, request.Status);
        if (flight == null) return NotFound();
        return Ok(flight);
    }
}

[ApiController]
[Route("api/admin/passengers")]
// [Authorize(Roles = "Admin")]
public class AdminPassengersController : ControllerBase
{
    private readonly IPassengerService _passengerService;

    public AdminPassengersController(IPassengerService passengerService)
    {
        _passengerService = passengerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PassengerDto>>> GetAll()
    {
        var passengers = await _passengerService.GetAllPassengersAsync();
        return Ok(passengers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PassengerDto>> GetById(int id)
    {
        var passenger = await _passengerService.GetPassengerByIdAsync(id);
        if (passenger == null) return NotFound();
        return Ok(passenger);
    }

    [HttpPost]
    public async Task<ActionResult<PassengerDto>> Create([FromBody] CreatePassengerDto dto)
    {
        var passenger = await _passengerService.CreatePassengerAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = passenger.PassengerId }, passenger);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PassengerDto>> Update(int id, [FromBody] UpdatePassengerDto dto)
    {
        var passenger = await _passengerService.UpdatePassengerAsync(id, dto);
        if (passenger == null) return NotFound();
        return Ok(passenger);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _passengerService.DeletePassengerAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}

[ApiController]
[Route("api/admin/auth")]
public class AdminAuthController : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public ActionResult<object> Login([FromBody] LoginRequest request)
    {
        // Simple hardcoded authentication for demo
        if (request.Username == "admin" && request.Password == "admin123")
        {
            // In production, generate actual JWT token
            var token = "demo_token_admin_authenticated";
            return Ok(new
            {
                token,
                username = "admin",
                role = "Admin",
                message = "Login successful"
            });
        }

        return Unauthorized(new { message = "Invalid credentials" });
    }
}

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
