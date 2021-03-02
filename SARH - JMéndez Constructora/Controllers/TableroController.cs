using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SARH___JMéndez_Constructora.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SARH___JMéndez_Constructora.Controllers
{
    public class TableroController : Controller
    {
        private readonly ILogger<TableroController> _logger;

        public TableroController(ILogger<TableroController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
