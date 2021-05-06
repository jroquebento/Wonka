using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wonka.Models;

namespace Wonka.Repositorio
{
    public class RepositorioPessoa
    {
        private SqlConnection conexaoDB;

        public RepositorioPessoa()
        {
            conexaoDB = new RepositorioConexaoDB().GetConnection();
        }

        public void Insert(AdicionarPessoaViewModel viewModel)
        {
            using (conexaoDB)
            {
                SqlCommand cmd = conexaoDB.CreateCommand();
                SqlTransaction transaction = conexaoDB.BeginTransaction("");
                cmd.Transaction = transaction;
                try
                {
                    string queryString = "INSERT INTO PESSOA VALUES(@nome,@sobrenome) SELECT SCOPE_IDENTITY()";                    
                    cmd.CommandText = queryString;
                    cmd.Parameters.AddWithValue("@nome", viewModel.Pessoa.Nome);
                    cmd.Parameters.AddWithValue("@sobrenome", viewModel.Pessoa.Sobrenome);
                    int idPessoa = Convert.ToInt32(cmd.ExecuteScalar());

                    queryString = "INSERT INTO ENDERECO VALUES(@idPessoa,@tipoEndereco,@cep,@logradouro," +
                        "@numeroEndereco,@bairro,@cidade,@uf)";
                    cmd.CommandText = queryString;                    
                    cmd.Parameters.AddWithValue("@idPessoa", idPessoa);
                    cmd.Parameters.AddWithValue("@tipoEndereco", viewModel.Endereco.Tipo);
                    cmd.Parameters.AddWithValue("@cep", viewModel.Endereco.CEP);
                    cmd.Parameters.AddWithValue("@logradouro", viewModel.Endereco.Logradouro);
                    cmd.Parameters.AddWithValue("@numeroEndereco", viewModel.Endereco.Numero);
                    cmd.Parameters.AddWithValue("@bairro", viewModel.Endereco.Bairro);
                    cmd.Parameters.AddWithValue("@cidade", viewModel.Endereco.Cidade);
                    cmd.Parameters.AddWithValue("@uf", viewModel.Endereco.UF);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    queryString = "INSERT INTO DOCUMENTO VALUES(@idPessoa,@tipoDocumento,@numeroDocumento)";
                    
                    foreach (var item in viewModel.Documento)
                    {                        
                        cmd.CommandText = queryString;
                        cmd.Parameters.AddWithValue("@idPessoa", idPessoa);
                        cmd.Parameters.AddWithValue("@tipoDocumento", item.Tipo);
                        cmd.Parameters.AddWithValue("@numeroDocumento", item.Numero);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    queryString = "INSERT INTO TELEFONE VALUES(@idPessoa,@tipoTelefone,@ddd,@numeroTelefone)";
                    
                    foreach (var item in viewModel.Telefone)
                    {
                        cmd.CommandText = queryString;
                        cmd.Parameters.AddWithValue("@idPessoa", idPessoa);
                        cmd.Parameters.AddWithValue("@tipoTelefone", item.Tipo);
                        cmd.Parameters.AddWithValue("@ddd", item.DDD);
                        cmd.Parameters.AddWithValue("@numeroTelefone", item.Numero);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    cmd.Transaction.Rollback();
                }
            }
        }
    }
}
