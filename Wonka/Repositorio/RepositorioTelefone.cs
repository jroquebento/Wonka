using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wonka.Models;

namespace Wonka.Repositorio
{
    public class RepositorioTelefone
    {
        private SqlConnection conexaoDB;

        public RepositorioTelefone()
        {
            conexaoDB = new RepositorioConexaoDB().GetConnection();
        }

        public void Insert(List<Telefone> telefone, int idPessoa, SqlCommand cmd)
        {
            var queryString = "INSERT INTO TELEFONE VALUES(@idPessoa,@tipoTelefone,@ddd,@numeroTelefone)";
            
            foreach (var item in telefone)
            {
                cmd.CommandText = queryString;
                cmd.Parameters.AddWithValue("@idPessoa", idPessoa);
                cmd.Parameters.AddWithValue("@tipoTelefone", item.Tipo);
                cmd.Parameters.AddWithValue("@ddd", item.DDD);
                cmd.Parameters.AddWithValue("@numeroTelefone", item.Numero);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
        }
    }
}
