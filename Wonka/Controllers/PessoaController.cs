﻿using System;
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
        private RepositorioPessoa repositorioPessoa = new RepositorioPessoa();
        private RepositorioDocumento repositorioDocumento = new RepositorioDocumento();
        private RepositorioEndereco repositorioEndereco = new RepositorioEndereco();
        private RepositorioTelefone repositorioTelefone = new RepositorioTelefone();

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
            repositorioPessoa.Insert(jsonPessoa);
            return Json(new { sucesso = true });
        }
    }
}
