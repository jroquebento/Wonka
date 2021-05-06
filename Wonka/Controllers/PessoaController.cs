using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Services;
using Wonka.Models;
using Wonka.Repositorio;

namespace Wonka.Controllers
{
    public class PessoaController : Controller
    {
        private RepositorioPessoa pessoaRepositorio = new RepositorioPessoa();

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Inserir(AdicionarPessoaViewModel jsonPessoa)
        {
           
            pessoaRepositorio.Insert(jsonPessoa);
            
            throw new NotImplementedException();
        }
    }
}
