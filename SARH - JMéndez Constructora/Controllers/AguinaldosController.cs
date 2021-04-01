using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SARH___JMéndez_Constructora.Data;
using SARH___JMéndez_Constructora.Models;
using SARH___JMéndez_Constructora.Models.AguinaldosViewModels;

namespace SARH___JMéndez_Constructora.Controllers
{
    public class AguinaldosController : Controller
    {
        private readonly ApplicationDbContext _appContext;

        public AguinaldosController(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }
        //
        // GET: Aguinaldos
        public IActionResult Index()
        {
            return View(new AguinaldosViewModel
            {
                AguinaldosList = GetAguinaldosList(), 
                EmpleadosList = GetTrabajadoresToSelect()
            });
        }
        //
        // POST: Aguinaldos
        [HttpPost]
        public IActionResult Index(AguinaldosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.EmpleadosList = GetTrabajadoresToSelect();
                return View(model);
            }
            CalcularMonto(model.AguinaldoForm);
            if(InserAguinaldo(model.AguinaldoForm) > 0)
            return RedirectToAction(nameof (Index));

            return RedirectToAction(nameof(Index));
        }
        // AJAX
        // GET: AGUINALDO 
        [HttpGet]
        public IActionResult GetAguinaldo (string IdEmpleado)
        {
            IdEmpleado  = (string.IsNullOrEmpty(IdEmpleado)) ? "0" : IdEmpleado;
            
            return Json(CalcularMonto(new Aguinaldos 
            { 
                IdEmpleado = int.Parse(IdEmpleado)
            }));
        }
        [HttpGet]
        public IActionResult GetPuesto (string IdEmpleado)
        {
            IdEmpleado  = (string.IsNullOrEmpty(IdEmpleado)) ? "0" : IdEmpleado;
            
            return Json(new{ Puesto = GetPuesto(int.Parse(IdEmpleado)) });
        }
        
        #region Helpers
        private int InserAguinaldo (Aguinaldos model)
        {
            try
            {
                _appContext.Aguinaldos.Add(model);
                return _appContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return -1;
            }
        }
        private IEnumerable<AguinaldosListViewModel> GetAguinaldosList()
        {
            List<AguinaldosListViewModel> aux = new List<AguinaldosListViewModel>();

            foreach (var aguinaldo in _appContext.Aguinaldos
                .Include(a => a.IdEmpleadoNavigation).ToList())
            {
                aux.Add(new AguinaldosListViewModel
                {
                    Id = aguinaldo.Id, 
                    IdEmpleado = aguinaldo.IdEmpleado, 
                    Cedula = aguinaldo.IdEmpleadoNavigation.Cedula,
                    Nombre = aguinaldo.IdEmpleadoNavigation.Nombre, 
                    Apellido1 = aguinaldo.IdEmpleadoNavigation.Apellido1, 
                    Apellido2 = aguinaldo.IdEmpleadoNavigation.Apellido2, 
                    IdContrato = aguinaldo.IdContrato, 
                    FechaInicio = aguinaldo.FechaInicio,
                    FechaFin = aguinaldo.FechaFin,
                    SumatoriaSalarioBrutos = aguinaldo.SumatoriaSalarioBrutos, 
                    MontoAguinaldo = aguinaldo.MontoAguinaldo, 
                    Anotaciones = aguinaldo.Anotaciones
                });
            }
            return aux;
        }
        private IEnumerable<SelectListItem> GetTrabajadoresToSelect ()
        {
            IEnumerable<SelectListItem> trababajoresSelectList;
            List<SelectListItem> trabajadoresSelectItem = new List<SelectListItem>();
            trabajadoresSelectItem.Add(new SelectListItem()
            {
                Text = "Seleccione un trabajador",
                Selected = true,
                Disabled = true
            });
            
            // ACTIVOS:
            var auxEmpleadosActivos = _appContext.Empleados
                .Where(e => (e.Ingresocontrato != null && e.Ingresocontrato.Any(i => i.Fincontrato == null)))
                .Include(e =>  e.Ingresocontrato)
                    .ThenInclude(p =>  p.IdPuestoNavigation)
                .Include(f =>  f.Ingresocontrato)
                .OrderBy(e => e.Apellido1)
                .ToList();

            foreach (Empleados empleado in auxEmpleadosActivos)
            {
                trabajadoresSelectItem.Add(new SelectListItem()
                {
                    Value = empleado.Id.ToString(),
                    Text = empleado.Cedula + " - " 
                        + empleado.Apellido1 + " " 
                        + empleado.Apellido2 + " "
                        + empleado.Nombre
                });
            }

            trababajoresSelectList = trabajadoresSelectItem;
            return trababajoresSelectList;
        }
        private Aguinaldos CalcularMonto (Aguinaldos model)
        {
            Ingresocontrato contrato = GetContrato(model.IdEmpleado);
            model.IdContrato = contrato.Id; 
            model.FechaInicio = contrato.Inicio;
            model.FechaFin = DateTime.Today;
            double diasLaborados = TotalDaysWorked(model);
            double mesesLaborados = diasLaborados / 30;
            model.SumatoriaSalarioBrutos = GetTotalSalarios(model);
            decimal montoPagado = GetMontosPagados(model);
            model.MontoAguinaldo = (model.SumatoriaSalarioBrutos 
                / (decimal) mesesLaborados) - montoPagado;
            return model;
        }
        private Ingresocontrato GetContrato(int idEmpleado)
        {
            return _appContext.Ingresocontrato.SingleOrDefault(
                i => (i.IdEmpleado == idEmpleado && i.Fincontrato == null)
            );
        }
        private double TotalDaysWorked (Aguinaldos model)
        {
            return (model.FechaFin - model.FechaInicio).TotalDays + 1;
        }
        private decimal GetTotalSalarios(Aguinaldos model)
        {
            decimal totalBrutos = 0;
            IEnumerable<Pagos> pagosList = _appContext
                .Pagos
                .Include(p => p.IdTiempoNavigation)
                .Where(p => p.IdContrato == model.IdContrato)
                .ToList();
            foreach (var pago in pagosList)
            {
                totalBrutos += pago.SalarioBruto;
            }

            return totalBrutos;
        }
        private decimal GetMontosPagados(Aguinaldos model)
        {
            decimal total = 0;
            total = _appContext
                .Aguinaldos
                .Where(p => p.IdContrato == model.IdContrato)
                .Sum(p => p.MontoAguinaldo);
            
            return total;
        }
        private string GetPuesto(int idEmpleado)
        {
            return _appContext.Ingresocontrato
                .Include(i => i.IdPuestoNavigation)
                .SingleOrDefault(i => 
                    (i.IdEmpleado == idEmpleado && i.Fincontrato == null))
                .IdPuestoNavigation.Nombre;
        }
        #endregion
    }
}
