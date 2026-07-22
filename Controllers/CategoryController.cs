using BelediyeTicketAPI.DTOs.Category;
using BelediyeTicketAPI.Data;
using BelediyeTicketAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BelediyeTicketAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Tüm kategorileri getir
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
    var categories = await _context.Categories
        .Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name
        })
        .ToListAsync();

    return Ok(categories);
    }

    // Id'ye göre kategori getir
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
    var category = await _context.Categories.FindAsync(id);

    if (category == null)
    {
        return NotFound();
    }

    var result = new CategoryDto
    {
        Id = category.Id,
        Name = category.Name
    };

    return Ok(result);
    }

    // Yeni kategori ekle
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto dto)
    {
    var category = new Category
    {
        Name = dto.Name
    };

    _context.Categories.Add(category);
    await _context.SaveChangesAsync();

    var result = new CategoryDto
    {
        Id = category.Id,
        Name = category.Name
    };

    return CreatedAtAction(
        nameof(GetCategory),
        new { id = category.Id },
        result);
    }

    // Kategoriyi güncelle
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto dto)
{
    if (id != dto.Id)
    {
        return BadRequest();
    }

    var category = await _context.Categories.FindAsync(id);

    if (category == null)
    {
        return NotFound();
    }

    category.Name = dto.Name;

    await _context.SaveChangesAsync();

    return NoContent();
}

    // Kategoriyi sil
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}