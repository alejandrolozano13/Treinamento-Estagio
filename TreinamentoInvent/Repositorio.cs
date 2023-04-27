using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinamentoInvent
{
    public class Repositorio : IRepositorio
    {
        public void Atualizar(int id, Cliente ClienteAntigo)
        {
            foreach (Cliente clienteNovo in SingletonCliente.Lista().ToList())
            {
                if (clienteNovo.Id == id)
                {
                    clienteNovo.Id = ClienteAntigo.Id;
                    clienteNovo.Email = ClienteAntigo.Email;
                    clienteNovo.Telefone = ClienteAntigo.Telefone;
                    clienteNovo.Cpf = ClienteAntigo.Cpf;
                    clienteNovo.Data= ClienteAntigo.Data;
                    clienteNovo.Nome= ClienteAntigo.Nome;

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

        public BindingList<Cliente> ObterTodos()
        {
            return SingletonCliente.Lista();
        }

        public void Remover(int id)
        {
            var ClienteRemover = ObterPorId(id);
            SingletonCliente.Lista().Remove(ClienteRemover);
        }
    }
}
