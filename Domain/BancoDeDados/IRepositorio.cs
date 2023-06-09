﻿using Domain.Modelo;
using System.ComponentModel;

namespace Domain.BancoDeDados
{
    public interface IRepositorio
    {
        public List<Cliente> ObterTodos(string nome);

        public void Criar(Cliente novoCliente);

        public void Atualizar(Cliente cliente);

        public void Remover(int id);

        public Cliente ObterPorId(int id);

        public bool ValidaCPF(string cpf);

        public Cliente pesquisarPeloCpf(string cpf);

    }
}