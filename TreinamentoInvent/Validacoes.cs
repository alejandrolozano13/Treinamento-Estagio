﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TreinamentoInvent
{
    public class Validacoes
    {
        BindingList<string> mensagem = new BindingList<string>();
        private readonly IRepositorio _repositorio;

        public Validacoes(IRepositorio repositorio)
        {
            _repositorio= repositorio;
        }

        SqlConnection conexao = new SqlConnection("server=DESKTOPALEK\\MSSQLSERVER01;database=CinemaClientes;User ID=sa;Password=Sap@123");

        public void ValidarCliente(Cliente cliente, bool E_ClienteEdicao)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                mensagem.Add("O nome do cliente é obrigatório");
            }

            if (string.IsNullOrEmpty(cliente.Email))
            {
                mensagem.Add("O E-Mail do cliente é obrigatório");
            }
            else
            {
                try
                {
                    var mailAddres = new System.Net.Mail.MailAddress(cliente.Email);
                }
                catch (FormatException)
                {
                    mensagem.Add("O E-Mail do cliente não está correto.");
                }
            }

            if (cliente.Telefone.Length < 14 || !cliente.Telefone[3].Equals('9'))
            {
                mensagem.Add("O Telefone é inválido");
            }

            if (cliente.Cpf.Length < 14)
            {
                mensagem.Add("CPF inválido");
            }

            DateTime dataSelecionada = cliente.Data ;
            DateTime dataAtual = DateTime.Now;
            TimeSpan periodo = dataAtual - dataSelecionada;

            if (periodo.TotalDays < (18 * 365))
            {
                mensagem.Add("Precisa ter mais de 18 anos.");
            }

            if (!E_ClienteEdicao)
            {
                if (_repositorio.ValidaCPF(cliente.Cpf))
                {
                    mensagem.Add("CPF já existe!");
                }
            }
            
            if (mensagem.Count > 0)
            {
                var erro = string.Join(Environment.NewLine, mensagem);
                throw new Exception(erro);
            }
        }
    }
}
