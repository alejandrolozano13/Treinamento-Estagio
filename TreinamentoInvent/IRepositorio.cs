using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinamentoInvent
{
    public interface IRepositorio
    {
        public BindingList<Cliente> ObterTodos();

        public void Criar(Cliente novoCliente);

        public void Atualizar(int id, Cliente cliente);

        public void Remover(int id);

        public Cliente ObterPorId(int id);

        public bool ValidaCPF(string cpf);
    }
}
