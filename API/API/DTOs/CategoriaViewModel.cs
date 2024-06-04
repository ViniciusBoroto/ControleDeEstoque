using API.Models;

namespace API.DTOs;

public class CategoriaViewModel
{
    public CategoriaViewModel(Categoria categoria)
    {
        Id = categoria.Id;
        Nome = categoria.Nome;
        Descricao = categoria.Descricao;
    }

    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
}
