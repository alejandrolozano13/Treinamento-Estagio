using System.ComponentModel;
using Domain.Modelo;

namespace Infra.BancoDeDados
{
    public class SingletonCliente
    {
        static int id = 0;

        private static BindingList<Cliente> lista;

        private SingletonCliente()
        {
            CriarLista();
        }

        public static void CriarLista()
        {
            if (lista == null)
            {
                lista = new BindingList<Cliente>();
            }
        }

        public static BindingList<Cliente> Lista()
        {
            return lista;
        }

        public static int GeraId()
        {
            id++;
            return id;
        }
    }
}
