using Domain.BancoDeDados;
using Domain.Modelo;
using Domain.Validacao;

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

        public CadastroDeClientes() { }

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

        public Cliente PreencherDadosCliente(string nome, string cpf, string email, string telefone, DateTime data)
        {
            Cliente cliente = new Cliente();

            cliente.Nome = nome;
            cliente.Cpf = cpf;
            cliente.Email = email;
            cliente.Telefone = telefone;
            cliente.Data = data;

            return cliente;
        }

        private void AoClicarEmSalvar(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            var cadastroClientes = new CadastroDeClientes();
            ValidadorDeCliente validacoes = new ValidadorDeCliente(_repositorio);

            if (!_eClienteParaEdicao)
            {
                bool edita = false;

                cliente = cadastroClientes.PreencherDadosCliente(txtNome.Text, txtCpf.Text, txtEmail.Text, txtTelefone.Text, txtData.Value);

                try
                {
                    validacoes.Validar(cliente, edita);
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
                bool edita = true;

                cliente = cadastroClientes.PreencherDadosCliente(txtNome.Text, txtCpf.Text, txtEmail.Text, txtTelefone.Text, txtData.Value);
                cliente.Id = idAtual;

                try
                {
                    validacoes.Validar(cliente, edita);
                    _repositorio.Atualizar(cliente);

                    Close();
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }    
}
