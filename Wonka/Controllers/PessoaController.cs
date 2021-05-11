using System;
using System.Collections.Generic;
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
        private RepositorioPessoa repositorioPessoa = new RepositorioPessoa();
        
        public ViewResult Index()
        {
            var repositorio = repositorioPessoa.FindById(1);
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
