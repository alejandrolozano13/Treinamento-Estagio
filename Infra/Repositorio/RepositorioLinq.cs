using Domain.BancoDeDados;
using Domain.Modelo;
using Infra.BancoDeDados;
using LinqToDB;
using System.ComponentModel;

namespace Infra.Repositorio
{
    public class RepositorioLinq : IRepositorio
    {
        public void Atualizar(int id, Cliente cliente)
        {
            ConexaoBD bd = new();

            using var conexaoLinq2Db = bd.MinhaConexao();
                conexaoLinq2Db.Update(cliente);
        }

        public void Criar(Cliente novoCliente)
        {
            ConexaoBD bd = new();

            using var conexaoLinq2Db = bd.MinhaConexao();
                conexaoLinq2Db.Insert(novoCliente);
        }



        public Cliente ObterPorId(int id)
        {
            try
            {
                ConexaoBD bd = new();
                var conexaoLinq2Db = bd.MinhaConexao();

                return conexaoLinq2Db.GetTable<Cliente>().FirstOrDefault(c => c.Id == id)
                    ?? throw new Exception($"Erro ao obter cliente com id: [{id}]");
            }
            catch(Exception ex)
            {
                throw new Exception("Erro ao obter Id", ex);
            }
        }

        public BindingList<Cliente> ObterTodos()
        {
            ConexaoBD bd = new();
            var conexaoLinq2Db = bd.MinhaConexao();

            var query = conexaoLinq2Db.GetTable<Cliente>();

            return new BindingList<Cliente>(query.ToList());
        }

        public void Remover(int id)
        {
            try
            {
                ConexaoBD bd = new();

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
            ConexaoBD bd = new();

            using var conexaoLinq2Db = bd.MinhaConexao();
                return conexaoLinq2Db.GetTable<Cliente>().Any(c => c.Cpf == cpf);
        }
    }
}
