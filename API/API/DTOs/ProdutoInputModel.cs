namespace API.DTOs;

public class ProdutoInputModel
{
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public int Estoque { get; set; } = 0;
    public required int CategoriaId { get; set; }
}
