using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wonka.Repositorio
{
    public class RepositorioConexaoDB
    {
        public SqlConnection GetConnection() 
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WonkaConn"].ToString());
            if (con.State == ConnectionState.Closed) 
            {
                con.Open();
            }
            return con;
        }
    }
}
