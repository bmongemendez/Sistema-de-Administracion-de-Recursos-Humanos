using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Controllers
{
    public class PagosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
