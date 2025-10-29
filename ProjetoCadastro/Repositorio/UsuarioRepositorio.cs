using MySql.Data.MySqlClient;
using ProjetoCadastro.Models;
using System.Data;
namespace ProjetoCadastro.Repositorio
{
    public class UsuarioRepositorio
    {
        private readonly string _connectionString = "server=localhost;port=3307;database=dbProjetoCad;user=root;password=;";

        //(INSERT)
        public void Cadastrar(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();
                string sql = "INSERT INTO tbUsuarios (Nome, Email, Senha) VALUES (@Nome, @Email, @Senha)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);

                cmd.ExecuteNonQuery();
            }
        }

        //(SELECT)
        public Usuario? Login(string email, string senha)
        {
            using (var conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();
                string sql = "SELECT * FROM tbUsuarios WHERE Email=@Email AND Senha=@Senha";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Senha", senha);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Usuario
                    {
                        Id = reader.GetInt32("Id"),
                        Nome = reader.GetString("Nome"),
                        Email = reader.GetString("Email"),
                        Senha = reader.GetString("Senha")
                    };
                }

                return null; // se não encontrou o usuário
            }
        }
            
            // SELECT Listar todos os usuários
        public List<Usuario> Listar()
        {
            var lista = new List<Usuario>();

            using (var conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();
                string sql = "SELECT * FROM tbUsuarios";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Usuario
                        {
                            Id = reader.GetInt32("Id"),
                            Nome = reader.GetString("Nome"),
                            Email = reader.GetString("Email"),
                            Senha = reader.GetString("Senha")
                        });
                    }
                }
            }

            return lista;
        }

        // DELETE
        public void Deletar(int id)
        {
            using (var conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();
                string sql = "DELETE FROM tbUsuarios WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
