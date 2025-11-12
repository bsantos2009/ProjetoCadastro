using System.ComponentModel.DataAnnotations;

namespace ProjetoCadastro.Models
{
    public class Usuario
    {
        //CRIANDO O ENCAPSULAMENTO DO OBJETO COM GET E SET
        public int Id { get; set; } //acessores 
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public required string Nome { get; set; }
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public required string Senha { get; set; }
    }
}
