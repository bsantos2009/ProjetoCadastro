using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using ProjetoCadastro.Models;
using System.Data;
namespace ProjetoCadastro.Repositorio
{
    public class UsuarioRepositorio
    {
        private readonly string _connectionString = "server=localhost;port=3307;database=dbProjetoCad;user=root;password=1692b;";

        //(INSERT)
        // Método para cadastrar um novo cliente no banco de dados
        public void Cadastrar(Usuario usuario)
        {
            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_connectionString))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                string sql = "INSERT INTO tbUsuarios (Nome, Email, Senha) VALUES (@Nome, @Email, @Senha)";
                // Cria um novo comando SQL para inserir dados na tabela 'cliente'
                MySqlCommand cmd = new MySqlCommand(sql, conexao);


                // Adiciona um parâmetro para Nome, Email e Senha, definindo tipo e valor de cada um
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);

                // Executa o comando SQL de inserção e retorna o número de linhas afetadas
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

        public Usuario ObterUsuarioPorId(int Codigo)
        {
            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_connectionString))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                // Cria um novo comando SQL para selecionar um registro da tabela 'cliente' com base no código
                MySqlCommand cmd = new MySqlCommand("SELECT * from tbUsuarios where Id=@codigo ", conexao);

                // Adiciona um parâmetro para o código a ser buscado, definindo seu tipo e valor
                cmd.Parameters.AddWithValue("@codigo", Codigo);

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return new Usuario
                        {
                            // ?. evita erro se o valor for nulo no banco (não chama ToString em null)
                            // ?? define "" se o resultado for nulo, garantindo que nunca seja null
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = dr["Nome"]?.ToString() ?? string.Empty,
                            Email = dr["Email"]?.ToString() ?? string.Empty,
                            Senha = dr["Senha"]?.ToString() ?? string.Empty
                        };
                    }
                }
            }

            return null;
        }
        //(UPDATE)
        // Método para Editar (atualizar) os dados de um cliente existente no banco de dados
        public void Atualizar(Usuario usuario)
        {
            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_connectionString))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                string sql = "UPDATE tbUsuarios SET Nome=@Nome, Email=@Email, Senha=@Senha WHERE Id=@Id";
                // Cria um novo comando SQL para atualizar dados na tabela 'tbUsuarios' com base no código
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                // Adiciona um parâmetro para o Nome,Email,Senha e Id do cliente a ser atualizado, definindo seu tipo e valor
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@Id", usuario.Id);

                cmd.ExecuteNonQuery();
            }
        }

        // DELETE
        // Método para excluir um cliente do banco de dados pelo seu código (ID)
        public void Deletar(int id)
        {
            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_connectionString))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                // Cria um novo comando SQL para deletar um registro da tabela 'cliente' com base no código
                string sql = "DELETE FROM tbUsuarios WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                // Adiciona um parâmetro para o código a ser excluído, definindo seu tipo e valor
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
