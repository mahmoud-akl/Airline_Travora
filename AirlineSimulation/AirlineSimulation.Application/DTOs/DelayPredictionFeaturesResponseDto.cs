using System;

namespace AirlineSimulation.Application.DTOs;

public class DelayPredictionFeaturesResponseDto
{
    public string FlightNumber { get; set; } = string.Empty;
    public string DepartureIataCode { get; set; } = string.Empty;
    public DateTime ScheduledDepartureUtc { get; set; }
    
    // عدد الرحلات المجدولة الفعلي في نفس الساعة واليوم بمطار المغادرة
    public double OriginTotalTrafficHour { get; set; }
    
    // متوسط الزحام التاريخي للمطار في هذا اليوم من الأسبوع وفي هذه الساعة
    public double OriginHistAvgCongestion { get; set; }
}
