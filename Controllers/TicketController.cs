using BelediyeTicketAPI.DTOs.Ticket;
using BelediyeTicketAPI.Helpers;
using BelediyeTicketAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BelediyeTicketAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    // GET: api/Ticket
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<TicketDto>>>> GetTickets()
    {
        var tickets = await _ticketService.GetAllAsync();

        return Ok(new ApiResponse<IEnumerable<TicketDto>>(
            true,
            "Talepler başarıyla getirildi.",
            tickets));
    }

    // GET: api/Ticket/5
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

        return Ok(new ApiResponse<TicketDto>(
            true,
            "Talep başarıyla getirildi.",
            ticket));
    }

    // POST: api/Ticket
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

        var createdTicket = await _ticketService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetTicket),
            new { id = createdTicket.Id },
            new ApiResponse<TicketDto>(
                true,
                "Talep başarıyla oluşturuldu.",
                createdTicket));
    }

    // PUT: api/Ticket/5
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

        var updated = await _ticketService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound(new ApiResponse<object>(
                false,
                "Talep bulunamadı.",
                null));
        }

        return Ok(new ApiResponse<object>(
            true,
            "Talep başarıyla güncellendi.",
            null));
    }

    // DELETE: api/Ticket/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteTicket(int id)
    {
        var deleted = await _ticketService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new ApiResponse<object>(
                false,
                "Talep bulunamadı.",
                null));
        }

        return Ok(new ApiResponse<object>(
            true,
            "Talep başarıyla silindi.",
            null));
    }
}