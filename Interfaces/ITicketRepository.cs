using BelediyeTicketAPI.Models;

namespace BelediyeTicketAPI.Interfaces;

public interface ITicketRepository
{
    Task<IEnumerable<Ticket>> GetAllAsync(
    string? search,
    string? status,
    int? categoryId);

    Task<Ticket?> GetByIdAsync(int id);

    Task<Ticket> CreateAsync(Ticket ticket);

    Task UpdateAsync(Ticket ticket);

    Task DeleteAsync(Ticket ticket);
}