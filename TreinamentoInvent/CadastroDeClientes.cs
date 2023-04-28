using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreinamentoInvent
{
    public partial class CadastroDeClientes : Form
    {
        private int idAtual;
        private bool _eClienteParaEdicao = false;

        private readonly IRepositorio _repositorio;
        public CadastroDeClientes(bool eClienteParaEdicao, int id, IRepositorio repositorio)
        {
            InitializeComponent();
            idAtual = id;

            if (eClienteParaEdicao)
            {
                foreach (Cliente cliente in SingletonCliente.Lista())
                {
                    if (id == cliente.Id)
                    {
                        idAtual = cliente.Id;
                        txtNome.Text = cliente.Nome;
                        txtCpf.Text = cliente.Cpf;
                        txtTelefone.Text = cliente.Telefone;
                        txtEmail.Text = cliente.Email;
                        txtData.Value = cliente.Data;
                    }
                }
            }
            _eClienteParaEdicao = eClienteParaEdicao;
            _repositorio = repositorio;
        }

        private void EntradaDeLetrasParaNome(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void AoClicarEmCancelar(object sender, EventArgs e)
        {
            Close();
        }

        private void AoClicarEmSalvar(object sender, EventArgs e)
        {
            if (!_eClienteParaEdicao)
            {
                Cliente cliente = new Cliente();
                Validacoes validacoes = new Validacoes();

                cliente.Nome = txtNome.Text;
                cliente.Email = txtEmail.Text;
                cliente.Data = txtData.Value;
                cliente.Telefone = txtTelefone.Text;
                cliente.Cpf = txtCpf.Text;

                try
                {
                    validacoes.ValidarCliente(cliente.Nome, cliente.Email, txtTelefone.Text, SingletonCliente.Lista(), cliente.Cpf, cliente.Data);
                    cliente.Id = SingletonCliente.GeraId();
                    _repositorio.Criar(cliente);
                    
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Cliente cliente = new Cliente();
                cliente.Nome = txtNome.Text;
                cliente.Email = txtEmail.Text;
                cliente.Data = txtData.Value;
                cliente.Telefone = txtTelefone.Text;
                cliente.Cpf = txtCpf.Text;
                cliente.Id = idAtual;

                _repositorio.Atualizar(idAtual, cliente);
                Close();
            }
        }
    }    
}
