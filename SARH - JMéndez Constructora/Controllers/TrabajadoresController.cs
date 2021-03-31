using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SARH___JMéndez_Constructora.Data;
using SARH___JMéndez_Constructora.Models;
using SARH___JMéndez_Constructora.Models.TrabajadoresViewModels;
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
        //
        // GET: Details/5
        [HttpGet]
        [Route("Trabajadores/Details/{id}")]
        public IActionResult Details(int id)
        {
            return RedirectToAction(nameof(Basic), new { id = id });
        }
        // GET: Basic/5
        [HttpGet]
        [Route("Trabajadores/Details/Basic/{id}")]
        public IActionResult Basic(int id, TrabajadoresMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == TrabajadoresMessageId.UpdateEmployeeSuccess ? "Se ha actualizado el empleado."
                : message == TrabajadoresMessageId.Error ? "Ha ocurrido un error."
                : "";
            return ReturnCurrentTrabajador(id, nameof(Basic));
        }
        // 
        // POST: Basic/
        [HttpPost]
        [Route("Trabajadores/Details/Basic/{IdEmpleado}")]
        public IActionResult Basic(int IdEmpleado, BasicFormViewModel model)
        {
            if (!ModelState.IsValid)
            {   
                return View(model);
            }
            if (UpdateBasicTrabajador(model) == 1)
            {
                return RedirectToAction(nameof(Basic), new { id = model.IdEmpleado, Message = TrabajadoresMessageId.UpdateEmployeeSuccess });
            }
            return RedirectToAction(nameof(Basic), new { Message = TrabajadoresMessageId.Error});
        }
        //
        // GET: EdccnLicncs/5
        [HttpGet]
        [Route("Trabajadores/Details/EdccnLicncs/{id}")]
        public IActionResult EdccnLicncs(int id, TrabajadoresMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == TrabajadoresMessageId.UpdateEmployeeSuccess ? "Se ha actualizado el empleado."
                : message == TrabajadoresMessageId.Error ? "Ha ocurrido un error."
                : "";
            return ReturnCurrentTrabajador(id, nameof(EdccnLicncs));
        }
        // 
        // POST: EdccnLicncs/
        [HttpPost]
        [Route("Trabajadores/Details/EdccnLicncs/{IdEmpleado}")]
        public IActionResult EdccnLicncs(int IdEmpleado, Edcn_n_LicncFormViewModel model)
        {
            if (!ModelState.IsValid)
            return View(model);
            
            if (UpdateEdcn_n_LicncFormTrabajador(model) == 1)
            {
                return RedirectToAction(nameof(EdccnLicncs), new { id = model.IdEmpleado, Message = TrabajadoresMessageId.UpdateEmployeeSuccess });
            }
            return RedirectToAction(nameof(EdccnLicncs), new { Message = TrabajadoresMessageId.Error});
        }
        //
        // GET: Contract/5
        [HttpGet]
        [Route("Trabajadores/Details/Contract/{id}")]
        public IActionResult Contract(int id, TrabajadoresMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == TrabajadoresMessageId.UpdateEmployeeSuccess ? "Se ha actualizado el empleado."
                : message == TrabajadoresMessageId.Error ? "Ha ocurrido un error."
                : "";
            return ReturnCurrentTrabajador(id, nameof(Contract));
        }
        [HttpPost]
        [Route("Trabajadores/Details/Contract/{IdEmpleado}")]
        public IActionResult Contract(int IdEmpleado, ContractFormViewModel model)
        {
            if (!ModelState.IsValid)
            return View(model);
            
            if(!ContractExist(model.IdEmpleado))
            if(AddContractOnly(model) == 1)
            return RedirectToAction(nameof(Contract), new { id = model.IdEmpleado, Message = TrabajadoresMessageId.UpdateEmployeeSuccess });

            if (UpdateContractTrabajador(model) == 1)
            return RedirectToAction(nameof(Contract), new { id = model.IdEmpleado, Message = TrabajadoresMessageId.UpdateEmployeeSuccess });
            
            return RedirectToAction(nameof(Contract), new { Message = TrabajadoresMessageId.Error});
        }
        private IActionResult ReturnCurrentTrabajador(int id, string actionName)
        {
            ViewData["Empleado"] = getTrabajadorPartial(id);
            switch (actionName)
            {
                case nameof(Basic):
                    BasicFormViewModel modelA = GetTrabajadorBasic(id);
                    if (modelA != null)
                    return View(nameof(Basic), modelA);
                    break;
                case nameof(EdccnLicncs):
                    Edcn_n_LicncFormViewModel modelB = GetTrabajadorEdcn_n_Licnc(id);
                    if (modelB != null)
                    return View(nameof(EdccnLicncs), modelB);
                    break;
                case nameof(Contract):
                    ContractFormViewModel modelC = GetTrabajadorContract(id);
                    if (modelC != null)
                    {
                        ViewData["puestos"] = PuestosToSelectList();
                        return View(nameof(Contract), modelC);
                    }
                    break;
                default:
                    return RedirectToAction(nameof(Index), new { Message = TrabajadoresMessageId.Error});
            }
            return RedirectToAction(nameof(Index), new { Message = TrabajadoresMessageId.Error});
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
                        UserName = User.Identity.Name
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
        private BasicFormViewModel GetTrabajadorBasic(int id)
        {
            BasicFormViewModel trabajador;
            var queryResult = _appContext.Empleados
                .Include(e => e.Ingresocontrato)
                .SingleOrDefault(e => e.Id == id);

            if (queryResult == null)
            return null;
            
            trabajador = new BasicFormViewModel
            {
                IdEmpleado = queryResult.Id,
                Cedula = queryResult.Cedula,
                Nombre = queryResult.Nombre,
                Apellido1 = queryResult.Apellido1,
                Apellido2 = queryResult.Apellido2,
                FechaNacimiento = queryResult.FechaNacimiento,
                Telefono = queryResult.Telefono,
                TelefonoEmergencia = queryResult.TelefonoEmergencia,
                ContactoEmergencia = queryResult.ContactoEmergencia
            };
            
            return trabajador;  
        }

        private Edcn_n_LicncFormViewModel GetTrabajadorEdcn_n_Licnc(int id)
        {
            Edcn_n_LicncFormViewModel trabajador;
            var queryResult = _appContext.Empleados
                .Include(e => e.Ingresocontrato)
                .SingleOrDefault(e => e.Id == id);

            if (queryResult == null)
            return null;

            trabajador = new Edcn_n_LicncFormViewModel
            {
                IdEmpleado = queryResult.Id,
                TieneBachiller = (bool)queryResult.TieneBachiller,
                TieneLicenciatura = (bool)queryResult.TieneLicenciatura,
                TieneTecnico = (bool)queryResult.TieneTecnico,
                TieneLicenciaA3 = (bool)queryResult.TieneLicenciaA3,
                TieneLicenciaB1 = (bool)queryResult.TieneLicenciaB1,
                TieneLicenciaB2 = (bool)queryResult.TieneLicenciaB2,
                TieneLicenciaB3 = (bool)queryResult.TieneLicenciaB3,
                TieneLicenciaD = (bool)queryResult.TieneLicenciaD,
                TieneLicenciaE = (bool)queryResult.TieneLicenciaE
            };
            return trabajador;
        }
        private ContractFormViewModel GetTrabajadorContract(int id)
        {
            ContractFormViewModel trabajador;
            var queryResult = _appContext.Empleados
                .Include(e => e.Ingresocontrato)
                .SingleOrDefault(e => e.Id == id);

            if (queryResult == null)
            return null;
            Ingresocontrato ingresocontrato = new Ingresocontrato();
            ingresocontrato = queryResult.Ingresocontrato
                .SingleOrDefault(q => q.Fincontrato == null);

            trabajador = new ContractFormViewModel
            {
                IdEmpleado = queryResult.Id,
                Inicio = DateTime.Now.Date
            };

            if (ingresocontrato == null)
            return trabajador;


            trabajador.IdPuesto = ingresocontrato.IdPuesto.ToString();
            trabajador.Inicio = ingresocontrato.Inicio;
            trabajador.SalarioDefinidoDia = ingresocontrato.SalarioDefinidoDia;
            trabajador.CargoEspecifico = ingresocontrato.CargoEspecifico;
            

            return trabajador;  
        }
        private PartialDetailsViewModel getTrabajadorPartial (int id)
        {
            PartialDetailsViewModel trabajador;
            var queryResult = _appContext.Empleados
                .Include(e => e.Ingresocontrato)
                    .ThenInclude(i => i.IdPuestoNavigation)
                .SingleOrDefault(e => e.Id == id);

            if (queryResult == null)
            return null;
            Ingresocontrato ingresocontrato = new Ingresocontrato();
            ingresocontrato = queryResult.Ingresocontrato
                .SingleOrDefault(q => q.Fincontrato == null);
            
            trabajador = new PartialDetailsViewModel
            {
                Id = queryResult.Id,
                Cedula = queryResult.Cedula,
                Nombre = queryResult.Nombre,
                Apellido1 = queryResult.Apellido1,
                Apellido2 = queryResult.Apellido2
            };

            if(ingresocontrato != null)
            trabajador.Puesto = ingresocontrato.IdPuestoNavigation.Nombre;

            return trabajador; 
        }
        private int UpdateBasicTrabajador(BasicFormViewModel model)
        {
            try
            {
                Empleados aux = _appContext.Empleados.Find(model.IdEmpleado);
                
                aux.Cedula = model.Cedula;
                aux.Nombre = model.Nombre;
                aux.Apellido1 = model.Apellido1;
                aux.Apellido2 = model.Apellido2;
                aux.FechaNacimiento = model.FechaNacimiento;
                aux.Telefono = model.Telefono;
                aux.TelefonoEmergencia = model.TelefonoEmergencia;
                aux.ContactoEmergencia = model.ContactoEmergencia;
                aux.UserName = User.Identity.Name;
                
                _appContext.Update(aux);
                
                return _appContext.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }
        private int UpdateEdcn_n_LicncFormTrabajador(Edcn_n_LicncFormViewModel model)
        {
            try
            {
                Empleados aux = _appContext.Empleados.Find(model.IdEmpleado);
                
                aux.Id = model.IdEmpleado;
                aux.TieneBachiller = (bool)model.TieneBachiller;
                aux.TieneLicenciatura = (bool)model.TieneLicenciatura;
                aux.TieneTecnico = (bool)model.TieneTecnico;
                aux.TieneLicenciaA3 = (bool)model.TieneLicenciaA3;
                aux.TieneLicenciaB1 = (bool)model.TieneLicenciaB1;
                aux.TieneLicenciaB2 = (bool)model.TieneLicenciaB2;
                aux.TieneLicenciaB3 = (bool)model.TieneLicenciaB3;
                aux.TieneLicenciaD = (bool)model.TieneLicenciaD;
                aux.TieneLicenciaE = (bool)model.TieneLicenciaE;
                aux.UserName = User.Identity.Name;
                
                _appContext.Update(aux);
                
                return _appContext.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }
        private int UpdateContractTrabajador(ContractFormViewModel model)
        {
            try
            {
                Ingresocontrato aux = _appContext.Ingresocontrato
                    .Single(i => (i.IdEmpleado == model.IdEmpleado 
                        && i.Fincontrato == null));
                
                aux.Inicio = model.Inicio;
                aux.IdPuesto = int.Parse(model.IdPuesto);
                aux.SalarioDefinidoDia = model.SalarioDefinidoDia;
                aux.CargoEspecifico = model.CargoEspecifico;
                
                _appContext.Update(aux);
                
                return _appContext.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }
        private int AddContractOnly(ContractFormViewModel model)
        {
            try
            {
                _appContext.Ingresocontrato.Add(new Ingresocontrato {
                        IdEmpleado = model.IdEmpleado,
                        Inicio = model.Inicio,
                        IdPuesto = int.Parse(model.IdPuesto),
                        SalarioDefinidoDia = model.SalarioDefinidoDia,
                        CargoEspecifico = model.CargoEspecifico
                    });
                
                return _appContext.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }
        private bool ContractExist(int idEmpleado)
        {
            return (_appContext.Ingresocontrato
                    .SingleOrDefault(i => (i.IdEmpleado == idEmpleado 
                        && i.Fincontrato == null)) != null);
        }
        #endregion
        public enum TrabajadoresMessageId
        {
            AddEmployeeSuccess,
            UpdateEmployeeSuccess,
            Error
        }
    }
}
