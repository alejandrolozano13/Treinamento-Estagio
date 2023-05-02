using System.ComponentModel;

namespace TreinamentoInvent
{
    public partial class ListaDeClientes : Form
    {
        private readonly IRepositorio _repositorio;
        public ListaDeClientes(IRepositorio repositorio)
        {
            //var repositorio = new RepositorioBancoDeDados();
            _repositorio = repositorio;
            InitializeComponent();
            SingletonCliente.CriarLista();
            DataGridView.DataSource = _repositorio.ObterTodos();
        }

        int id = 0;
        private void AoClicarEmAdicionar(object sender, EventArgs e)
        {
            var eClienteParaEdicao = false;
            var telaDeCadastro = new CadastroDeClientes(eClienteParaEdicao, id, _repositorio);
            telaDeCadastro.ShowDialog();
            _repositorio.ObterTodos();
        }

        private void AoClicarEmEditar(object sender, EventArgs e)
        {
            var eClienteParaEdicao = true;

            if (DataGridView.SelectedRows.Count == 1)
            {
                var clienteSelecionado = (Cliente)DataGridView.SelectedRows[0].DataBoundItem;
                var telaDeCadastro = new CadastroDeClientes( eClienteParaEdicao, clienteSelecionado.Id, _repositorio);
                telaDeCadastro.ShowDialog();
                _repositorio.ObterTodos();
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

                _repositorio.Remover(clienteSelecionado.Id);
                _repositorio.ObterTodos();
            }
            else
            {
                MessageBox.Show("NENHUM CLIENTE SELECIONADO", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}