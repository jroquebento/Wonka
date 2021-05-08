using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wonka.Models;

namespace Wonka.Repositorio
{
    public class RepositorioDocumento
    {
        private SqlConnection conexaoDB;
        public RepositorioDocumento()
        {
            conexaoDB = new RepositorioConexaoDB().GetConnection();
        }

        public void Insert(List<Documento> documento, int idPessoa, SqlCommand cmd)
        {
            var queryString = "INSERT INTO DOCUMENTO VALUES(@idPessoa,@tipoDocumento,@numeroDocumento)";
            
            foreach (var item in documento)
            {                
                cmd.CommandText = queryString;
                cmd.Parameters.AddWithValue("@idPessoa", idPessoa);
                cmd.Parameters.AddWithValue("@tipoDocumento", item.Tipo);
                cmd.Parameters.AddWithValue("@numeroDocumento", item.Numero);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
        }
    }
}
