using BelediyeTicketAPI.DTOs.Category;
using BelediyeTicketAPI.Interfaces;
using BelediyeTicketAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BelediyeTicketAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    // Tüm kategorileri getir
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var categories = await _service.GetAllAsync();

        var result = categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name
        });

        return Ok(result);
    }

    // Id'ye göre kategori getir
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
        var category = await _service.GetByIdAsync(id);

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

        await _service.CreateAsync(category);

        var result = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };

        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, result);
    }

    // Kategori güncelle
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        var category = await _service.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        category.Name = dto.Name;

        await _service.UpdateAsync(category);

        return NoContent();
    }

    // Kategori sil
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _service.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        await _service.DeleteAsync(category);

        return NoContent();
    }
}