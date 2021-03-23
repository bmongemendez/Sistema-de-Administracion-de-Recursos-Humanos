using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SARH___JMéndez_Constructora.Data;
using SARH___JMéndez_Constructora.Models;
using SARH___JMéndez_Constructora.Models.VacacionesViewModels;

namespace SARH___JMéndez_Constructora.Controllers
{
    public class VacacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VacacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vacaciones
        public async Task<IActionResult> Index(VacacionesMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == VacacionesMessageId.AddVacationsSuccess ? "Se ha agregado el reporte de vacaciones."
                : message == VacacionesMessageId.Error ? "Ha ocurrido un error."
                : "";
            return View(GetVacacionesList());
        }

        // GET: Vacaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacaciones = await _context.Vacaciones
                .Include(v => v.IdEmpleadoNavigation)
                .Include(v => v.IdTiempoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacaciones == null)
            {
                return NotFound();
            }

            return View(vacaciones);
        }

        // GET: Vacaciones/Create
        public IActionResult Create()
        {
            ViewData["IdEmpleado"] = GetTrabajadoresToSelect();
            ViewData["IdTiempo"] = new SelectList(_context.Tiempo, "Id", "Id");
            ViewData["IdContrato"] = new SelectList(_context.Ingresocontrato, "Id", "Id");
            return View();
        }

        // POST: Vacaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VacacionesFormViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(vacaciones);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            if (!ModelState.IsValid)
            {
                ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "Id", "Apellido1", model.IdEmpleado);
                ViewData["IdTiempo"] = new SelectList(_context.Tiempo, "Id", "Id", model.IdTiempo);
                ViewData["IdContrato"] = new SelectList(_context.Ingresocontrato, "Id", "Id");
                return View(model);
            }

            if (InsertVacations(BindModel(model)
                , BindTiempoModel(model)) == 1)

                return RedirectToAction(nameof(Index), new { Message = VacacionesMessageId.AddVacationsSuccess });

            return RedirectToAction(nameof(Index), new { Message = VacacionesMessageId.Error });

        }

        // GET: Vacaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacaciones = await _context.Vacaciones.FindAsync(id);
            if (vacaciones == null)
            {
                return NotFound();
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "Id", "Apellido1", vacaciones.IdEmpleado);
            ViewData["IdTiempo"] = new SelectList(_context.Tiempo, "Id", "Id", vacaciones.IdTiempo);
            return View(vacaciones);
        }

        // POST: Vacaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdEmpleado,FueronAprobadas,Notas,IdTiempo")] Vacaciones vacaciones)
        {
            if (id != vacaciones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacaciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacacionesExists(vacaciones.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "Id", "Apellido1", vacaciones.IdEmpleado);
            ViewData["IdTiempo"] = new SelectList(_context.Tiempo, "Id", "Id", vacaciones.IdTiempo);
            return View(vacaciones);
        }

        // GET: Vacaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacaciones = await _context.Vacaciones
                .Include(v => v.IdEmpleadoNavigation)
                .Include(v => v.IdTiempoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacaciones == null)
            {
                return NotFound();
            }

            return View(vacaciones);
        }

        // POST: Vacaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacaciones = await _context.Vacaciones.FindAsync(id);
            _context.Vacaciones.Remove(vacaciones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacacionesExists(int id)
        {
            return _context.Vacaciones.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult GetIdContrato(string idEmpleado)
        {
            if (string.IsNullOrEmpty(idEmpleado))
                return Json(new { idContrato = 0, salarioHora = 0 });

            int id = int.Parse(idEmpleado);
            int idContrato = GetContrato(id);
            return Json(new { idContrato = idContrato});
        }

        private int GetContrato(int idEmpleado)
        {
            return _context.Ingresocontrato.SingleOrDefault(
                i => (i.IdEmpleado == idEmpleado && i.Fincontrato == null)
            ).Id;
        }

        private int InsertVacations(Vacaciones model, Tiempo modelTiempo)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.Tiempo.Add(modelTiempo);
                _context.SaveChanges();

                // Agregar el id tiempo registrado
                //Se le asigna el id aquí porque hasta hacer el SaveChanges() se le crea el ID
                model.IdTiempo = modelTiempo.Id;

                _context.Vacaciones.Add(model);
                _context.SaveChanges();

                transaction.Commit();

                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }


        private Vacaciones BindModel(VacacionesFormViewModel model)
        {
            Vacaciones vacacionesModel = new Vacaciones();
            vacacionesModel.IdEmpleado = model.IdEmpleado;
            vacacionesModel.FueronAprobadas = model.FueronAprobadas;
            vacacionesModel.Notas = model.Notas;

            return vacacionesModel;
        }

        private Tiempo BindTiempoModel(VacacionesFormViewModel model)
        {
            return new Tiempo
            {
                IdEmpleado = model.IdEmpleado,
                IdContrato = model.IdContrato,
                EsVacaciones = true,
                FechaInicio = model.FechaInicio,
                FechaFin = model.FechaFin
            };
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
            var auxEmpleadosActivos = _context.Empleados
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
        private IEnumerable<TableViewModel> GetVacacionesList()
        {
            List<TableViewModel> list = new List<TableViewModel>();
            var queryResult = _context.Vacaciones
                .Include(v => v.IdEmpleadoNavigation)
                .Include(v => v.IdTiempoNavigation)
                .ToList();

            foreach (var vacaciones in queryResult)
            {
                list.Add(new TableViewModel
                {
                    Cedula = vacaciones.IdEmpleadoNavigation.Cedula,
                    Nombre = vacaciones.IdEmpleadoNavigation.Nombre,
                    Apellido1 = vacaciones.IdEmpleadoNavigation.Apellido1,
                    Apellido2 = vacaciones.IdEmpleadoNavigation.Apellido2,
                    Desde = vacaciones.IdTiempoNavigation.FechaInicio,
                    Hasta = vacaciones.IdTiempoNavigation.FechaFin,
                    Observaciones = vacaciones.Notas,
                    FueronAprobadas = vacaciones.FueronAprobadas
                });
            }
            return list;
        }
        public enum VacacionesMessageId
        {
            AddVacationsSuccess,
            Error
        }

    }
}
