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
            
            TelaCadastro2 t2 = new TelaCadastro2(clientes);
            t2.ShowDialog();
            DataGridView.DataSource = null;
            DataGridView.DataSource = clientes;
        }
    }
}