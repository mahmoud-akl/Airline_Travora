using System;

namespace AirlineSimulation.Domain.Entities;

public class AirportCongestion
{
    public int AirportCongestionId { get; set; }
    
    // كود مطار المغادرة (مثل CAI أو JED)
    public string AirportIataCode { get; set; } = string.Empty;
    
    // يوم الأسبوع (0 = الأحد، 1 = الإثنين، ...، 6 = السبت)
    public int DayOfWeek { get; set; }
    
    // ساعة اليوم (من 0 إلى 23)
    public int HourOfDay { get; set; }
    
    // متوسط زحام المغادرة التاريخي (كثافة الرحلات المتوقعة)
    public decimal AverageCongestion { get; set; }
}
