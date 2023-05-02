using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TreinamentoInvent
{
    public class Validacoes
    {
        BindingList<string> mensagem = new BindingList<string>();
        private readonly IRepositorio _repositorio;

        public Validacoes(IRepositorio repositorio)
        {
            _repositorio= repositorio;
        }

        SqlConnection conexao = new SqlConnection("server=DESKTOPALEK\\MSSQLSERVER01;database=CinemaClientes;User ID=sa;Password=Sap@123");

        public void ValidarCliente(string nome, string email, string telefone, BindingList<Cliente> clientes, string cpf, DateTime data)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                mensagem.Add("O nome do cliente é obrigatório");
            }

            if (string.IsNullOrEmpty(email))
            {
                mensagem.Add("O E-Mail do cliente é obrigatório");
            }
            else
            {
                try
                {
                    var mailAddres = new System.Net.Mail.MailAddress(email);
                }
                catch (FormatException)
                {
                    mensagem.Add("O E-Mail do cliente não está correto.");
                }
            }

            if (telefone.Length < 14 || !telefone[3].Equals('9'))
            {
                mensagem.Add("O Telefone é inválido");
            }

            if (cpf.Length < 14)
            {
                mensagem.Add("CPF inválido");
            }

            DateTime dataSelecionada = data;
            DateTime dataAtual = DateTime.Now;
            TimeSpan periodo = dataAtual - dataSelecionada;

            if (periodo.TotalDays < (18 * 365))
            {
                mensagem.Add("Precisa ter mais de 18 anos.");
            }

            string query = "SELECT * FROM CadastroCliente WHERE @CPF = Cpf";
            SqlCommand comandoSql = new SqlCommand(query, conexao);
            comandoSql.Parameters.AddWithValue("@CPF", cpf);
            conexao.Open();

            var dataReader = comandoSql.ExecuteReader();
            var existe = dataReader.Cast<DbDataRecord>().Any();
            dataReader.Close();

            if (existe)
            {
                mensagem.Add("CPF JÁ EXISTE!");
            }
            
            if (mensagem.Count > 0)
            {
                var erro = string.Join(Environment.NewLine, mensagem);
                throw new Exception(erro);
            }
        }
    }
}
