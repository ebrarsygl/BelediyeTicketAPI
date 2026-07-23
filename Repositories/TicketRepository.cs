using BelediyeTicketAPI.Data;
using BelediyeTicketAPI.Interfaces;
using BelediyeTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BelediyeTicketAPI.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly ApplicationDbContext _context;

    public TicketRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Ticket>> GetAllAsync(
    string? search,
    string? status,
    int? categoryId)
    {
    var query = _context.Tickets
        .Include(t => t.Category)
        .AsQueryable();

    if (!string.IsNullOrWhiteSpace(search))
    {
        query = query.Where(t =>
            t.Title.Contains(search) ||
            t.Description.Contains(search));
    }

    if (!string.IsNullOrWhiteSpace(status))
    {
        query = query.Where(t => t.Status == status);
    }

    if (categoryId.HasValue)
    {
        query = query.Where(t => t.CategoryId == categoryId.Value);
    }

    return await query.ToListAsync();
    }

    public async Task<Ticket?> GetByIdAsync(int id)
    {
        return await _context.Tickets
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Ticket> CreateAsync(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task UpdateAsync(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Ticket ticket)
    {
        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
    }
}