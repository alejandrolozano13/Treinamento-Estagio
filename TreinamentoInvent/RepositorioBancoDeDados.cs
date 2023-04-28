using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data;

namespace TreinamentoInvent
{
    public class RepositorioBancoDeDados : IRepositorio
    {
        SqlConnection conexao = new SqlConnection("server=DESKTOPALEK\\MSSQLSERVER01;database=CinemaClientes;User ID=sa;Password=Sap@123");

        string sqlInsere = "INSERT INTO CadastroCliente(Nome, CPF, Telefone, EMail, Data_Nascimento) VALUES (@Nome, @CPF, @Telefone, @Email, @Data_Nascimento)";

        string sqlMostrarTodos = "SELECT * FROM CadastroCliente";

        string sqlExcluir = "DELETE FROM CadastroCliente where @id = id";

        string sqlEditar = "UPDATE CadastroCliente SET NOME = @Nome, CPF = @CPF, TELEFONE = @Telefone, EMAIL = @Email, DATA_NASCIMENTO = @Data_Nascimento WHERE ID = @Id";

        public void Atualizar(int id, Cliente clienteAntigo)
        {

            foreach (Cliente clienteNovo in SingletonCliente.Lista().ToList())
            {
                if (clienteNovo.Id == id)
                {
                    clienteNovo.Id = clienteAntigo.Id;
                    clienteNovo.Email = clienteAntigo.Email;
                    clienteNovo.Telefone = clienteAntigo.Telefone;
                    clienteNovo.Cpf = clienteAntigo.Cpf;
                    clienteNovo.Data = clienteAntigo.Data;
                    clienteNovo.Nome = clienteAntigo.Nome;

                    clienteAntigo = clienteNovo;
                }
            }

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
            return SingletonCliente
                .Lista()
                .ToList()
                .Find(cliente => cliente.Id == id);
        }

        public BindingList<Cliente> ObterTodos()
        {
            conexao.Open();
            SqlCommand comando = new SqlCommand(sqlMostrarTodos, conexao);
            var adaptadorSql = new SqlDataAdapter(comando);
            var dadosDoBanco = new DataTable();

            SqlDataReader leitor = comando.ExecuteReader();
            SingletonCliente.Lista().Clear();

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
                SingletonCliente.Lista().Add(cliente);
            }
            conexao.Close();
            
            return SingletonCliente.Lista();
        }

        public void Remover(int id)
        {
            conexao.Open();
            SqlCommand comando = new SqlCommand(sqlExcluir, conexao);
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
