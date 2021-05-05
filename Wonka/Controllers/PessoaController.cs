using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Services;
using Wonka.Models;

namespace Wonka.Controllers
{
    public class PessoaController : Controller
    {
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
            throw new NotImplementedException();
        }
    }
}
