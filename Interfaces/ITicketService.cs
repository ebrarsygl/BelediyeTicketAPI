using BelediyeTicketAPI.Models;

namespace BelediyeTicketAPI.Interfaces;

public interface ITicketService
{
    Task<IEnumerable<Ticket>> GetAllAsync();

    Task<Ticket?> GetByIdAsync(int id);

    Task<Ticket> CreateAsync(Ticket ticket);

    Task UpdateAsync(Ticket ticket);

    Task DeleteAsync(Ticket ticket);
}