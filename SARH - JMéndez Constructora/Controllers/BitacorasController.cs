using Microsoft.AspNetCore.Mvc;
using SARH___JMéndez_Constructora.Data;
using SARH___JMéndez_Constructora.Models;
using SARH___JMéndez_Constructora.Models.IncapacidadesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SARH___JMéndez_Constructora.Controllers
{
    public class BitacorasController : Controller
    {
        private readonly ApplicationDbContext _appContext;

        public BitacorasController(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel 
            { 
                BitacoraEmpleados = GetBitacoraEmpleados(),
                BitacoraPagos = GetBitacoraPagos() 
            });
        }

        #region Helpers
        private IEnumerable<Empleadosregistroauditoria> GetBitacoraEmpleados ()
        {
            return _appContext.Empleadosregistroauditoria.ToList();
        }
        private IEnumerable<Pagosregistroauditoria> GetBitacoraPagos ()
        {
            return _appContext.Pagosregistroauditoria.ToList();
        }
        #endregion
    }
}
