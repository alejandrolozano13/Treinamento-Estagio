using System.ComponentModel;

namespace TreinamentoInvent
{
    public partial class ListaDeClientes : Form
    {
        public ListaDeClientes()
        {
            var repositorio = new RepositorioBancoDeDados();
            InitializeComponent();
            SingletonCliente.CriarLista();
            DataGridView.DataSource = repositorio.ObterTodos();
        }

        int id = 0;
        private void AoClicarEmAdicionar(object sender, EventArgs e)
        {
            var repositorio = new RepositorioBancoDeDados();
            var eClienteParaEdicao = false;
            var telaDeCadastro = new CadastroDeClientes(eClienteParaEdicao, id);
            telaDeCadastro.ShowDialog();
            repositorio.ObterTodos();
        }

        private void AoClicarEmEditar(object sender, EventArgs e)
        {
            var eClienteParaEdicao = true;

            if (DataGridView.SelectedRows.Count == 1)
            {
                var repositorio = new RepositorioBancoDeDados();
                var clienteSelecionado = (Cliente)DataGridView.SelectedRows[0].DataBoundItem;
                var telaDeCadastro = new CadastroDeClientes( eClienteParaEdicao, clienteSelecionado.Id);
                telaDeCadastro.ShowDialog();
                repositorio.ObterTodos();
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

                var repositorio = new RepositorioBancoDeDados();
                repositorio.Remover(clienteSelecionado.Id);
                repositorio.ObterTodos();
            }
            else
            {
                MessageBox.Show("NENHUM CLIENTE SELECIONADO", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}