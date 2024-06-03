using API.Models;

namespace API.Interfaces;

public interface ICategoriaRepository
{
    public Task<IEnumerable<Categoria>> GetAllAsync();
    public Task<Categoria?> GetByIdAsync(int id);
    public Task<Categoria?> UpdateAsync(Categoria categoria);
    public Task<Categoria?> DeleteByIdAsync(int id);
}
