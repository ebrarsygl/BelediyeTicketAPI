using BelediyeTicketAPI.DTOs.Ticket;
using BelediyeTicketAPI.Data;
using BelediyeTicketAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BelediyeTicketAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TicketController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetTickets()
    {
    var tickets = await _context.Tickets
        .Include(t => t.Category)
        .Select(t => new TicketDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status,
            CreatedDate = t.CreatedDate,
            CategoryId = t.CategoryId,
            CategoryName = t.Category!.Name
        })
        .ToListAsync();

    return Ok(tickets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDto>> GetTicket(int id)
    {
    var ticket = await _context.Tickets
        .Include(t => t.Category)
        .FirstOrDefaultAsync(t => t.Id == id);

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
    var category = await _context.Categories.FindAsync(dto.CategoryId);

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

    _context.Tickets.Add(ticket);
    await _context.SaveChangesAsync();

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

    var ticket = await _context.Tickets.FindAsync(id);

    if (ticket == null)
    {
        return NotFound();
    }

    var category = await _context.Categories.FindAsync(dto.CategoryId);

    if (category == null)
    {
        return BadRequest("Kategori bulunamadı.");
    }

    ticket.Title = dto.Title;
    ticket.Description = dto.Description;
    ticket.Status = dto.Status;
    ticket.CategoryId = dto.CategoryId;

    await _context.SaveChangesAsync();

    return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
    var ticket = await _context.Tickets.FindAsync(id);

    if (ticket == null)
    {
        return NotFound();
    }

    _context.Tickets.Remove(ticket);
    await _context.SaveChangesAsync();

    return NoContent();
    }
}