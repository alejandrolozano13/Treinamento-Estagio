using System.ComponentModel;

namespace TreinamentoInvent
{
    public partial class TelaCadastro1 : Form
    {

        BindingList<Cliente> clientes = new BindingList<Cliente>();

        public TelaCadastro1()
        {
            InitializeComponent();
        }

        static int id = 0;

        public int GeraId()
        {
            id++;
            return id;
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            bool eClienteParaEdicao = false;
            TelaCadastro2 t2 = new TelaCadastro2(clientes,eClienteParaEdicao, id);
            t2.ShowDialog();
            DataGridView.DataSource = null;
            DataGridView.DataSource = clientes;
        }

        private void btnEditar_click(object sender, EventArgs e)
        {
            bool eClienteParaEdicao = true;

            if (DataGridView.SelectedRows.Count == 1)
            {
                var clienteSelecionado = (Cliente)DataGridView.SelectedRows[0].DataBoundItem;
                TelaCadastro2 t2 = new TelaCadastro2(clientes, eClienteParaEdicao, clienteSelecionado.Id);
                t2.ShowDialog();
                DataGridView.DataSource = null;
                DataGridView.DataSource = clientes;
            }
            else
            {
                MessageBox.Show("NENHUM CLIENTE SELECIONADO", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}