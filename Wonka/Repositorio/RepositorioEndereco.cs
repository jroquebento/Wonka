using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wonka.Models;

namespace Wonka.Repositorio
{
    public class RepositorioEndereco
    {
        private SqlConnection conexaoDB;
        public RepositorioEndereco()
        {
            conexaoDB = new RepositorioConexaoDB().GetConnection();
        }
        public void Insert(Endereco endereco, int idPessoa, SqlCommand cmd)
        {         
            var queryString = "INSERT INTO ENDERECO VALUES(@idPessoa,@tipo,@cep,@logradouro," +
             "@numero,@bairro,@cidade,@uf)";
            cmd.CommandText = queryString;
            cmd.Parameters.AddWithValue("@idPessoa", idPessoa);
            cmd.Parameters.AddWithValue("@tipo", endereco.Tipo);
            cmd.Parameters.AddWithValue("@cep", endereco.CEP);
            cmd.Parameters.AddWithValue("@logradouro", endereco.Logradouro);
            cmd.Parameters.AddWithValue("@numero", endereco.Numero);
            cmd.Parameters.AddWithValue("@bairro", endereco.Bairro);
            cmd.Parameters.AddWithValue("@cidade", endereco.Cidade);
            cmd.Parameters.AddWithValue("@uf", endereco.UF);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }

        public Endereco FindById(int idPessoa)
        {
            string queryString = "SELECT * FROM ENDERECO WHERE IDPESSOA = " + idPessoa;
            
            Endereco endereco = new Endereco();

            using (conexaoDB)
            {
                SqlCommand command = new SqlCommand(queryString, conexaoDB);
                try
                {
                    SqlDataReader resultado = command.ExecuteReader();
                    while (resultado.Read())
                    {                        
                        endereco.Id = resultado.GetInt32(0);
                        endereco.IdPessoa = resultado.GetInt32(1);
                        endereco.Tipo = resultado.GetString(2);
                        endereco.CEP = resultado.GetString(3);
                        endereco.Logradouro = resultado.GetString(4);
                        endereco.Numero = resultado.GetString(5);
                        endereco.Bairro = resultado.GetString(6);
                        endereco.Cidade = resultado.GetString(7);
                        endereco.UF = resultado.GetString(8);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return endereco;
        }

    }
}
