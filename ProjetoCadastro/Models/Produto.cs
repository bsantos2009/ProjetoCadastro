namespace ProjetoCadastro.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public decimal Quantidade { get; set; }
    }
}
