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
        public void Atualizar(int id, Cliente cliente)
        {
            foreach (Cliente c in SingletonCliente.Lista().ToList())
            {
                if (c.Id == id)
                {
                    c.Id = cliente.Id;
                    c.Email = cliente.Email;
                    c.Telefone = cliente.Telefone;
                    c.Cpf = cliente.Cpf;
                    c.Data= cliente.Data;
                    c.Nome= cliente.Nome;

                    cliente = c;
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
