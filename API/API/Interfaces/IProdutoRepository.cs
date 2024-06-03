using API.Models;

namespace API.Interfaces;

public interface IProdutoRepository
{
    public Task<IEnumerable<Produto>> GetAllAsync();
    public Task<Produto?> GetByIdAsync(int id);
    public Task<Produto?> UpdateAsync(Produto produto);
    public Task<Produto?> DeleteByIdAsync(int id);
}
