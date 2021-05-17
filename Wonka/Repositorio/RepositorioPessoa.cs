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

        public void Insert(PessoaViewModel pessoa)
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

        public List<Pessoa> FindAll()
        {
            List<Pessoa> listaPessoa = new List<Pessoa>();

            using (conexaoDB)
            {
                string queryString = "SELECT ID,NOME,SOBRENOME FROM PESSOA";
                SqlCommand cmd = new SqlCommand(queryString, conexaoDB);
                try
                {
                    SqlDataReader resultado = cmd.ExecuteReader();
                    RepositorioEndereco repositorioEndereco = new RepositorioEndereco();

                    if (resultado.HasRows)
                    {
                        while (resultado.Read())
                        {
                            var pessoa = new Pessoa
                            {
                                Id = resultado.GetInt32(0),
                                Nome = resultado.GetString(1),
                                Sobrenome = resultado.GetString(2)
                            };
                            listaPessoa.Add(pessoa);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return listaPessoa;
        }

        public List<PessoaViewModel> FindById(int idPessoa)
        {
            List<PessoaViewModel> listaPessoa = new List<PessoaViewModel>();

            using (conexaoDB)
            {
                string queryString = "SELECT ID,NOME,SOBRENOME FROM PESSOA WHERE ID=" + idPessoa;
                SqlCommand cmd = new SqlCommand(queryString, conexaoDB);
                try
                {
                    SqlDataReader resultado = cmd.ExecuteReader();
                    RepositorioEndereco repositorioEndereco = new RepositorioEndereco();
                    RepositorioDocumento repositorioDocumento = new RepositorioDocumento();
                    RepositorioTelefone repositorioTelefone = new RepositorioTelefone();

                    if (resultado.HasRows)
                    {
                        while (resultado.Read())
                        {
                            PessoaViewModel pessoaViewModel = new PessoaViewModel
                            {
                                Pessoa = new Pessoa
                                {
                                    Id = resultado.GetInt32(0),
                                    Nome = resultado.GetString(1),
                                    Sobrenome = resultado.GetString(2)
                                },
                                Endereco = repositorioEndereco.FindById(idPessoa),
                                Documento = repositorioDocumento.FindById(idPessoa),
                                Telefone = repositorioTelefone.FindById(idPessoa)
                            };
                            listaPessoa.Add(pessoaViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return listaPessoa;
        }

        public void Update(PessoaViewModel pessoa)
        {
            using (conexaoDB)
            {
                SqlCommand cmd = conexaoDB.CreateCommand();
                try
                {
                    string queryString = "UPDATE PESSOA SET NOME=(@nome),SOBRENOME=(@sobrenome)" +
                        " WHERE ID = " + pessoa.Pessoa.Id;
                    cmd.CommandText = queryString;
                    cmd.Parameters.AddWithValue("@nome", pessoa.Pessoa.Nome);
                    cmd.Parameters.AddWithValue("@sobrenome", pessoa.Pessoa.Sobrenome);
                    cmd.ExecuteNonQuery();

                    RepositorioEndereco repositorioEndereco = new RepositorioEndereco();
                    repositorioEndereco.Update(pessoa.Endereco, pessoa.Endereco.Id);

                    #region comentado
                    //foreach (var documento in pessoa.Documento)
                    //{
                    //    RepositorioDocumento repositorioDocumento = new RepositorioDocumento();

                    //    if (documento.Id > 0)
                    //    {
                    //        repositorioDocumento.Update(documento.Id, documento);
                    //    }
                    //    else
                    //    {
                    //        repositorioDocumento.Insert(documento, pessoa.Pessoa.Id);
                    //    }
                    //}

                    //foreach (var telefone in pessoa.Telefone)
                    //{
                    //    RepositorioTelefone repositorioTelefone = new RepositorioTelefone();

                    //    if (telefone.Id > 0)
                    //    {
                    //        repositorioTelefone.Update(telefone.Id, telefone);
                    //    }
                    //    else
                    //    {
                    //        repositorioTelefone.Insert(telefone, pessoa.Pessoa.Id);
                    //    }
                    //}
                    #endregion
                }
                catch (Exception ex)
                {

                }
            }
        }

    }
}
