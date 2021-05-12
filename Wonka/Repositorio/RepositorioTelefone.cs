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

        public List<Telefone> FindById(int idPessoa)
        {
            string queryString = "SELECT * FROM TELEFONE WHERE IDPESSOA = " + idPessoa;

            List<Telefone> listaTelefone = new List<Telefone>();

            using (conexaoDB)
            {
                SqlCommand command = new SqlCommand(queryString, conexaoDB);
                try
                {
                    SqlDataReader resultado = command.ExecuteReader();
                    while (resultado.Read())
                    {
                        var telefone = new Telefone
                        {
                            Id = resultado.GetInt32(0),
                            IdPessoa = resultado.GetInt32(1),
                            DDD = resultado.GetString(2),
                            Numero = resultado.GetString(3),
                            Tipo = resultado.GetString(4)
                        };
                        listaTelefone.Add(telefone);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return listaTelefone;
        }
    }
}
