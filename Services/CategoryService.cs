using BelediyeTicketAPI.Interfaces;
using BelediyeTicketAPI.Models;

namespace BelediyeTicketAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Category> CreateAsync(Category category)
    {
        return await _repository.CreateAsync(category);
    }

    public async Task UpdateAsync(Category category)
    {
        await _repository.UpdateAsync(category);
    }

    public async Task DeleteAsync(Category category)
    {
        await _repository.DeleteAsync(category);
    }
}