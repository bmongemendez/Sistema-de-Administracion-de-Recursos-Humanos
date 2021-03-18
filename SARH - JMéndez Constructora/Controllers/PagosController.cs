using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SARH___JMéndez_Constructora.Data;
using SARH___JMéndez_Constructora.Models;
using SARH___JMéndez_Constructora.Models.PagosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcMovie.Controllers
{
    public class PagosController : Controller
    {
        private readonly ApplicationDbContext _appContext;

        public PagosController(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }

        [HttpGet]
        public IActionResult Index(PagosMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == PagosMessageId.AddPaymentSuccess ? "Se ha agregado el pago."
                : message == PagosMessageId.Error ? "Ha ocurrido un error."
                : "";

            return View( new PagosViewModel 
            {
                empleados = GetTrabajadoresToSelect(),
                pagos = GetPagosList()
            } );
        }

        [HttpPost]
        public IActionResult Index(PagosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.empleados = GetTrabajadoresToSelect();
                model.pagos = GetPagosList();
                return View(model);
            }

            if (InsertPago(BindModel(model.ingresoPago)
                , BindTiempoModel(model.ingresoPago)) == 1)
            return RedirectToAction(nameof(Index), new { Message = PagosMessageId.AddPaymentSuccess });                
            
            return RedirectToAction(nameof(Index), new { Message = PagosMessageId.Error });
        }

        [HttpGet]
        public IActionResult GetIdContrato (string idEmpleado)
        {
            if(string.IsNullOrEmpty(idEmpleado))
            return Json( new { idContrato = 0, salarioHora = 0 } );
            
            int id = int.Parse(idEmpleado);
            int idContrato = GetContrato(id);
            return Json( new { idContrato = idContrato, salarioHora = GetPagoHora(idContrato) } );
        }

        [HttpGet]
        public IActionResult GetPreviewIngresoPago (string IdEmpleado, string IdContrato, string Inicio,
            string Fin, string HorasNormal, string HorasExtra, string DiaDescanso)
        {
            HorasNormal = (string.IsNullOrEmpty(HorasNormal)) ? "0" : HorasNormal;
            HorasExtra = (string.IsNullOrEmpty(HorasExtra)) ? "0" : HorasExtra;
            DiaDescanso = (string.IsNullOrEmpty(DiaDescanso)) ? "0" : DiaDescanso;
            return Json( BindModel(new IngresoPagoFormViewModel 
            {
                IdEmpleado = int.Parse(IdEmpleado),
                IdContrato = int.Parse(IdContrato),
                Inicio = DateTime.Parse(Inicio),
                Fin = DateTime.Parse(Fin),
                HorasNormal = decimal.Parse(HorasNormal),
                HorasExtra = decimal.Parse(HorasExtra),
                DiaDescanso = decimal.Parse(DiaDescanso)
            }) );
        }

        #region Helpers
        // Operaciones Contexto
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
        private IEnumerable<Deducciones> GetAllDeducciones()
        {
            return _appContext.Deducciones.ToList();
        }
        private int InsertPago(Pagos model, Tiempo modelTiempo)
        {
            using var transaction = _appContext.Database.BeginTransaction();

            try
            {
                _appContext.Tiempo.Add(modelTiempo);
                _appContext.SaveChanges();
                
                // Agregar el id tiempo registrado
                model.IdTiempo = modelTiempo.Id;

                _appContext.Pagos.Add(model);
                _appContext.SaveChanges();
                
                transaction.Commit();

                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        private int GetContrato(int idEmpleado)
        {
            return _appContext.Ingresocontrato.SingleOrDefault(
                i => (i.IdEmpleado == idEmpleado && i.Fincontrato == null)
            ).Id;
        }
        private IEnumerable<PagosList> GetPagosList()
        {
            List<PagosList> pagosLists = new List<PagosList>();
            var contextList = _appContext.Pagos
                .Include(p => p.IdEmpleadoNavigation)
                .Include(p => p.IdTiempoNavigation)
                .ToList();
                
            foreach (var item in contextList)
            {
                pagosLists.Add(new PagosList
                {
                    Id = item.Id,
                    IdEmpleado = item.IdEmpleado,
                    Cedula = item.IdEmpleadoNavigation.Cedula,
                    Nombre = item.IdEmpleadoNavigation.Nombre,
                    Apellido1 = item.IdEmpleadoNavigation.Apellido1,
                    Apellido2 = item.IdEmpleadoNavigation.Apellido2,
                    Inicio = item.IdTiempoNavigation.FechaInicio,
                    Fin = item.IdTiempoNavigation.FechaFin,
                    SalarioBruto = item.SalarioBruto,
                    SalarioNeto = item.SalarioNeto,
                    TotalDeduccionesPatrono = item.PatronoCcss 
                        + item.PatronoLpt + item.PatronoRotrasInstituciones,
                    HorasNormal = item.HorasNormal,
                    HorasExtra = item.HorasExtra
                    //DiaDescanso,
                });
            }
            return pagosLists;
        }
        // Operaciones en Aplicacion
        private double GetNormalHours(DateTime from, DateTime to)
        {
            return ((to.Date - from.Date).TotalDays + 1) * 8;
        }
        private decimal GetNormalHoursPayment(decimal horas, decimal pagoHora)
        {
            return (decimal)horas * pagoHora;
        }
        private decimal GetExtraHoursPayment(decimal horas, decimal pagoHora)
        {
            return (decimal)horas * (pagoHora + (pagoHora/2));
        }
        private decimal GetFreeDaysHours(decimal totalHoras)
        {
            return (totalHoras * 8 / 48);
        }
        private decimal GetFreeDaysHoursPayment(decimal freeDaysHours, decimal pagoHora)
        {
            return (decimal)freeDaysHours * pagoHora;
        }
        private decimal GetMontoDeduccionPatrono(decimal salarioEmpleado, string grupo)
        {
            decimal total = 0;
            foreach (Deducciones concepto in GetAllDeducciones()
                .Where(d => d.Grupo.Equals(grupo)))
            {
                //total += (concepto.Patrono != null) ? concepto.Patrono * salarioEmpleado / 100;
                total += concepto.Patrono.GetValueOrDefault() * salarioEmpleado / 100;
            }
            return total;
        }
        private decimal GetMontoDeduccionTrabajador(decimal salarioEmpleado, string grupo)
        {
            decimal total = 0;
            foreach (Deducciones concepto in GetAllDeducciones()
                .Where(d => d.Grupo.Equals(grupo)))
            {
                //total += (concepto.Patrono != null) ? concepto.Patrono * salarioEmpleado / 100;
                total += concepto.Trabajador.GetValueOrDefault() * salarioEmpleado / 100;
            }
            return total;
        }
        private decimal GetPagoHora(int idContrato)
        {
            return (_appContext.Ingresocontrato
                .SingleOrDefault(i => i.Id == idContrato)
                .SalarioDefinidoDia) / 8;
        }
        private Pagos BindModel(IngresoPagoFormViewModel model)
        {
            Pagos pagoModel = new Pagos();
            pagoModel.IdEmpleado = model.IdEmpleado;
            pagoModel.IdContrato = model.IdContrato;
            pagoModel.Observaciones = model.Observaciones;
            pagoModel.UserName = "rrhh";
            // Pago por hora
            decimal pagoHora = GetPagoHora(model.IdContrato);
            // Calculo Horas
            decimal horasExtra = model.HorasExtra.GetValueOrDefault();
            pagoModel.HorasNormal = (decimal)GetNormalHours(model.Inicio, model.Fin);
            
            if ( model.HorasNormal != pagoModel.HorasNormal && 
                model.HorasNormal != 0)
            pagoModel.HorasNormal = model.HorasNormal;

            decimal horasDiasDescanso = GetFreeDaysHours(pagoModel.HorasNormal);
            pagoModel.HorasExtra = horasExtra;
            pagoModel.DiaDescanso = horasDiasDescanso;
            //Calculo Pagos
            decimal montoHorasNormal = GetNormalHoursPayment(pagoModel.HorasNormal, pagoHora);
            decimal montoHorasExtra = GetExtraHoursPayment(horasExtra, pagoHora);
            decimal montoDiasHorasDescanso = GetFreeDaysHoursPayment(horasDiasDescanso, pagoHora);

            // Desgloce Salario:
            pagoModel.SalarioNormal = montoHorasNormal;
            pagoModel.SalarioExtras = montoHorasExtra;
            pagoModel.SalarioDiaDescanso = montoDiasHorasDescanso;
            //Falta cuentas por pagar
            pagoModel.SalarioBruto = montoHorasNormal + montoHorasExtra + montoDiasHorasDescanso;

            // Deducciones (trabajador)
            decimal deduccionCCSS = GetMontoDeduccionTrabajador(pagoModel.SalarioBruto, "A");
            decimal deduccionBancoPopular = GetMontoDeduccionTrabajador(pagoModel.SalarioBruto, "C");
            pagoModel.Deducciones = deduccionCCSS + deduccionBancoPopular;

            // Salario Neto:
            pagoModel.SalarioNeto = pagoModel.SalarioBruto - (deduccionCCSS + deduccionBancoPopular);

            //Deducciones Patrono:
            pagoModel.PatronoCcss = GetMontoDeduccionPatrono(pagoModel.SalarioBruto, "A");
            pagoModel.PatronoRotrasInstituciones = GetMontoDeduccionPatrono(pagoModel.SalarioBruto, "B");
            pagoModel.PatronoLpt = GetMontoDeduccionPatrono(pagoModel.SalarioBruto, "C");

            //decimal totalDeduccionesPatrono = patronoCCSS + patronoOtrasInstituciones + patronoLPT;
            RoundRecimalValues(pagoModel);
            return pagoModel;
        }
        private Tiempo BindTiempoModel(IngresoPagoFormViewModel model)
        {
            return new Tiempo
            {
                IdEmpleado = model.IdEmpleado,
                IdContrato = model.IdContrato,
                EsLaborado = true,
                FechaInicio = model.Inicio,
                FechaFin = model.Fin
            };
        }
        private void RoundRecimalValues(Pagos model)
        {
            model.HorasNormal = Decimal.Round(model.HorasNormal, 3);
            model.HorasExtra = Decimal.Round(model.HorasExtra.GetValueOrDefault(), 3);
            model.DiaDescanso = Decimal.Round(model.DiaDescanso.GetValueOrDefault(), 3);
            // Desgloce Salario:
            model.SalarioNormal = Decimal.Round(model.SalarioNormal, 3);
            model.SalarioExtras = Decimal.Round(model.SalarioExtras.GetValueOrDefault(), 3);
            model.SalarioDiaDescanso = Decimal.Round(model.SalarioDiaDescanso.GetValueOrDefault(), 3);
            model.SalarioBruto = Decimal.Round(model.SalarioBruto, 3);
            // Deducciones (trabajador)
            model.Deducciones = Decimal.Round(model.Deducciones.GetValueOrDefault(), 3);
            model.CuentasPorPagar = Decimal.Round(model.CuentasPorPagar.GetValueOrDefault(), 3);
            // Salario Bruto:
            model.SalarioNeto = Decimal.Round(model.SalarioNeto, 3);
            //Deducciones Patrono:
            model.PatronoCcss = Decimal.Round(model.PatronoCcss, 3);
            model.PatronoRotrasInstituciones = Decimal.Round(model.PatronoRotrasInstituciones, 3);
            model.PatronoLpt = Decimal.Round(model.PatronoLpt, 3);
        }
        public enum PagosMessageId
        {
            AddPaymentSuccess,
            Error
        }
        #endregion
    }
}
