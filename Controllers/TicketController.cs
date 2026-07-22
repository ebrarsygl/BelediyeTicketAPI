using BelediyeTicketAPI.DTOs.Ticket;
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

    public TicketController(
        ITicketService ticketService,
        ICategoryService categoryService)
    {
        _ticketService = ticketService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetTickets()
    {
        var tickets = await _ticketService.GetAllAsync();

        var result = tickets.Select(t => new TicketDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status,
            CreatedDate = t.CreatedDate,
            CategoryId = t.CategoryId,
            CategoryName = t.Category!.Name
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDto>> GetTicket(int id)
    {
        var ticket = await _ticketService.GetByIdAsync(id);

        if (ticket == null)
        {
            return NotFound();
        }

        var result = new TicketDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Status = ticket.Status,
            CreatedDate = ticket.CreatedDate,
            CategoryId = ticket.CategoryId,
            CategoryName = ticket.Category!.Name
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TicketDto>> CreateTicket(CreateTicketDto dto)
    {
        var category = await _categoryService.GetByIdAsync(dto.CategoryId);

        if (category == null)
        {
            return BadRequest("Kategori bulunamadı.");
        }

        var ticket = new Ticket
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = "Beklemede",
            CreatedDate = DateTime.UtcNow,
            CategoryId = dto.CategoryId
        };

        await _ticketService.CreateAsync(ticket);

        var result = new TicketDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Status = ticket.Status,
            CreatedDate = ticket.CreatedDate,
            CategoryId = ticket.CategoryId,
            CategoryName = category.Name
        };

        return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTicket(int id, UpdateTicketDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        var ticket = await _ticketService.GetByIdAsync(id);

        if (ticket == null)
        {
            return NotFound();
        }

        var category = await _categoryService.GetByIdAsync(dto.CategoryId);

        if (category == null)
        {
            return BadRequest("Kategori bulunamadı.");
        }

        ticket.Title = dto.Title;
        ticket.Description = dto.Description;
        ticket.Status = dto.Status;
        ticket.CategoryId = dto.CategoryId;

        await _ticketService.UpdateAsync(ticket);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var ticket = await _ticketService.GetByIdAsync(id);

        if (ticket == null)
        {
            return NotFound();
        }

        await _ticketService.DeleteAsync(ticket);

        return NoContent();
    }
}