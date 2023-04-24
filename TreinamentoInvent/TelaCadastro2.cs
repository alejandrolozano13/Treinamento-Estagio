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

        private BindingList<Cliente> _clientes;

        public TelaCadastro2(BindingList<Cliente>clientes)
        {
            InitializeComponent();
            _clientes= clientes;
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
            Cliente cliente = new Cliente();
            TelaCadastro1 t1 = new TelaCadastro1();
            cliente.Nome = txtNome.Text;
            cliente.Cpf = txtCpf.Text;
            cliente.Email = txtEmail.Text;
            cliente.Data = txtData.Value;
            cliente.Id = t1.GeraId();
            _clientes.Add(cliente);
            Close();
        }
    }
}
