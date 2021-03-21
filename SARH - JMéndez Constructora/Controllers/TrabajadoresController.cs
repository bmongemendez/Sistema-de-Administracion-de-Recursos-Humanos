using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SARH___JMéndez_Constructora.Data;
using SARH___JMéndez_Constructora.Models;
using SARH___JMéndez_Constructora.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SARH___JMéndez_Constructora.Controllers
{
    [Authorize]
    public class TrabajadoresController : Controller
    {
        private readonly ApplicationDbContext _appContext;
        public TrabajadoresController(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }
        [HttpGet]
        public IActionResult Index(TrabajadoresMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == TrabajadoresMessageId.AddEmployeeSuccess ? "Se ha agregado al empleado."
                : message == TrabajadoresMessageId.Error ? "Ha ocurrido un error."
                : "";

            TrabajadorViewModel trabajadorViewModel = new TrabajadorViewModel
            {
                trabajadoresList = GetTrabajadoresList(),
                puestos = PuestosToSelectList() 
            };
            return View(trabajadorViewModel);
        }
        [HttpPost]
        public IActionResult Index(TrabajadorViewModel model)
        {
            if (!ModelState.IsValid)
            {   
                model.trabajadoresList = GetTrabajadoresList();
                model.puestos = PuestosToSelectList();
                return View(model);
            }
            if (AddEmpleado(model.ingresoTrabajador) == 1)
            {
                return RedirectToAction(nameof(Index), new { Message = TrabajadoresMessageId.AddEmployeeSuccess });
            }
            return RedirectToAction(nameof(Index), new { Message = TrabajadoresMessageId.Error});
        }
        public IActionResult IndexORI()
        {
            return View();
        }
        #region Metodos
        private TrabajadoresListViewModel GetTrabajadoresList ()
        {
            TrabajadoresListViewModel trabajadoresList;
            List<Empleados> auxEmpleadosActivos = new List<Empleados>();
            List<Empleados> auxEmpleadosInactivos = new List<Empleados>();
                
            // INACTIVOS:
            auxEmpleadosInactivos = _appContext.Empleados
                .Where(e => (e.Ingresocontrato == null || e.Ingresocontrato.All(i => i.Fincontrato != null)))
                .Include(i =>  i.Ingresocontrato)
                    .ThenInclude(f =>  f.Fincontrato)
                .Include(i => i.Ingresocontrato)
                    .ThenInclude(p =>  p.IdPuestoNavigation)
                .ToList();
            
            // ACTIVOS:
            auxEmpleadosActivos = _appContext.Empleados
                .Where(e => (e.Ingresocontrato != null && e.Ingresocontrato.Any(i => i.Fincontrato == null)))
                .Include(e =>  e.Ingresocontrato)
                    .ThenInclude(p =>  p.IdPuestoNavigation)
                .Include(f =>  f.Ingresocontrato)
                .ToList();
            trabajadoresList = new TrabajadoresListViewModel
            {
                empleadosActivos = auxEmpleadosActivos,
                empleadosInactivos = auxEmpleadosInactivos
            };
            return trabajadoresList;
        }
        private IEnumerable<SelectListItem> PuestosToSelectList()
        {
            IEnumerable<SelectListItem> puestosSelectList;
            List<SelectListItem> puestosSelectItem = new List<SelectListItem>();
            puestosSelectItem.Add(new SelectListItem()
            {
                Value = "-1",
                Text = "Seleccione un puesto",
                Selected = true,
                Disabled = true
            });
            
            foreach (Puestos puesto in _appContext.Puestos.ToList())
            {

                puestosSelectItem.Add(new SelectListItem()
                {
                    Value = puesto.Id.ToString(),
                    Text = puesto.Siglas + " - " +puesto.Nombre
                });
            }

            puestosSelectList = puestosSelectItem;
            return puestosSelectList;
        }
        private int AddEmpleado (IngresoTrabajadorFormViewModel model)
        {
            using var transaction = _appContext.Database.BeginTransaction();

            try
            {
                Empleados empleado = new Empleados {
                        Cedula = model.Cedula,
                        Nombre = model.Nombre,
                        Apellido1 = model.Apellido1,
                        Apellido2 = model.Apellido2,
                        FechaNacimiento = model.FechaNacimiento,
                        Telefono = model.Telefono,
                        TelefonoEmergencia = model.TelefonoEmergencia,
                        ContactoEmergencia = model.ContactoEmergencia,
                        TieneBachiller = model.TieneBachiller,
                        TieneLicenciatura = model.TieneLicenciatura,
                        TieneTecnico = model.TieneTecnico,
                        TieneLicenciaA3 = model.TieneLicenciaA3,
                        TieneLicenciaB1 = model.TieneLicenciaB1,
                        //Falta B2,B3,D,E
                        UserName = "rrhh"
                    };
                
                _appContext.Empleados.Add(empleado);
                _appContext.SaveChanges();

                _appContext.Ingresocontrato.Add(new Ingresocontrato {
                        IdEmpleado = empleado.Id,
                        Inicio = model.Inicio,
                        IdPuesto = int.Parse(model.IdPuesto),
                        SalarioDefinidoDia = model.SalarioDefinidoDia,
                        CargoEspecifico = model.CargoEspecifico
                    });
                _appContext.SaveChanges();
                
                transaction.Commit();

                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
            
        }
        #endregion
        public enum TrabajadoresMessageId
        {
            AddEmployeeSuccess,
            Error
        }
    }
}
