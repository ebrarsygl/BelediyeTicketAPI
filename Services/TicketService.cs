using BelediyeTicketAPI.Interfaces;
using BelediyeTicketAPI.Models;

namespace BelediyeTicketAPI.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _repository;

    public TicketService(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Ticket?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Ticket> CreateAsync(Ticket ticket)
    {
        return await _repository.CreateAsync(ticket);
    }

    public async Task UpdateAsync(Ticket ticket)
    {
        await _repository.UpdateAsync(ticket);
    }

    public async Task DeleteAsync(Ticket ticket)
    {
        await _repository.DeleteAsync(ticket);
    }
}