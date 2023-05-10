using System.ComponentModel;
using System.Net.Mail;
using Domain.BancoDeDados;
using Domain.Modelo;

namespace Domain.Validacao
{
    public class Validacoes
    {
        BindingList<string> mensagem = new BindingList<string>();
        private readonly IRepositorio _repositorio;

        public Validacoes(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Validacoes() { }

        public bool IsCpf(string cpf)
        {
            cpf = cpf.Trim().Replace(",", "").Replace("-", "");
            if (cpf.Length != 11) return false;
            int[] somar = new int[2] { 0, 0 };
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string digito;
            for (int i = 0; i < 10; i++)
            {
                if (i < 9) somar[0] += int.Parse(cpf[i].ToString()) * multiplicador1[i];
                somar[1] += int.Parse(cpf[i].ToString()) * multiplicador2[i];
            }
            digito = ((somar[0] % 11) < 2 ? 0 : 11 - somar[0] % 11).ToString();
            digito += ((somar[1] % 11) < 2 ? 0 : 11 - somar[1] % 11).ToString();
            return cpf.EndsWith(digito);
        }



        public void ValidarCliente(Cliente cliente, bool E_ClienteEdicao)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                mensagem.Add("O nome do cliente é obrigatório");
            }

            if (string.IsNullOrEmpty(cliente.Email))
            {
                mensagem.Add("O E-Mail do cliente é obrigatório");
            }
            else
            {
                try
                {
                    var mailAddres = new MailAddress(cliente.Email);
                }
                catch (FormatException)
                {
                    mensagem.Add("O E-Mail do cliente não está correto.");
                }
            }

            if (cliente.Telefone.Length < 14 || !cliente.Telefone[3].Equals('9'))
            {
                mensagem.Add("O Telefone é inválido");
            }

            var v = new Validacoes();

            if (!v.IsCpf(cliente.Cpf))
            {
                mensagem.Add("CPF inválido");
            }

            DateTime dataSelecionada = cliente.Data;
            DateTime dataAtual = DateTime.Now;
            TimeSpan periodo = dataAtual - dataSelecionada;

            if (periodo.TotalDays < 18 * 365)
            {
                mensagem.Add("Precisa ter mais de 18 anos.");
            }

            if (!E_ClienteEdicao)
            {
                if (_repositorio.ValidaCPF(cliente.Cpf))
                {
                    mensagem.Add("CPF já existe!");
                }
            }

            if (mensagem.Count > 0)
            {
                var erro = string.Join(Environment.NewLine, mensagem);
                throw new Exception(erro);
            }
        }
    }
}
