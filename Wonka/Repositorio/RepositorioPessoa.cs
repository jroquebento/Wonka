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

        public void Insert(AdicionarPessoaViewModel pessoa)
        {
            int idPessoa = 0;

            using (conexaoDB)
            {
                SqlCommand cmd = conexaoDB.CreateCommand();
                SqlTransaction transaction = conexaoDB.BeginTransaction("");
                cmd.Transaction = transaction;
                try
                {
                    string queryString = "INSERT INTO PESSOA VALUES(@nome,@sobrenome) SELECT SCOPE_IDENTITY()";
                    cmd.CommandText = queryString;
                    cmd.Parameters.AddWithValue("@nome", pessoa.Pessoa.Nome);
                    cmd.Parameters.AddWithValue("@sobrenome", pessoa.Pessoa.Sobrenome);
                    idPessoa = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Parameters.Clear();

                    if (idPessoa > 0)
                    {                     
                        RepositorioDocumento repositorioDocumento = new RepositorioDocumento();
                        repositorioDocumento.Insert(pessoa.Documento, idPessoa, cmd);

                        RepositorioEndereco repositorioEndereco = new RepositorioEndereco();
                        repositorioEndereco.Insert(pessoa.Endereco, idPessoa, cmd);

                        RepositorioTelefone repositorioTelefone = new RepositorioTelefone();
                        repositorioTelefone.Insert(pessoa.Telefone, idPessoa, cmd);
                                                       
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();                    
                }
            }
        }
    }
}
