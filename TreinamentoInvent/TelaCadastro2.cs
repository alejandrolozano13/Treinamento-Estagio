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
    public partial class TelaCadastro2 : Form
    {
        private int idAtual, contador;
        private bool _eClienteParaEdicao = false;
        private DateTime DataZerada;
        public TelaCadastro2(BindingList<Cliente> clientes, bool eClienteParaEdicao, int id)
        {
            InitializeComponent();
            idAtual = id;

            if (eClienteParaEdicao)
            {
                foreach (Cliente cliente in clientes)
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
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // cancela o evento
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!_eClienteParaEdicao)
            {
                Cliente cliente = new Cliente();
                TelaCadastro1 t1 = new TelaCadastro1();
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
                    SingletonCliente.Lista().Add(cliente);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                foreach (Cliente cliente in SingletonCliente.Lista().ToList())
                {
                    if (cliente.Id == idAtual)
                    {
                        cliente.Nome = txtNome.Text;
                        cliente.Cpf = txtCpf.Text;
                        cliente.Email = txtEmail.Text;
                        cliente.Telefone = txtTelefone.Text;
                        cliente.Data = txtData.Value;
                        cliente.Id = idAtual;
                    }
                }
                Close();
            }
        }
    }    
}
