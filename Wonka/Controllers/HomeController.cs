﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Wonka.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() 
        {
            return View();
        }
        public ViewResult About() {
            return View();
        }
    }
}
