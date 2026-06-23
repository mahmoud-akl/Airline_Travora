using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirlineSimulation.Infrastructure.Data;
using AirlineSimulation.Application.DTOs;

namespace AirlineSimulation.API.Controllers;

[ApiController]
[Route("api/customs")]
public class CustomsController : ControllerBase
{
    private readonly AirlineDbContext _context;

    public CustomsController(AirlineDbContext context)
    {
        _context = context;
    }

    [HttpGet("lookup")]
    public async Task<ActionResult<ProductLookupDto>> LookupProduct([FromQuery] string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
            return BadRequest(new ProductLookupDto { Found = false });

        var product = await _context.Products
            .Include(p => p.SubCategory)
            .ThenInclude(s => s.Category)
            .FirstOrDefaultAsync(p => p.Name.ToLower() == productName.ToLower());

        if (product == null)
        {
            return Ok(new ProductLookupDto { Found = false });
        }

        return Ok(new ProductLookupDto
        {
            Found = true,
            Product = new ProductDetailsDto
            {
                Name = product.Name,
                CustomsRate = product.CustomsRate,
                Category = product.SubCategory.Category.Name,
                SubCategory = product.SubCategory.Name
            }
        });
    }

    [HttpGet("categories")]
    public async Task<ActionResult<List<CategoryDto>>> GetCategories()
    {
        var categories = await _context.Categories
            .Include(c => c.SubCategories)
            .Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                SubCategoriesCount = c.SubCategories.Count
            })
            .ToListAsync();

        return Ok(categories);
    }

    [HttpGet("subcategories")]
    public async Task<ActionResult<List<SubCategoryDto>>> GetSubCategories([FromQuery] int? categoryId = null)
    {
        var query = _context.SubCategories
            .Include(s => s.Category)
            .Include(s => s.Products)
            .AsQueryable();

        if (categoryId.HasValue)
        {
            query = query.Where(s => s.CategoryId == categoryId.Value);
        }

        var subCategories = await query
            .Select(s => new SubCategoryDto
            {
                SubCategoryId = s.SubCategoryId,
                CategoryId = s.CategoryId,
                Name = s.Name,
                CategoryName = s.Category.Name,
                ProductsCount = s.Products.Count
            })
            .ToListAsync();

        return Ok(subCategories);
    }

    [HttpGet("rate")]
    public async Task<ActionResult> GetProductRate([FromQuery] string categoryName, [FromQuery] string productName)
    {
        if (string.IsNullOrWhiteSpace(categoryName) || string.IsNullOrWhiteSpace(productName))
        {
            return BadRequest(new { Message = "Both categoryName and productName are required." });
        }

        var product = await _context.Products
            .Include(p => p.SubCategory)
            .ThenInclude(s => s.Category)
            .FirstOrDefaultAsync(p => 
                p.Name.ToLower() == productName.ToLower() && 
                p.SubCategory.Category.Name.ToLower() == categoryName.ToLower());

        if (product == null)
        {
            return NotFound(new { Message = "Product not found under the specified category." });
        }

        return Ok(new 
        { 
            ProductName = product.Name,
            CategoryName = product.SubCategory.Category.Name,
            Rate = product.CustomsRate 
        });
    }
}

