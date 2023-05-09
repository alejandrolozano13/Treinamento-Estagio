using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infra.BancoDeDados
{
    public class ConexaoBD
    {
        public DataConnection MinhaConexao()
        {

            DataConnection conexao = SqlServerTools.CreateDataConnection("server=DESKTOPALEK\\MSSQLSERVER01;database=CinemaClientes;Integrated Security=SSPI;TrustServerCertificate=True;User ID=sa;Password=Sap@123");
            return conexao;
        }

    }
}
