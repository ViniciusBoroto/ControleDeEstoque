namespace API.DTOs;

public class ProdutoViewModel
{
    public required int Id { get; set; }
    public required string Nome { get; set; }
    public string Descricao { get; set; } = String.Empty;
    public required int Estoque { get; set; }
    public required  CategoriaViewModel Categoria { get; set; }
}
