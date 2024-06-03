using API.DTOs;
using API.Models;

namespace API.Interfaces;

public interface IProdutoRepository
{
    public Task<IEnumerable<ProdutoViewModel>> GetAllAsync();
    public Task<ProdutoViewModel?> GetByIdAsync(int id);
    public Task<ProdutoViewModel?> CreateAsync(ProdutoInputModel produto);
    public Task<ProdutoViewModel?> UpdateAsync(int id, ProdutoInputModel produto);
    public Task<ProdutoViewModel?> DeleteByIdAsync(int id);
}
