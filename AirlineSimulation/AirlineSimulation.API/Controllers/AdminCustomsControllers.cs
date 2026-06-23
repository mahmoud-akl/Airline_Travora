using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirlineSimulation.Infrastructure.Data;
using AirlineSimulation.Application.DTOs;
using AirlineSimulation.Domain.Entities;

namespace AirlineSimulation.API.Controllers;

[ApiController]
[Route("api/admin/customs/categories")]
public class AdminCategoriesController : ControllerBase
{
    private readonly AirlineDbContext _context;

    public AdminCategoriesController(AirlineDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAll()
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

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return Ok(new CategoryDto
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            SubCategoriesCount = 0
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

[ApiController]
[Route("api/admin/customs/subcategories")]
public class AdminSubCategoriesController : ControllerBase
{
    private readonly AirlineDbContext _context;

    public AdminSubCategoriesController(AirlineDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<SubCategoryDto>>> GetAll()
    {
        var subCategories = await _context.SubCategories
            .Include(s => s.Category)
            .Include(s => s.Products)
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

    [HttpPost]
    public async Task<ActionResult<SubCategoryDto>> Create([FromBody] CreateSubCategoryDto dto)
    {
        var subCategory = new SubCategory
        {
            CategoryId = dto.CategoryId,
            Name = dto.Name
        };

        _context.SubCategories.Add(subCategory);
        await _context.SaveChangesAsync();

        var category = await _context.Categories.FindAsync(dto.CategoryId);

        return Ok(new SubCategoryDto
        {
            SubCategoryId = subCategory.SubCategoryId,
            CategoryId = subCategory.CategoryId,
            Name = subCategory.Name,
            CategoryName = category?.Name ?? "",
            ProductsCount = 0
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var subCategory = await _context.SubCategories.FindAsync(id);
        if (subCategory == null) return NotFound();

        _context.SubCategories.Remove(subCategory);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

[ApiController]
[Route("api/admin/customs/products")]
public class AdminProductsController : ControllerBase
{
    private readonly AirlineDbContext _context;

    public AdminProductsController(AirlineDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAll()
    {
        var products = await _context.Products
            .Include(p => p.SubCategory)
            .ThenInclude(s => s.Category)
            .Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                SubCategoryId = p.SubCategoryId,
                Name = p.Name,
                CustomsRate = p.CustomsRate,
                SubCategoryName = p.SubCategory.Name,
                CategoryName = p.SubCategory.Category.Name
            })
            .ToListAsync();

        return Ok(products);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<ProductDto>>> Search([FromQuery] string q)
    {
        var products = await _context.Products
            .Include(p => p.SubCategory)
            .ThenInclude(s => s.Category)
            .Where(p => p.Name.Contains(q))
            .Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                SubCategoryId = p.SubCategoryId,
                Name = p.Name,
                CustomsRate = p.CustomsRate,
                SubCategoryName = p.SubCategory.Name,
                CategoryName = p.SubCategory.Category.Name
            })
            .ToListAsync();

        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto)
    {
        var product = new Product
        {
            SubCategoryId = dto.SubCategoryId,
            Name = dto.Name,
            CustomsRate = dto.CustomsRate
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var subCategory = await _context.SubCategories
            .Include(s => s.Category)
            .FirstOrDefaultAsync(s => s.SubCategoryId == dto.SubCategoryId);

        return Ok(new ProductDto
        {
            ProductId = product.ProductId,
            SubCategoryId = product.SubCategoryId,
            Name = product.Name,
            CustomsRate = product.CustomsRate,
            SubCategoryName = subCategory?.Name ?? "",
            CategoryName = subCategory?.Category.Name ?? ""
        });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> Update(int id, [FromBody] UpdateProductDto dto)
    {
        var product = await _context.Products
            .Include(p => p.SubCategory)
            .ThenInclude(s => s.Category)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null) return NotFound();

        if (dto.Name != null) product.Name = dto.Name;
        if (dto.CustomsRate.HasValue) product.CustomsRate = dto.CustomsRate.Value;
        product.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new ProductDto
        {
            ProductId = product.ProductId,
            SubCategoryId = product.SubCategoryId,
            Name = product.Name,
            CustomsRate = product.CustomsRate,
            SubCategoryName = product.SubCategory.Name,
            CategoryName = product.SubCategory.Category.Name
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
