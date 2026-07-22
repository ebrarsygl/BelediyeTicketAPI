using AutoMapper;
using BelediyeTicketAPI.DTOs.Category;
using BelediyeTicketAPI.Helpers;
using BelediyeTicketAPI.Interfaces;
using BelediyeTicketAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BelediyeTicketAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;
    private readonly IMapper _mapper;

    public CategoryController(
        ICategoryService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // Tüm kategorileri getir
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<CategoryDto>>>> GetCategories()
    {
        var categories = await _service.GetAllAsync();

        var result = _mapper.Map<IEnumerable<CategoryDto>>(categories);

        return Ok(new ApiResponse<IEnumerable<CategoryDto>>(
            true,
            "Kategoriler başarıyla getirildi.",
            result));
    }

    // Id'ye göre kategori getir
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> GetCategory(int id)
    {
        var category = await _service.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound(new ApiResponse<object>(
                false,
                "Kategori bulunamadı.",
                null));
        }

        var result = _mapper.Map<CategoryDto>(category);

        return Ok(new ApiResponse<CategoryDto>(
            true,
            "Kategori başarıyla getirildi.",
            result));
    }

    // Yeni kategori ekle
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> CreateCategory(CreateCategoryDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "Gönderilen veriler hatalı.",
                ModelState));
        }

        var category = _mapper.Map<Category>(dto);

        await _service.CreateAsync(category);

        var result = _mapper.Map<CategoryDto>(category);

        return CreatedAtAction(
            nameof(GetCategory),
            new { id = category.Id },
            new ApiResponse<CategoryDto>(
                true,
                "Kategori başarıyla oluşturuldu.",
                result));
    }

    // Kategori güncelle
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> UpdateCategory(int id, UpdateCategoryDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "Gönderilen veriler hatalı.",
                ModelState));
        }

        if (id != dto.Id)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "Id bilgisi uyuşmuyor.",
                null));
        }

        var category = await _service.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound(new ApiResponse<object>(
                false,
                "Kategori bulunamadı.",
                null));
        }

        _mapper.Map(dto, category);

        await _service.UpdateAsync(category);

        return Ok(new ApiResponse<object>(
            true,
            "Kategori başarıyla güncellendi.",
            null));
    }

    // Kategori sil
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteCategory(int id)
    {
        var category = await _service.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound(new ApiResponse<object>(
                false,
                "Kategori bulunamadı.",
                null));
        }

        await _service.DeleteAsync(category);

        return Ok(new ApiResponse<object>(
            true,
            "Kategori başarıyla silindi.",
            null));
    }
}