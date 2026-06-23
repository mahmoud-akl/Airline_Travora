namespace AirlineSimulation.Domain.Entities;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
