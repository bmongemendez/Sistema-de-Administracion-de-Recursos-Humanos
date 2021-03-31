using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SARH___JMéndez_Constructora.Data;
using SARH___JMéndez_Constructora.Models;
using SARH___JMéndez_Constructora.Models.TableroViewModels;
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
        private readonly ApplicationDbContext _appContext;

        public TableroController(ILogger<TableroController> logger, ApplicationDbContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
        }

        public IActionResult Index()
        {
            ViewData["AuditRecent"] = BindAuditRecentViewModel 
            (GetBitacoraEmpleados(), GetBitacoraPagos());
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #region Helpers
        private IEnumerable<Empleadosregistroauditoria> GetBitacoraEmpleados ()
        {
            return _appContext.Empleadosregistroauditoria.ToList()
                .OrderByDescending(p => p.FechaHoraTransaccion)
                .Take(10);;
        }
        private IEnumerable<Pagosregistroauditoria> GetBitacoraPagos ()
        {
            return _appContext.Pagosregistroauditoria.ToList()
                .OrderByDescending(p => p.FechaHoraTransaccion)
                .Take(10);
        }
        private IEnumerable<AuditRecentViewModel> BindAuditRecentViewModel (
            IEnumerable<Empleadosregistroauditoria> empleadosAuditList, 
            IEnumerable<Pagosregistroauditoria> pagosAuditList)
        {
            List<AuditRecentViewModel> list = new List<AuditRecentViewModel>();
            foreach (var empleado in empleadosAuditList)
            {
                list.Add(new AuditRecentViewModel
                {
                    Tabla = "Empleados",
                    IdTabla = empleado.IdEmpleado,
                    Columna = empleado.Columna,
                    FechaHoraTransaccion = empleado.FechaHoraTransaccion,
                    ValorAnterior = empleado.ValorAnterior,
                    ValorNuevo = empleado.ValorNuevo,
                    UserName = empleado.UserName
                });
            } 
            foreach (var pago in pagosAuditList)
            {
                list.Add (new AuditRecentViewModel
                {
                    Tabla = "Pagos",
                    IdTabla = pago.IdPago,
                    Columna = pago.Columna,
                    FechaHoraTransaccion = pago.FechaHoraTransaccion,
                    ValorAnterior = pago.ValorAnterior,
                    ValorNuevo = pago.ValorNuevo,
                    UserName = pago.UserName
                });
            }
            return list
                .OrderByDescending(ar => ar.FechaHoraTransaccion)
                .Take(10);
        }
        #endregion
    }
}
