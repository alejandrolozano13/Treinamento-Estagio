using Domain.Modelo;

namespace Infra.BancoDeDados
{
    public class SingletonCliente
    {
        static int id = 0;

        private static List<Cliente> lista;

        private SingletonCliente()
        {
            CriarLista();
        }

        public static void CriarLista()
        {
            if (lista == null)
            {
                lista = new List<Cliente>();
            }
        }

        public static List<Cliente> Lista()
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
