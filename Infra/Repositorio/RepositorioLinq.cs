using Domain.BancoDeDados;
using Domain.Modelo;
using Infra.BancoDeDados;
using LinqToDB;

namespace Infra.Repositorio
{
    public class RepositorioLinq : IRepositorio
    {
        public void Atualizar(Cliente cliente)
        {
            var bd = new ConexaoBD();

            var cli = ObterPorId(cliente.Id);

            if (cli == null) { throw new Exception("Erro ao atualizar, cliente não existe"); }

            using var conexaoLinq2Db = bd.MinhaConexao();
                conexaoLinq2Db.Update(cliente);
        }

        public void Criar(Cliente novoCliente)
        {
            var bd = new ConexaoBD();

            using var conexaoLinq2Db = bd.MinhaConexao();
                conexaoLinq2Db.Insert(novoCliente);
        }



        public Cliente ObterPorId(int id)
        {
            var bd = new ConexaoBD();
            var conexaoLinq2Db = bd.MinhaConexao();

            return conexaoLinq2Db.GetTable<Cliente>().FirstOrDefault(c => c.Id == id)
                ?? throw new Exception($"Cliente não encontrado no banco: [{id}]");
        }

        public List<Cliente> ObterTodos()
        {
            var bd = new ConexaoBD();
            var conexaoLinq2Db = bd.MinhaConexao();

            var query = conexaoLinq2Db.GetTable<Cliente>().ToList();

            return query;
        }

        public void Remover(int id)
        {
            var bd = new ConexaoBD();

            var clienteComIdentidade = ObterPorId(id);

            if (clienteComIdentidade == null)
            {
                throw new Exception("Erro ao deletar, cliente não existe");
            }

            using var conexaoLinqDb = bd.MinhaConexao();
            conexaoLinqDb.Delete(clienteComIdentidade);
        }

        public bool ValidaCPF(string cpf)
        {
            var bd = new ConexaoBD();

            using var conexaoLinq2Db = bd.MinhaConexao();
                return conexaoLinq2Db.GetTable<Cliente>().Any(c => c.Cpf == cpf);
        }
    }
}
