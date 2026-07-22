using AutoMapper;
using BelediyeTicketAPI.DTOs.Ticket;
using BelediyeTicketAPI.Helpers;
using BelediyeTicketAPI.Interfaces;
using BelediyeTicketAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BelediyeTicketAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public TicketController(
        ITicketService ticketService,
        ICategoryService categoryService,
        IMapper mapper)
    {
        _ticketService = ticketService;
        _categoryService = categoryService;
        _mapper = mapper;
    }

    // Tüm talepleri getir
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<TicketDto>>>> GetTickets()
    {
        var tickets = await _ticketService.GetAllAsync();

        var result = _mapper.Map<IEnumerable<TicketDto>>(tickets);

        return Ok(new ApiResponse<IEnumerable<TicketDto>>(
            true,
            "Talepler başarıyla getirildi.",
            result));
    }

    // Id'ye göre talep getir
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<TicketDto>>> GetTicket(int id)
    {
        var ticket = await _ticketService.GetByIdAsync(id);

        if (ticket == null)
        {
            return NotFound(new ApiResponse<object>(
                false,
                "Talep bulunamadı.",
                null));
        }

        var result = _mapper.Map<TicketDto>(ticket);

        return Ok(new ApiResponse<TicketDto>(
            true,
            "Talep başarıyla getirildi.",
            result));
    }

    // Yeni talep oluştur
    [HttpPost]
    public async Task<ActionResult<ApiResponse<TicketDto>>> CreateTicket(CreateTicketDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "Gönderilen veriler hatalı.",
                ModelState));
        }

        var category = await _categoryService.GetByIdAsync(dto.CategoryId);

        if (category == null)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "Kategori bulunamadı.",
                null));
        }

        var ticket = _mapper.Map<Ticket>(dto);

        ticket.Status = "Beklemede";
        ticket.CreatedDate = DateTime.UtcNow;

        await _ticketService.CreateAsync(ticket);

        var result = _mapper.Map<TicketDto>(ticket);
        result.CategoryName = category.Name;

        return CreatedAtAction(
            nameof(GetTicket),
            new { id = ticket.Id },
            new ApiResponse<TicketDto>(
                true,
                "Talep başarıyla oluşturuldu.",
                result));
    }

    // Talep güncelle
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> UpdateTicket(int id, UpdateTicketDto dto)
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

        var ticket = await _ticketService.GetByIdAsync(id);

        if (ticket == null)
        {
            return NotFound(new ApiResponse<object>(
                false,
                "Talep bulunamadı.",
                null));
        }

        var category = await _categoryService.GetByIdAsync(dto.CategoryId);

        if (category == null)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "Kategori bulunamadı.",
                null));
        }

        _mapper.Map(dto, ticket);

        await _ticketService.UpdateAsync(ticket);

        return Ok(new ApiResponse<object>(
            true,
            "Talep başarıyla güncellendi.",
            null));
    }

    // Talep sil
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteTicket(int id)
    {
        var ticket = await _ticketService.GetByIdAsync(id);

        if (ticket == null)
        {
            return NotFound(new ApiResponse<object>(
                false,
                "Talep bulunamadı.",
                null));
        }

        await _ticketService.DeleteAsync(ticket);

        return Ok(new ApiResponse<object>(
            true,
            "Talep başarıyla silindi.",
            null));
    }
}