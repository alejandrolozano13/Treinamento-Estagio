using Domain.BancoDeDados;
using Domain.Modelo;
using Infra.BancoDeDados;
using LinqToDB;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio
{
    public class RepositorioLinq : IRepositorio
    {
        public void Atualizar(int id, Cliente cliente)
        {
            ConexaoBD bd = new ConexaoBD();

            using (var conexaoLinq2Db = bd.MinhaConexao())
            {
                conexaoLinq2Db.Update(cliente);
            }
        }

        public void Criar(Cliente novoCliente)
        {
            ConexaoBD bd = new ConexaoBD();

            using (var conexaoLinq2Db = bd.MinhaConexao())
            {
                conexaoLinq2Db.Insert(novoCliente);
            }
        }


        public Cliente ObterPorId(int id)
        {
            ConexaoBD bd = new ConexaoBD();

            var conexaoLinq2Db = bd.MinhaConexao();

            return conexaoLinq2Db.GetTable<Cliente>().FirstOrDefault(c => c.Id == id);
        }

        public BindingList<Cliente> ObterTodos()
        {
            ConexaoBD bd = new ConexaoBD();
            var conexaoLinq2Db = bd.MinhaConexao();

            var query = from c in conexaoLinq2Db.GetTable<Cliente>()
                        select c;

            var listaDeClientes = new BindingList<Cliente>(query.ToList());
            return listaDeClientes;
        }

        public void Remover(int id)
        {
            ConexaoBD bd = new ConexaoBD();

            using (var conexaoLinq2Db = bd.MinhaConexao())
            {
                conexaoLinq2Db.GetTable<Cliente>().Delete(c => c.Id == id);
            }
        }

        public bool ValidaCPF(string cpf)
        {
            ConexaoBD bd = new ConexaoBD();
            bool existe = false;

            using (var conexaoLinq2Db = bd.MinhaConexao())
            {
                if (conexaoLinq2Db.GetTable<Cliente>().Count((c) => c.Cpf.Equals(cpf)) > 0)
                {
                    existe = true;
                }
            }

            return existe;
        }
    }
}
