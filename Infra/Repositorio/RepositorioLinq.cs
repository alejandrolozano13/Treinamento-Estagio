using Domain.BancoDeDados;
using Domain.Modelo;
using Infra.BancoDeDados;
using LinqToDB;
using System.ComponentModel;

namespace Infra.Repositorio
{
    public class RepositorioLinq : IRepositorio
    {
        public void Atualizar(Cliente cliente)
        {
            var bd = new ConexaoBD();

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

        public BindingList<Cliente> ObterTodos()
        {
            var bd = new ConexaoBD();
            var conexaoLinq2Db = bd.MinhaConexao();

            var query = conexaoLinq2Db.GetTable<Cliente>();

            return new BindingList<Cliente>(query.ToList());
        }

        public void Remover(int id)
        {
            try
            {
                var bd = new ConexaoBD();

                Cliente identidade = ObterPorId(id);

                using var conexaoLinqDb = bd.MinhaConexao();
                    conexaoLinqDb.Delete(identidade);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar", ex);
            }
        }

        public bool ValidaCPF(string cpf)
        {
            var bd = new ConexaoBD();

            using var conexaoLinq2Db = bd.MinhaConexao();
                return conexaoLinq2Db.GetTable<Cliente>().Any(c => c.Cpf == cpf);
        }
    }
}
