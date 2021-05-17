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

        public void Insert(Telefone telefone, int idPessoa)
        {
            using (conexaoDB) 
            {
                var queryString = "INSERT INTO TELEFONE VALUES(@idPessoa,@tipoTelefone,@ddd,@numeroTelefone)";
                SqlCommand cmd = new SqlCommand(queryString, conexaoDB);
                cmd.CommandText = queryString;
                cmd.Parameters.AddWithValue("@idPessoa", idPessoa);
                cmd.Parameters.AddWithValue("@tipoTelefone", telefone.Tipo);
                cmd.Parameters.AddWithValue("@ddd", telefone.DDD);
                cmd.Parameters.AddWithValue("@numeroTelefone", telefone.Numero);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
        }

        public List<Telefone> FindById(int idPessoa)
        {
            string queryString = "SELECT * FROM TELEFONE WHERE IDPESSOA = " + idPessoa;

            List<Telefone> listaTelefone = new List<Telefone>();

            SqlCommand command = new SqlCommand(queryString, conexaoDB);

            SqlDataReader resultado = command.ExecuteReader();
            while (resultado.Read())
            {
                var telefone = new Telefone
                {
                    Id = resultado.GetInt32(0),
                    IdPessoa = resultado.GetInt32(1),
                    Tipo = resultado.GetString(2),
                    DDD = resultado.GetString(3),
                    Numero = resultado.GetString(4)
                };
                listaTelefone.Add(telefone);
            }
            return listaTelefone;
        }

        public void Update(int id, Telefone telefone)
        {
            var queryString = "UPDATE TELEFONE SET " +
                              "TIPO = (@tipo), DDD = (@ddd),NUMERO = (@numero) WHERE ID = " + id;

            using (conexaoDB)
            {
                SqlCommand cmd = new SqlCommand(queryString, conexaoDB);

                cmd.Parameters.AddWithValue("@tipo", telefone.Tipo);
                cmd.Parameters.AddWithValue("@ddd", telefone.DDD);
                cmd.Parameters.AddWithValue("@numero", telefone.Numero);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (conexaoDB)
            {
                var queryString = "DELETE FROM TELEFONE WHERE ID = " + id;
                SqlCommand cmd = new SqlCommand(queryString, conexaoDB);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
