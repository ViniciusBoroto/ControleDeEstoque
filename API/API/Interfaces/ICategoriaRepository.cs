using API.DTOs;
using API.Models;

namespace API.Interfaces;

public interface ICategoriaRepository
{
    public Task<IEnumerable<CategoriaViewModel>> GetAllAsync();
    public Task<CategoriaViewModel?> GetByIdAsync(int id);
    public Task<IEnumerable<ProdutoViewModel>> GetByCategoriaAsync(int categoriaId);
    public Task<CategoriaViewModel> CreateAsync(CategoriaInputModel categoria);
    public Task<CategoriaViewModel?> UpdateAsync(int id, CategoriaInputModel categoria);
    public Task<CategoriaViewModel?> DeleteByIdAsync(int id);
}
