namespace AirlineSimulation.Domain.Entities;

public class Product
{
    public int ProductId { get; set; }
    public int SubCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal CustomsRate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    public SubCategory SubCategory { get; set; } = null!;
}
