using MySql.Data.MySqlClient;
using System.Data;
using ProjetoCadastro.Models;
namespace ProjetoCadastro.Repositorio
{
    public class ProdutoRepositorio
    {
        private readonly string _connectionString;

        public ProdutoRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySQLConnection") 
                ?? throw new InvalidOperationException("Connection string 'MySQLConnection' not found.");
        }


        public void Cadastrar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();
                string sql = "INSERT INTO tbProdutos (Nome, Descricao, Preco, Quantidade) VALUES (@Nome, @Descricao, @Preco, @Quantidade)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);

                cmd.ExecuteNonQuery();
            }
        }

        //(SELECT)
        public List<Produto> Listar()
        {
            List<Produto> lista = new List<Produto>();

            using (var conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();
                string sql = "SELECT * FROM tbProdutos";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Produto
                    {
                        Id = reader.GetInt32("Id"),
                        Nome = reader.GetString("Nome"),
                        Descricao = reader.IsDBNull("Descricao") ? "" : reader.GetString("Descricao"),
                        Preco = reader.GetDecimal("Preco"),
                        Quantidade = reader.GetInt32("Quantidade")
                    });
                }
            }

            return lista;
        }

        //(UPDATE)
        public void Atualizar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();
                string sql = "UPDATE tbProdutos SET Nome=@Nome, Descricao=@Descricao, Preco=@Preco, Quantidade=@Quantidade WHERE Id=@Id";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
                cmd.Parameters.AddWithValue("@Id", produto.Id);

                cmd.ExecuteNonQuery();
            }
        }

        //(DELETE)
        public void Deletar(int id)
        {
            using (var conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();
                string sql = "DELETE FROM tbProdutos WHERE Id=@Id";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

    }
}