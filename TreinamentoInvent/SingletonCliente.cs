using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinamentoInvent
{
    public class SingletonCliente
    {
        static int id = 0;

        private static BindingList<Cliente> lista;
        
        private SingletonCliente() { }

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
            return id ;
        }
    }
}
