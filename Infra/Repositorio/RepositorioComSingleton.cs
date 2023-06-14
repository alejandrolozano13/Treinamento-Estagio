using Domain.BancoDeDados;
using Domain.Modelo;
using Infra.BancoDeDados;

namespace Infra.Repositorio
{
    public class RepositorioComSingleton : IRepositorio
    {
        public void Atualizar(Cliente ClienteAntigo)
        {
            foreach (Cliente clienteNovo in SingletonCliente.Lista().ToList())
            {
                if (clienteNovo.Id == ClienteAntigo.Id)
                {
                    clienteNovo.Id = ClienteAntigo.Id;
                    clienteNovo.Email = ClienteAntigo.Email;
                    clienteNovo.Telefone = ClienteAntigo.Telefone;
                    clienteNovo.Cpf = ClienteAntigo.Cpf;
                    clienteNovo.Data = ClienteAntigo.Data;
                    clienteNovo.Nome = ClienteAntigo.Nome;

                    ClienteAntigo = clienteNovo;
                }
            }
        }

        public void Criar(Cliente novoCliente)
        {
            SingletonCliente.Lista().Add(novoCliente);
        }

        public Cliente ObterPorId(int id)
        {
            return SingletonCliente
                .Lista()
                .ToList()
                .Find(cliente => cliente.Id == id);
        }

        public List<Cliente> ObterTodos(string? nome)
        {
            return SingletonCliente.Lista();
        }

        public void Remover(int id)
        {
            var ClienteRemover = ObterPorId(id);
            SingletonCliente.Lista().Remove(ClienteRemover);
        }

        public bool ValidaCPF(string cpf)
        {
            bool resposta = false;
            Cliente cliente = SingletonCliente
                .Lista()
                .ToList()
                .Find(cliente => cliente.Cpf.Equals(cpf));

            if (cliente.Cpf.Equals(cpf))
            {
                resposta = true;
            }
            return resposta;
        }
    }
}
