using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SARH___JMéndez_Constructora.Data;
using SARH___JMéndez_Constructora.Models;
using SARH___JMéndez_Constructora.Models.FinContratoViewModels;
using SARH___JMéndez_Constructora.Models.TrabajadoresViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SARH___JMéndez_Constructora.Controllers
{
    [Authorize]
    public class FinContratoController : Controller
    {
        private readonly ApplicationDbContext _appContext;
        public FinContratoController(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }
        [HttpGet]
        public IActionResult Index(int id, TrabajadoresMessageId? message = null)
        {
            //ViewData["StatusMessage"] =
            //    message == TrabajadoresMessageId.AddEmployeeSuccess ? "Se ha agregado al empleado."
            //    : message == TrabajadoresMessageId.Error ? "Ha ocurrido un error."
            //    : "";
            Ingresocontrato ingresocontrato = GetIgresoContratoInclude(id);
            
            return View(new FinContratoViewModel 
            {
                FinContratoForm = new FinContratoFormViewModel { IdInicioContrato = id }, 
                MotivoSalida = GetMotivoSelectList(), 
                PreavisoEjercido = GetPreavisoSelectList()
            });
        }
        //
        [HttpPost]
        public IActionResult Index(FinContratoViewModel pageModel)
        {
            if (!ModelState.IsValid)
            {   
                pageModel.MotivoSalida = GetMotivoSelectList();
                pageModel.PreavisoEjercido = GetPreavisoSelectList();
                return View(pageModel);
            }
            
            FinContratoFormViewModel modell = pageModel.FinContratoForm;

            if(!ContratoExist(modell.IdInicioContrato))
            return RedirectToAction(nameof(Index), new { Message = TrabajadoresMessageId.Error});
            
            //Ingresocontrato aux = GetIgresoContratoInclude(model.FinContratoForm.IdInicioContrato);
            
            Fincontrato aux2 = new Fincontrato 
            {
                IdInicioContrato = modell.IdInicioContrato, 
                MotivoSalida = modell.MotivoSalida, 
                PreavisoEjercido = modell.PreavisoEjercido, 
                DiasPendientesPreaviso = modell.DiasPendientesPreaviso, 
                SaldoVacaciones = modell.SaldoVacaciones
            };
            
            Calcular(aux2);
            
            return RedirectToAction(nameof(PreviewConfirm), aux2);
        }
        [HttpGet]
        public IActionResult PreviewConfirm (Fincontrato model)
        {
            return View(model);
        }
        [HttpPost]
        public IActionResult Confirm (Fincontrato model)
        {
            if(FinalizarContrato(model) >= 1)
            return RedirectToAction(nameof(Index), "Trabajadores", new { Message = TrabajadoresMessageId.EndContractSuccess});
            
            return RedirectToAction(nameof(Index), "Trabajadores", new { Message = TrabajadoresMessageId.Error});
        }
        #region Helpers
        private Ingresocontrato GetIgresoContratoInclude(int id)
        {
            return _appContext.Ingresocontrato
                .Include(i => i.IdPuestoNavigation)
                .Include(i => i.Pagos)
                .Include(i => i.Tiempo)
                .SingleOrDefault(i => i.Id == id);
        }
        private void Calcular (Fincontrato model) 
        { 
            Ingresocontrato ic = GetIgresoContratoInclude(model.IdInicioContrato);
            IEnumerable<Pagos> ultimos6Pagos = ic.Pagos.Where(p 
                => p.IdTiempoNavigation.FechaInicio > System.DateTime.Now.AddMonths(-6));
            int diasLaborados = (System.DateTime.Now.Date - ic.Inicio.Date).Days + 1; 
            //int motivoSalida = 1;
            //decimal total = 0;
            decimal salarioPromedio = 0;
            decimal salarioDiario = 0;
            //int saldoVacaciones = 0;
            //decimal cesantia = (decimal)GetDiasCesantia(diasLaborados) * salarioPromedio;
            model.Cesantia = (decimal)GetDiasCesantia(diasLaborados) * salarioPromedio;

            //if (ultimos6Pagos.Count() < 6)
            //{
            //    salarioPromedio = ultimos6Pagos.Sum(p => p.SalarioBruto) / ultimos6Pagos.Count();
            //}
            //else
            //{
                salarioPromedio = ultimos6Pagos.Sum(p => p.SalarioBruto) / 6;
            //}

            salarioDiario = salarioPromedio / 26;
            
            // decimal aguinaldo = ultimos6Pagos.Where(p => 
            //     p.IdTiempoNavigation.FechaInicio.Year == System.DateTime.Now.Year)
            //     .Sum(p => p.SalarioBruto) / 12;
            model.Aguinaldo = ultimos6Pagos.Where(p => 
                p.IdTiempoNavigation.FechaInicio.Year == System.DateTime.Now.Year)
                .Sum(p => p.SalarioBruto) / 12;
            //decimal vacaciones = saldoVacaciones * salarioPromedio;
            model.Vacaciones = model.SaldoVacaciones * salarioDiario;

            model.Preaviso = salarioPromedio;

            if(model.MotivoSalida == 1 || model.MotivoSalida == 3)
            //cesantia = (decimal)GetDiasCesantia(diasLaborados) * salarioDiario;
            model.Cesantia = (decimal)GetDiasCesantia(diasLaborados) * salarioDiario;
            
            //total = aguinaldo + vacaciones + cesantia;
        }
        private int GetDiasPreaviso(int diasLaborados = 0)
        {
            int diasPreaviso = 0;
            if (diasLaborados > 91 && diasLaborados <= 180)
            diasPreaviso = 7;
            if (diasLaborados > 180 && diasLaborados <= 360)
            diasPreaviso = 15;
            if (diasLaborados > 360)
            diasPreaviso = 30;
            return diasPreaviso;
        }
        private double GetDiasCesantia(int dias = 0)
        {
            double diasCesantia = 0;
            int diasAnio = 360;
            if (dias > 90 && dias <= 180)
            { diasCesantia = 7; }
            else if(dias > 180 && dias <= 360)
            { diasCesantia = 14; } 
            else if(dias > 360)
            { diasCesantia = 19.5; } 
            else if (dias >= diasAnio * 2)
            { diasCesantia = 20; }
            else if (dias >= diasAnio * 3)
            { diasCesantia = 20.5; }
            else if (dias >= diasAnio * 4)
            { diasCesantia = 21; }
            else if (dias >= diasAnio * 5)
            { diasCesantia = 21.24; }
            else if (dias >= diasAnio * 6)
            { diasCesantia = 21.5; }
            else if (dias >= diasAnio * 7)
            { diasCesantia = 22; }
            else if (dias >= diasAnio * 10)
            { diasCesantia = 21.5; }
            else if (dias >= diasAnio * 11)
            { diasCesantia = 21; }
            else if (dias >= diasAnio * 12)
            { diasCesantia = 20.5; }
            else if (dias >= diasAnio * 13)
            { diasCesantia = 20; }
            return diasCesantia;
        }
        private IEnumerable<SelectListItem> GetMotivoSelectList ()
        {
            List<SelectListItem> motivosSelectList = new List<SelectListItem>();
            motivosSelectList.Add(new SelectListItem()
            {
                Value = "-1",
                Text = "Seleccione un motivo",
                Selected = true,
                Disabled = true
            });
            motivosSelectList.Add(new SelectListItem() { Value = "0", Text = "Renuncia" });
            //motivosSelectList.Add(new SelectListItem() { Value = "1", Text = "Renuncia con Responsabilidad Patronal" });
            motivosSelectList.Add(new SelectListItem() { Value = "2", Text = "Despido sin Responsabilidad Patronal" });
            motivosSelectList.Add(new SelectListItem() { Value = "3", Text = "Despído con Responsabilidad Patronal" });
            motivosSelectList.Add(new SelectListItem() { Value = "5", Text = "Se acoje a pensión" });
            
            return motivosSelectList;
        }
        private IEnumerable<SelectListItem> GetPreavisoSelectList ()
        {
            //IEnumerable<SelectListItem> puestosSelectList;
            List<SelectListItem> preavisoSelectList = new List<SelectListItem>();
            preavisoSelectList.Add(new SelectListItem()
            {
                Value = "-1",
                Text = "Seleccione una opción",
                Selected = true,
                Disabled = true
            });
            preavisoSelectList.Add(new SelectListItem() { Value = "0", Text = "Preaviso trabajo total" });
            preavisoSelectList.Add(new SelectListItem() { Value = "2", Text = "Preaviso a pagar total" });
            preavisoSelectList.Add(new SelectListItem() { Value = "3", Text = "Días pendientes de preaviso: " });
            
            return preavisoSelectList;
        }
        public enum TrabajadoresMessageId
        {
            EndContractSuccess, 
            Error
        }
        private bool ContratoExist(int id)
        {
            return _appContext.Ingresocontrato
                .SingleOrDefault(i => i.Id == id) != null;
        }
        private int FinalizarContrato (Fincontrato model)
        {
            _appContext.Fincontrato.Add(model);
            try
            {
                return _appContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return -1;
            }
        }
        #endregion
    }
}
