using API.DTOs;
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
    public async Task<IEnumerable<ProdutoViewModel>> GetAllAsync()
    {
        return await _produtos
            .AsNoTracking()
            .Take(15)
            .Select(p => MapToViewModel(p))
            .ToListAsync();
    }

    public async Task<ProdutoViewModel?> GetByIdAsync(int id)
    {
        var produto = await _produtos.FindAsync(id);
        if (produto is null) return null;
        return MapToViewModel(produto);
    }

    public async Task<ProdutoViewModel> CreateAsync(ProdutoInputModel produto)
    {
        var produtoParaAdicionar = new Produto
        {
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Estoque = produto.Estoque,
            CategoriaId = produto.CategoriaId,
        };
        await _produtos.AddAsync(produtoParaAdicionar);
        return MapToViewModel(produtoParaAdicionar);
    }

    public async Task<ProdutoViewModel?> UpdateAsync(int id, ProdutoInputModel produto)
    {
        var produtoDB = await _produtos.FindAsync(id);
        if (produtoDB is null) return null;

        produtoDB.Descricao = produto.Descricao;
        produtoDB.CategoriaId = produto.CategoriaId;
        produtoDB.Nome = produto.Nome;

        _context.Entry(produtoDB).State =
            Microsoft.EntityFrameworkCore.EntityState.Modified;
        await _context.SaveChangesAsync();
        return MapToViewModel(produtoDB);

    }

    public async Task<ProdutoViewModel?> DeleteByIdAsync(int id)
    {
        var produto = await _produtos.FindAsync(id);
        if (produto is null) return null;
        _produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return MapToViewModel(produto);
    }

    public ProdutoViewModel MapToViewModel(Produto produto)
    {
        return new ProdutoViewModel
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Estoque = produto.Estoque,
            Categoria = new CategoriaViewModel
            {
                Descricao = produto.Categoria.Descricao,
                Id = produto.Categoria.Id,
                Nome = produto.Categoria.Nome
            }
        };
    }
}
