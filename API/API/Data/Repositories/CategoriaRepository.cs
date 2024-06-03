using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Data.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Categoria> _categorias;
    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
        _categorias = _context.Categorias;
    }

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _categorias.ToListAsync();
    }

    public async Task<Categoria?> GetByIdAsync(int id)
    {
        return await _categorias.FindAsync(id);
    }

    public async Task<Categoria?> UpdateAsync(Categoria categoria)
    {
        var categoriaDB = await _categorias.FindAsync(categoria.Id);
        if (categoriaDB is null) return null;

        _context.Entry(categoria).State =
            Microsoft.EntityFrameworkCore.EntityState.Modified;
        await _context.SaveChangesAsync();
        return categoria;

    }

    public async Task<Categoria?> DeleteByIdAsync(int id)
    {
        var categoria = await _categorias.FindAsync(id);
        if (categoria is null) return null;
        _categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return categoria;

    }
}
