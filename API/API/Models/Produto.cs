namespace API.Models;

public class Produto
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public string Descricao { get; set; } = String.Empty;
    public required Categoria Categoria { get; set; }
    public int CategoriaId { get; set; }

}
