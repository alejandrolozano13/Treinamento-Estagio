using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Windows.Forms.VisualStyles;

namespace TreinamentoInvent
{
    public class RepositorioBancoDeDados : IRepositorio
    {
        SqlConnection conexao = new SqlConnection("server=DESKTOPALEK\\MSSQLSERVER01;database=CinemaClientes;User ID=sa;Password=Sap@123");
        
        string sqlInsere = "INSERT INTO CadastroCliente(Nome, CPF, Telefone, EMail, Data_Nascimento)" +
                " VALUES (@Nome, @CPF, @Telefone, @Email, @Data_Nascimento)";

        string sqlMostrarTodos = "SELECT * FROM CadastroCliente";

        string sqlExcluir = "DELETE FROM CadastroCliente where @id = id";

        static int identidade;

        public void Atualizar(int id, Cliente cliente)
        {
            throw new NotImplementedException();
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

            identidade++;

            novoCliente.Id = identidade;

            SingletonCliente.Lista().Add(novoCliente);

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

            while(leitor.Read())
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
                identidade = cliente.Id;
            }
            conexao.Close();
            return SingletonCliente.Lista();
        }

        public void Remover(int id)
        {
            conexao.Open();
            SqlCommand comando = new SqlCommand(sqlExcluir, conexao);
            comando.Parameters.AddWithValue("@id", id);

            var ClienteRemover = ObterPorId(id);
            SingletonCliente.Lista().Remove(ClienteRemover);

            comando.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
