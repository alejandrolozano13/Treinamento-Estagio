using com.sun.security.ntlm;
using Domain.BancoDeDados;
using Domain.Modelo;
using Infra.BancoDeDados;
using LinqToDB;
using Microsoft.Graph;

namespace Infra.Repositorio
{
    public class RepositorioLinq : IRepositorio
    {
        public void Atualizar(Cliente cliente)
        {
            var bd = new ConexaoBD();

            if (cliente == null) { throw new Exception("Erro ao atualizar, cliente não existe"); }

            using var conexaoLinq2Db = bd.MinhaConexao();
                conexaoLinq2Db.Update(cliente);
        }

        public void Criar(Cliente novoCliente)
        {
            var bd = new ConexaoBD();

            using var conexaoLinq2Db = bd.MinhaConexao();
                conexaoLinq2Db.Insert(novoCliente);
        }

        public Cliente pesquisarPeloCpf(string cpf)
        {
            var bd = new ConexaoBD();
            var conexaoLinq2Db = bd.MinhaConexao();

            return conexaoLinq2Db.GetTable<Cliente>().FirstOrDefault(c => c.Cpf == cpf)
                ?? throw new Exception($"Cliente não encontrado com o CPF: [{cpf}]");
        }

        public Cliente ObterPorId(int id)
        {
            var bd = new ConexaoBD();
            var conexaoLinq2Db = bd.MinhaConexao();

            return conexaoLinq2Db.GetTable<Cliente>().FirstOrDefault(c => c.Id == id)
                ?? throw new Exception($"Cliente não encontrado no banco: [{id}]");
        }

        public List<Cliente> ObterTodos(string nome = null)
        {
            var clientes = new List<Cliente>();
            var bd = new ConexaoBD();
            var conexaoLinq2Db = bd.MinhaConexao();

            var query = from cliente in conexaoLinq2Db.GetTable<Cliente>()
                        select cliente;


            if (!string.IsNullOrWhiteSpace(nome))
            {
                query = query.Where(x => x.Nome.StartsWith(nome));
            }

            clientes = query.ToList();

            return clientes;
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
