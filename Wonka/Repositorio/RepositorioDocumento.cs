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

        public List<Documento> FindById(int idPessoa)
        {
            string queryString = "SELECT * FROM DOCUMENTO WHERE IDPESSOA = " + idPessoa;

            List<Documento> listaDocumento = new List<Documento>();


            SqlCommand command = new SqlCommand(queryString, conexaoDB);

            SqlDataReader resultado = command.ExecuteReader();
            while (resultado.Read())
            {
                var documento = new Documento
                {
                    Id = resultado.GetInt32(0),
                    IdPessoa = resultado.GetInt32(1),
                    Tipo = resultado.GetString(2),
                    Numero = resultado.GetString(3)
                };
                listaDocumento.Add(documento);
            }
            return listaDocumento;
        }
        public void Update(int? id, Documento documento)
        {
            var queryString = "UPDATE DOCUMENTO SET" +
                              "TIPO = (@tipo), NUMERO = (@numero) WHERE ID = " + id;

            using (conexaoDB)
            {
                SqlCommand cmd = new SqlCommand(queryString, conexaoDB);
                try
                {
                    cmd.Parameters.AddWithValue("@tipo", documento.Tipo);                
                    cmd.Parameters.AddWithValue("@numero", documento.Numero);                   
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
