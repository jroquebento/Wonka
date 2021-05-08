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
    }
}
