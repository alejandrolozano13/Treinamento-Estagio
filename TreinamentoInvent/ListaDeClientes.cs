using System.ComponentModel;

namespace TreinamentoInvent
{
    public partial class ListaDeClientes : Form
    {
        public ListaDeClientes()
        {
            InitializeComponent();
            SingletonCliente.CriarLista();
        }

        int id = 0;
        private void AoClicarEmAdicionar(object sender, EventArgs e)
        {
            var repositorio = new Repositorio();
            var eClienteParaEdicao = false;
            var telaDeCadastro = new CadastroDeClientes(eClienteParaEdicao, id);
            telaDeCadastro.ShowDialog();
            DataGridView.DataSource = null;
            DataGridView.DataSource = repositorio.ObterTodos();
        }

        private void AoClicarEmEditar(object sender, EventArgs e)
        {
            var eClienteParaEdicao = true;

            if (DataGridView.SelectedRows.Count == 1)
            {
                var repositorio = new Repositorio();
                var clienteSelecionado = (Cliente)DataGridView.SelectedRows[0].DataBoundItem;
                var telaDeCadastro = new CadastroDeClientes( eClienteParaEdicao, clienteSelecionado.Id);
                telaDeCadastro.ShowDialog();
                DataGridView.DataSource = null;
                DataGridView.DataSource = repositorio.ObterTodos();
            }
            else
            {
                MessageBox.Show("NENHUM CLIENTE SELECIONADO", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AoClicarEmRemover(object sender, EventArgs e)
        {
            if (DataGridView.SelectedRows.Count == 1)
            {
                var clienteSelecionado = (Cliente)DataGridView.SelectedRows[0].DataBoundItem;

                var repositorio = new Repositorio();
                repositorio.Remover(clienteSelecionado.Id);
            }
            else
            {
                MessageBox.Show("NENHUM CLIENTE SELECIONADO", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}