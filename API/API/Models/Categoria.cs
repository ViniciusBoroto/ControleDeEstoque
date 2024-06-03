namespace API.Models;

public class Categoria
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public string Descricao { get; set; } = String.Empty;
    public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
