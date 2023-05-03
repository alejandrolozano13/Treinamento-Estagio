using Microsoft.IdentityModel.Tokens;
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
            _repositorio = repositorio;
            if (eClienteParaEdicao)
            {
                Cliente cliente = _repositorio.ObterPorId(id);

                txtNome.Text = cliente.Nome;
                txtCpf.Text = cliente.Cpf;
                txtEmail.Text = cliente.Email;
                txtTelefone.Text = cliente.Telefone;
                txtData.Value = cliente.Data;

                txtCpf.Enabled = false;
                this.Text = "ATUALIZAR";
            }
            
            _eClienteParaEdicao = eClienteParaEdicao;
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
                bool edita = false;
                Cliente cliente = new Cliente();
                Validacoes validacoes = new Validacoes(_repositorio);

                cliente.Nome = txtNome.Text;
                cliente.Email = txtEmail.Text;
                cliente.Data = txtData.Value;
                cliente.Telefone = txtTelefone.Text;
                cliente.Cpf = txtCpf.Text;

                try
                {
                    validacoes.ValidarCliente(cliente, edita);
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
                //cliente.Id = idAtual;

                try
                {
                    bool edita = true;
                    Validacoes validacoes = new Validacoes(_repositorio);
                    validacoes.ValidarCliente(cliente, edita);
                    _repositorio.Atualizar(idAtual, cliente);
                    Close();
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }
    }    
}
