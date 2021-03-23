using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SARH___JMéndez_Constructora.Data;
using SARH___JMéndez_Constructora.Models;
using SARH___JMéndez_Constructora.Models.IncapacidadesViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SARH___JMéndez_Constructora.Controllers
{
    
    public class IncapacidadesController : Controller
    {
        private readonly ApplicationDbContext _appContext;

        public IncapacidadesController(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }
        //
        // GET: Incapacidades
        [HttpGet]
        public IActionResult Index(IncapacidadesMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == IncapacidadesMessageId.AddIncapacidadSuccess ? "Se ha agregado la incapacidad."
                : message == IncapacidadesMessageId.Error ? "Ha ocurrido un error."
                : "";
            return View(new IncapacidadesViewModel 
            { 
                Empleados  = GetTrabajadoresToSelect(),
                Tipo = GetTipos(),
                Incapacidades = GetIncapacidades()
            });
        }
        //
        // POST: Incapacidad
        [HttpPost]
        public IActionResult Index(IncapacidadesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Empleados = GetTrabajadoresToSelect();
                model.Tipo = GetTipos();
                model.Incapacidades = GetIncapacidades();
                return View(model);
            }

            if(AgregarIncapacidad(model.Incapacidad) == 1)
            return RedirectToAction(nameof(Index), new { Message = IncapacidadesMessageId.AddIncapacidadSuccess });

            return RedirectToAction(nameof(Index), new { Message = IncapacidadesMessageId.Error });
        }
        // 
        // GET: File
        [HttpGet]
        public ActionResult Adjunto(int id)
        {
            Evidencias evidencia = GetEvidencia(id);
            
            if (evidencia == null)
            return RedirectToAction(nameof(Index));

            string tipo = "." + evidencia.Tipo.Split("/")[1];
            return File(evidencia.Evidencia, "application/force-download"
                , evidencia.Id.ToString()+tipo);
        }
        #region Helpers
        private IEnumerable<IncapacidadesListViewModel> GetIncapacidades()
        {
            List<IncapacidadesListViewModel> list = new List<IncapacidadesListViewModel>();
            var queryResult = _appContext.Tiempo
                .Where(t => t.EsIncapacidad == true)
                .Include(t => t.IdEmpleadoNavigation)
                .Include(t => t.Incapacidades)
                .ToList();
            foreach (var incapacidad in queryResult)
            {
                list.Add(new IncapacidadesListViewModel
                {
                    Cedula = incapacidad.IdEmpleadoNavigation.Cedula,
                    Nombre = incapacidad.IdEmpleadoNavigation.Nombre,
                    Apellido1 = incapacidad.IdEmpleadoNavigation.Apellido1,
                    Apellido2 = incapacidad.IdEmpleadoNavigation.Apellido2,
                    Desde = incapacidad.FechaInicio,
                    Hasta = incapacidad.FechaFin,
                    Tipo = incapacidad.Incapacidades.Tipo,
                    Evidencias = incapacidad.Incapacidades.Evidencias
                });
            }
            return list;
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
        private int AgregarIncapacidad(IncapacidadesFormViewModel model)
        {
            using var transaction = _appContext.Database.BeginTransaction();

            try
            {
                var tiempo = new Tiempo
                {
                    IdEmpleado = model.IdEmpleado,
                    IdContrato = GetContrato(model.IdEmpleado),
                    EsIncapacidad = true,
                    FechaInicio = model.Desde,
                    FechaFin = model.Hasta
                };
                _appContext.Tiempo.Add(tiempo);
                _appContext.SaveChanges();

                var incapacidad = new Incapacidades
                {
                    IdTiempo = tiempo.Id,
                    Tipo = model.Tipo
                };

                _appContext.Incapacidades.Add(incapacidad);
                _appContext.SaveChanges();

                AdjuntarEvidencias(model, incapacidad.Id);
                
                transaction.Commit();
                return 1;
            }
            catch (DbUpdateException)
            {
                transaction.Rollback();
                return -1;
            }
        }
        private int GetContrato(int idEmpleado)
        {
            return _appContext.Ingresocontrato.SingleOrDefault(
                i => (i.IdEmpleado == idEmpleado && i.Fincontrato == null)
            ).Id;
        }
        private void AdjuntarEvidencias(IncapacidadesFormViewModel model, int idIncapacidad) 
        {
            
            if(model.Evidencias != null 
                && model.Evidencias.Count > 0)
            {
                foreach (var file in model.Evidencias)
                {
                    var memoryStream = new MemoryStream();
                    
                    file.CopyTo(memoryStream);
                    if (memoryStream.Length < 2097152)
                    {
                        _appContext.Add( new Evidencias 
                        {
                            IdIncapacidad = idIncapacidad,
                            Evidencia = memoryStream.ToArray(),
                            Tipo = file.ContentType
                        });
                        _appContext.SaveChanges();
                    }
                }
                
            }
            
        }
        private Evidencias GetEvidencia(int id)
        {
            return _appContext.Evidencias.SingleOrDefault( e => e.Id == id );
        }
        private IEnumerable<SelectListItem> GetTipos()
        {
            List<SelectListItem> tiposList = new List<SelectListItem>();
            tiposList.Add(new SelectListItem()
            {
                Text = "Seleccione un tipo",
                Selected = true,
                Disabled = true
            });
            
            tiposList.Add(new SelectListItem()
            {
                Text = "Enfermedad Común",
                Value = "1"
            });

            tiposList.Add(new SelectListItem()
            {
                Text = "Riesgo de trabajo",
                Value = "2"
            });
            return tiposList;
        }
        public enum IncapacidadesMessageId
        {
            AddIncapacidadSuccess,
            Error
        }
        #endregion
    }
}
