using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
using Wonka.Models;
using Wonka.Repositorio;

namespace Wonka.Controllers
{
    public class PessoaController : Controller
    {
        public enum TipoPessoa
        {
            [Description("Cliente")]
            Cliente = 1,
            [Description("Fornecedor")]
            Fornecedor = 2
        }

        public enum TipoDocumento
        {
            [Description("Cpf")]
            Cpf = 1,
            [Description("CNPJ")]
            Cnpj = 2
        }

        private RepositorioPessoa repositorioPessoa = new RepositorioPessoa();
        private RepositorioEndereco repositorioEndereco = new RepositorioEndereco();
        private RepositorioDocumento repositorioDocumento = new RepositorioDocumento();
        private RepositorioTelefone repositorioTelefone = new RepositorioTelefone();

        public ViewResult Index()
        {            
            return View();
        }

        [HttpGet]
        public ActionResult FindAll()
        {            
            var listaPessoas = new
            {              
                ListaPessoas = repositorioPessoa.FindAll()
            };

            return Json(listaPessoas, JsonRequestBehavior.AllowGet);
        }

        public ViewResult Alterar(int id)
        {
            ViewBag.Id = id;
            ViewBag.IdEndereco = repositorioEndereco.FindById(id).Id;
            return View();
        }

        [HttpPost]
        public JsonResult Alterar(PessoaViewModel jsonPessoa)
        {
            if (jsonPessoa.Pessoa.Id > 0) 
            {               
                repositorioPessoa.Update(jsonPessoa);
            }
            
            return Json(new { sucesso = true });
        }

        [HttpPost]
        public JsonResult AdicionarDocumento(Documento documento, int idPessoa)
        {
            int id = repositorioDocumento.Insert(documento, idPessoa);

            return Json(new { sucesso = true, id });
        }

        [HttpPost]
        public JsonResult AdicionarTelefone(Telefone telefone, int idPessoa)
        {
            repositorioTelefone.Insert(telefone, idPessoa);

            return Json(new { sucesso = true });
        }

        [HttpPost]
        public JsonResult DeletarDocumento(int id)
        {
            if (id > 0)
            {
                repositorioDocumento.Delete(id);
            }

            return Json(new { sucesso = true });
        }

        [HttpPost]
        public JsonResult DeletarTelefone(int id)
        {
            if (id > 0)
            {
                repositorioTelefone.Delete(id);
            }

            return Json(new { sucesso = true });
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            var pessoa = new
            {
                Pessoa = repositorioPessoa.FindById(id)
            };

            return Json(pessoa, JsonRequestBehavior.AllowGet);
        }

        public ViewResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Inserir(PessoaViewModel jsonPessoa)
        {
            repositorioPessoa.Insert(jsonPessoa);
            return Json(new { sucesso = true });
        }
    }
}
