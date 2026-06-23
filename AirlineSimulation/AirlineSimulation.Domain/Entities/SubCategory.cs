namespace AirlineSimulation.Domain.Entities;

public class SubCategory
{
    public int SubCategoryId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Category Category { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
