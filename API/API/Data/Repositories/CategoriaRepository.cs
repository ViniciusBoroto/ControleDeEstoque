using API.DTOs;
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

    public async Task<IEnumerable<CategoriaViewModel>> GetAllAsync()
    {
        return await _categorias
            .AsNoTracking()
            .Select(c => MapToViewModel(c))
            .ToListAsync();
    }

    public async Task<CategoriaViewModel?> GetByIdAsync(int id)
    {
        var categoria = await _categorias.FindAsync(id);
        if (categoria is null) return null;
        return MapToViewModel(categoria);
    }

    public async Task<CategoriaViewModel> CreateAsync(CategoriaInputModel categoria)
    {
        var categoriaParaAdicionar = new Categoria
        {
            Nome = categoria.Nome,
            Descricao = categoria.Descricao
        };
        await _categorias.AddAsync(categoriaParaAdicionar);
        await _context.SaveChangesAsync();
        return MapToViewModel(categoriaParaAdicionar);
    }

    public async Task<CategoriaViewModel?> UpdateAsync(int id, CategoriaInputModel categoria)
    {
        var categoriaDB = await _categorias.FindAsync(id);
        if (categoriaDB is null) return null;

        categoriaDB.Nome = categoria.Nome;
        categoriaDB.Descricao = categoria.Descricao;

        _context.Entry(categoria).State =
            Microsoft.EntityFrameworkCore.EntityState.Modified;
        await _context.SaveChangesAsync();
        return MapToViewModel(categoriaDB);
    }

    public async Task<CategoriaViewModel?> DeleteByIdAsync(int id)
    {
        var categoria = await _categorias.FindAsync(id);
        if (categoria is null) return null;
        _categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return MapToViewModel(categoria);
    }

    private CategoriaViewModel MapToViewModel(Categoria categoria)
    {
        return new CategoriaViewModel
        {
            Id = categoria.Id,
            Nome = categoria.Nome,
            Descricao = categoria.Descricao
        };
    }
}
