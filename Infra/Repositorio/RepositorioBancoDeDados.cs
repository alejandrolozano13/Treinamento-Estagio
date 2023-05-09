using Domain.BancoDeDados;
using Domain.Modelo;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace Infra.Repositorio
{
    public class RepositorioBancoDeDados : IRepositorio
    {


        public void Atualizar(int id, Cliente clienteAntigo)
        {
            SqlConnection conexao = new SqlConnection("server=DESKTOPALEK\\MSSQLSERVER01;database=CinemaClientes;Integrated Security=SSPI;TrustServerCertificate=True;User ID=sa;Password=Sap@123");
            string sqlEditar = "UPDATE CadastroCliente SET NOME = @Nome, CPF = @CPF, TELEFONE = @Telefone, EMAIL = @Email, DATA_NASCIMENTO = @Data_Nascimento WHERE ID = @Id";

            SqlCommand comando = new SqlCommand(sqlEditar, conexao);
            comando.Parameters.AddWithValue("@Id", id);
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
            SqlConnection conexao = new SqlConnection("server=DESKTOPALEK\\MSSQLSERVER01;database=CinemaClientes;Integrated Security=SSPI;TrustServerCertificate=True;User ID=sa;Password=Sap@123");
            string sqlInsere = "INSERT INTO CadastroCliente(Nome, CPF, Telefone, EMail, Data_Nascimento) VALUES (@Nome, @CPF, @Telefone, @Email, @Data_Nascimento)";
            SqlCommand comando = new SqlCommand(sqlInsere, conexao);
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
            SqlConnection conexao = new SqlConnection("server=DESKTOPALEK\\MSSQLSERVER01;database=CinemaClientes;Integrated Security=SSPI;TrustServerCertificate=True;User ID=sa;Password=Sap@123");
            string sqlObterPorId = "SELECT * FROM CadastroCliente WHERE @ID = Id";
            conexao.Open();
            SqlCommand sqlComando = new SqlCommand(sqlObterPorId, conexao);
            sqlComando.Parameters.AddWithValue("@id", id);

            SqlDataReader leitor = sqlComando.ExecuteReader();
            Cliente clienteId = new Cliente();
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

        public BindingList<Cliente> ObterTodos()
        {
            var listaDeClientes = new BindingList<Cliente>();
            SqlConnection conexao = new SqlConnection("server=DESKTOPALEK\\MSSQLSERVER01;database=CinemaClientes;Integrated Security=SSPI;TrustServerCertificate=True;User ID=sa;Password=Sap@123");
            string sqlMostrarTodos = "SELECT * FROM CadastroCliente";
            conexao.Open();
            SqlCommand comando = new SqlCommand(sqlMostrarTodos, conexao);

            SqlDataReader leitor = comando.ExecuteReader();

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
            SqlConnection conexao = new SqlConnection("server=DESKTOPALEK\\MSSQLSERVER01;database=CinemaClientes;Integrated Security=SSPI;TrustServerCertificate=True;User ID=sa;Password=Sap@123");

            string sqlExcluir = "DELETE FROM CadastroCliente where @id = id";
            conexao.Open();
            SqlCommand comando = new SqlCommand(sqlExcluir, conexao);
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public bool ValidaCPF(string cpf)
        {
            SqlConnection conexao = new SqlConnection("server=DESKTOPALEK\\MSSQLSERVER01;database=CinemaClientes;Integrated Security=SSPI;TrustServerCertificate=True;User ID=sa;Password=Sap@123");

            string query = "SELECT * FROM CadastroCliente WHERE @CPF = Cpf";
            SqlCommand comandoSql = new SqlCommand(query, conexao);
            comandoSql.Parameters.AddWithValue("@CPF", cpf);
            conexao.Open();

            var dataReader = comandoSql.ExecuteReader();
            var existe = dataReader.Cast<DbDataRecord>().Any();
            dataReader.Close();

            return existe;
        }
    }
}
