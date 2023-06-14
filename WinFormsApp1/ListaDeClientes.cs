using Domain.BancoDeDados;
using Domain.Modelo;

namespace TreinamentoInvent
{
    public partial class ListaDeClientes : Form
    {
        private readonly IRepositorio _repositorio;
        public ListaDeClientes(IRepositorio repositorio)
        {
            string nome;
            InitializeComponent();
            _repositorio = repositorio;
            DataGridView.DataSource = _repositorio.ObterTodos(nome = null);
        }

        int id = 0;
        private void AoClicarEmAdicionar(object sender, EventArgs e)
        {
            string nome;
            var eClienteParaEdicao = false;
            var telaDeCadastro = new CadastroDeClientes(eClienteParaEdicao, id, _repositorio);
            telaDeCadastro.ShowDialog();
            DataGridView.DataSource = null;
            DataGridView.DataSource = _repositorio.ObterTodos(nome = null);
        }

        private void AoClicarEmEditar(object sender, EventArgs e)
        {
            var eClienteParaEdicao = true;

            if (DataGridView.SelectedRows.Count == 1)
            {
                string nome;
                var clienteSelecionado = (Cliente)DataGridView.SelectedRows[0].DataBoundItem;
                var telaDeCadastro = new CadastroDeClientes( eClienteParaEdicao, clienteSelecionado.Id, _repositorio);
                telaDeCadastro.ShowDialog();
                DataGridView.DataSource = null;
                DataGridView.DataSource = _repositorio.ObterTodos(nome = null);
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
                string nome = null;
                var clienteSelecionado = (Cliente)DataGridView.SelectedRows[0].DataBoundItem;

                _repositorio.Remover(clienteSelecionado.Id);
                DataGridView.DataSource = null;
                DataGridView.DataSource = _repositorio.ObterTodos(nome = null);
            }
            else
            {
                MessageBox.Show("NENHUM CLIENTE SELECIONADO", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}