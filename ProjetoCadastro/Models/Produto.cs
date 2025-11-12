namespace ProjetoCadastro.Models
{
    public class Produto
    {
       
        //CRIANDO O ENCAPSULAMENTO DO OBJETO COM GET E SET
        public int Id { get; set; } //acessores 
        public required string Nome { get; set; }
        public string? Descricao { get; set; }  
        // Ao usar ?, você está explicitamente dizendo que a propriedade pode intencionalmente ter um valor nulo.
        public decimal Preco { get; set; }
        public decimal Quantidade { get; set; }
    }
}
