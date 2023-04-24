using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public void ValidarCliente(string nome, string email, string telefone, BindingList<Cliente>clientes, string cpf, DateTime data)
        {
            Validacoes validacoes= new Validacoes();

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

            int contador = 0;

            foreach(Cliente cliente in clientes)
            {
                if (cliente.Cpf.Equals(cpf))
                {
                    contador++;
                }
            }

            if (contador != 0)
            {
                mensagem.Add("CPF já existe!");
            }
            else
            {
                if (cpf.Length < 14)
                {
                    mensagem.Add("CPF inválido");
                }
            }

            DateTime dataSelecionada = data;
            DateTime dataAtual = DateTime.Now;
            TimeSpan periodo = dataAtual - dataSelecionada;

            if (periodo.TotalDays < (18 * 365))
            {
                mensagem.Add("Precisa ter mais de 18 anos.");
            }

            if (mensagem.Count > 0)
            {
                var erro = string.Join(Environment.NewLine, mensagem);
                throw new Exception(erro);
            }
        }
    }
}
