using API.Models;

namespace API.DTOs;

public class ProdutoViewModel
{
    public ProdutoViewModel()
    {
        
    }
    public ProdutoViewModel(Produto produto)
    {
        Id = produto.Id;
        Nome = produto.Nome;
        Descricao = produto.Descricao;
        Estoque = produto.Estoque;
        CategoriaId = produto.CategoriaId;
    }

    public required int Id { get; set; }
    public required string Nome { get; set; }
    public string Descricao { get; set; } = String.Empty;
    public required int Estoque { get; set; }
    public required int CategoriaId { get; set; }
}

