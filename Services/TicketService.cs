using BelediyeTicketAPI.Data;
using BelediyeTicketAPI.DTOs.Ticket;
using BelediyeTicketAPI.Interfaces;
using BelediyeTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BelediyeTicketAPI.Services;

public class TicketService : ITicketService
{
    private readonly ApplicationDbContext _context;

    public TicketService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TicketDto>> GetAllAsync()
    {
        return await _context.Tickets
            .Select(t => new TicketDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                CreatedDate = t.CreatedDate,
                CategoryId = t.CategoryId,
                UserId = t.UserId
            })
            .ToListAsync();
    }

    public async Task<TicketDto?> GetByIdAsync(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null)
            return null;

        return new TicketDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Status = ticket.Status,
            CreatedDate = ticket.CreatedDate,
            CategoryId = ticket.CategoryId,
            UserId = ticket.UserId
        };
    }

    public async Task<TicketDto> CreateAsync(CreateTicketDto dto)
    {
        var ticket = new Ticket
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            CreatedDate = DateTime.UtcNow,
            CategoryId = dto.CategoryId,
            UserId = dto.UserId
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return new TicketDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Status = ticket.Status,
            CreatedDate = ticket.CreatedDate,
            CategoryId = ticket.CategoryId,
            UserId = ticket.UserId
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateTicketDto dto)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null)
            return false;

        ticket.Title = dto.Title;
        ticket.Description = dto.Description;
        ticket.Status = dto.Status;
        ticket.CategoryId = dto.CategoryId;
        ticket.UserId = dto.UserId;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null)
            return false;

        _context.Tickets.Remove(ticket);

        await _context.SaveChangesAsync();

        return true;
    }
}