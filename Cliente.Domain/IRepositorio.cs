using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

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