using System.ComponentModel;

namespace TreinamentoInvent
{
    public partial class TelaCadastro1 : Form
    {
        public TelaCadastro1()
        {
            InitializeComponent();
        }

        int id = 0;
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            bool eClienteParaEdicao = false;
            TelaCadastro2 t2 = new TelaCadastro2(SingletonCliente.Lista(),eClienteParaEdicao, id);
            t2.ShowDialog();
            DataGridView.DataSource = null;
            DataGridView.DataSource = SingletonCliente.Lista();
        }

        private void btnEditar_click(object sender, EventArgs e)
        {
            bool eClienteParaEdicao = true;

            if (DataGridView.SelectedRows.Count == 1)
            {
                var clienteSelecionado = (Cliente)DataGridView.SelectedRows[0].DataBoundItem;
                TelaCadastro2 t2 = new TelaCadastro2(SingletonCliente.Lista(), eClienteParaEdicao, clienteSelecionado.Id);
                t2.ShowDialog();
                DataGridView.DataSource = null;
                DataGridView.DataSource = SingletonCliente.Lista();
            }
            else
            {
                MessageBox.Show("NENHUM CLIENTE SELECIONADO", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (DataGridView.SelectedRows.Count == 1)
            {
                var clienteSelecionado = (Cliente)DataGridView.SelectedRows[0].DataBoundItem;

                foreach (Cliente cliente in SingletonCliente.Lista().ToList())
                {
                    if (cliente.Id == clienteSelecionado.Id)
                    {
                        SingletonCliente.Lista().Remove(cliente);
                    }
                }
            }
            else
            {
                MessageBox.Show("NENHUM CLIENTE SELECIONADO", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}