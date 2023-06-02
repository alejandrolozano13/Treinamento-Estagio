using Domain.BancoDeDados;
using Domain.Modelo;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Infra.Repositorio
{
    public class RepositorioBancoDeDados : IRepositorio
    {
        public void Atualizar(Cliente clienteAntigo)
        {
            var conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings
            ["CinemaConexao"].ToString());

            var sqlEditar = "UPDATE CadastroCliente SET NOME = @Nome, CPF = @CPF, TELEFONE = @Telefone, EMAIL = @Email, DATA_NASCIMENTO = @Data_Nascimento WHERE ID = @Id";

            SqlCommand comando = new SqlCommand(sqlEditar, conexao);
            comando.Parameters.AddWithValue("@Id", clienteAntigo.Id);
            comando.Parameters.AddWithValue("@Nome", clienteAntigo.Nome);
            comando.Parameters.AddWithValue("@CPF", clienteAntigo.Cpf);
            comando.Parameters.AddWithValue("@Telefone", clienteAntigo.Telefone);
            comando.Parameters.AddWithValue("@Email", clienteAntigo.Email);
            comando.Parameters.AddWithValue("@Data_Nascimento", clienteAntigo.Data);

            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();

        }

        public void Criar(Cliente novoCliente)
        {
            var conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings
            ["CinemaConexao"].ToString());

            var sqlInsere = "INSERT INTO CadastroCliente(Nome, CPF, Telefone, EMail, Data_Nascimento) VALUES (@Nome, @CPF, @Telefone, @Email, @Data_Nascimento)";

            var comando = new SqlCommand(sqlInsere, conexao);
            comando.Parameters.Add(new SqlParameter("@Nome", novoCliente.Nome));
            comando.Parameters.Add(new SqlParameter("@CPF", novoCliente.Cpf));
            comando.Parameters.Add(new SqlParameter("@Telefone", novoCliente.Telefone));
            comando.Parameters.Add(new SqlParameter("@EMail", novoCliente.Email));
            comando.Parameters.Add(new SqlParameter("@Data_Nascimento", novoCliente.Data));

            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public Cliente ObterPorId(int id)
        {
            var conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings
            ["CinemaConexao"].ToString());

            var sqlObterPorId = "SELECT * FROM CadastroCliente WHERE @ID = Id";

            var sqlComando = new SqlCommand(sqlObterPorId, conexao);
            sqlComando.Parameters.AddWithValue("@id", id);

            conexao.Open();
            var leitor = sqlComando.ExecuteReader();
            var clienteId = new Cliente();

            while (leitor.Read())
            {
                Cliente cliente = new Cliente()
                {
                    Id = (int)leitor.GetInt64(0),
                    Nome = leitor.GetString(1),
                    Cpf = leitor.GetString(2),
                    Telefone = leitor.GetString(3),
                    Email = leitor.GetString(4),
                    Data = leitor.GetDateTime(5)
                };
                clienteId = cliente;
            }
            conexao.Close();
            return clienteId;
        }

        public List<Cliente> ObterTodos(string? nome)
        {
            var conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings
            ["CinemaConexao"].ToString());

            var listaDeClientes = new List<Cliente>();
            var sqlMostrarTodos = "SELECT * FROM CadastroCliente";

            conexao.Open();
            var comando = new SqlCommand(sqlMostrarTodos, conexao);
            var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                Cliente cliente = new Cliente()
                {
                    Id = (int)leitor.GetInt64(0),
                    Nome = leitor.GetString(1),
                    Cpf = leitor.GetString(2),
                    Telefone = leitor.GetString(3),
                    Email = leitor.GetString(4),
                    Data = leitor.GetDateTime(5)
                };
                listaDeClientes.Add(cliente);
            }
            conexao.Close();

            return listaDeClientes;
        }

        public void Remover(int id)
        {
            var conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings
            ["CinemaConexao"].ToString());

            var sqlExcluir = "DELETE FROM CadastroCliente where @id = id";

            conexao.Open();
            var comando = new SqlCommand(sqlExcluir, conexao);
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public bool ValidaCPF(string cpf)
        {
            var conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings
            ["CinemaConexao"].ToString());

            var query = "SELECT * FROM CadastroCliente WHERE @CPF = Cpf";

            var comandoSql = new SqlCommand(query, conexao);
            comandoSql.Parameters.AddWithValue("@CPF", cpf);
            
            conexao.Open();
            var dataReader = comandoSql.ExecuteReader();
            var existe = dataReader.Cast<DbDataRecord>().Any();
            dataReader.Close();

            return existe;
        }
    }
}
