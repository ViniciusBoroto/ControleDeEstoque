using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Produto> _produtos;
    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
        _produtos = context.Produtos;
    }
    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
        return await _produtos.Take(10).ToListAsync();
    }

    public async Task<Produto?> GetByIdAsync(int id)
    {
        return await _produtos.FindAsync(id);
    }

    public async Task<Produto?> UpdateAsync(Produto produto)
    {
        var produtoDB = await _produtos.FindAsync(produto.Id);
        if (produtoDB is null) return null;

        _context.Entry(produto).State =
            Microsoft.EntityFrameworkCore.EntityState.Modified;
        await _context.SaveChangesAsync();
        return produto;

    }

    public async Task<Produto?> DeleteByIdAsync(int id)
    {
        var produto = await _produtos.FindAsync(id);
        if (produto is null) return null;
        _produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return produto;

    }
}
