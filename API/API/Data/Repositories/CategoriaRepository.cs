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
            .Select(c => new CategoriaViewModel(c))
            .ToListAsync();
    }

    public async Task<CategoriaViewModel?> GetByIdAsync(int id)
    {
        var categoria = await _categorias.FindAsync(id);
        if (categoria is null) return null;
        return new CategoriaViewModel((categoria));
    }

    public async Task<IEnumerable<ProdutoViewModel>> GetByCategoriaAsync(int categoriaId)
    {
        return await _context.Produtos
            .Where(p => p.CategoriaId == categoriaId)
            .Select(p => new ProdutoViewModel()
            {
                Id = p.Id,
                CategoriaId = p.CategoriaId,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Estoque = p.Estoque
            })
            .ToListAsync();
        
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
        return new CategoriaViewModel(categoriaParaAdicionar);
    }

    public async Task<CategoriaViewModel?> UpdateAsync(int id, CategoriaInputModel categoria)
    {
        var categoriaDB = await _categorias.FindAsync(id);
        if (categoriaDB is null) return null;

        categoriaDB.Nome = categoria.Nome;
        categoriaDB.Descricao = categoria.Descricao;

        _context.Entry(categoriaDB).State =
            Microsoft.EntityFrameworkCore.EntityState.Modified;
        await _context.SaveChangesAsync();
        return new CategoriaViewModel(categoriaDB);
    }

    public async Task<CategoriaViewModel?> DeleteByIdAsync(int id)
    {
        var categoria = await _categorias.FindAsync(id);
        if (categoria is null) return null;
        _categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return new CategoriaViewModel(categoria);
    }


}
