using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SARH___JMéndez_Constructora.Data;
using SARH___JMéndez_Constructora.Models;
using SARH___JMéndez_Constructora.Models.AusenciasInjustificadasViewModels;

namespace SARH___JMéndez_Constructora.Controllers
{
    public class AusenciasinjustificadasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AusenciasinjustificadasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ausenciasinjustificadas
        public IActionResult Index(AIMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                  message == AIMessageId.AddAISuccess ? "Se ha agregado el reporte de ausencias injustificadas."
                  : message == AIMessageId.Error ? "Ha ocurrido un error."
                  : "";
            return View(GetAIList());
        }

        // GET: Ausenciasinjustificadas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ausenciasinjustificadas = await _context.Ausenciasinjustificadas
                .Include(a => a.IdEmpleadoNavigation)
                .Include(a => a.IdTiempoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ausenciasinjustificadas == null)
            {
                return NotFound();
            }

            return View(ausenciasinjustificadas);
        }

        // GET: Ausenciasinjustificadas/Create
        public IActionResult Create()
        {
            ViewData["IdEmpleado"] = GetTrabajadoresToSelect();
            ViewData["IdTiempo"] = new SelectList(_context.Tiempo, "Id", "Id");
            return View();
        }

        // POST: Ausenciasinjustificadas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(AIFormViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (InsertVacations(BindModel(model)
                    , BindTiempoModel(model)) == 1)

                    return RedirectToAction(nameof(Index), new { Message = AIMessageId.AddAISuccess });

                return RedirectToAction(nameof(Index), new { Message = AIMessageId.Error });
            }
            ViewData["IdEmpleado"] = GetTrabajadoresToSelect();
            ViewData["IdTiempo"] = new SelectList(_context.Tiempo, "Id", "Id", model.IdTiempo);
            return View(model);

        }

        // GET: Ausenciasinjustificadas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ausenciasinjustificadas = await _context.Ausenciasinjustificadas.FindAsync(id);
            if (ausenciasinjustificadas == null)
            {
                return NotFound();
            }
            ViewData["IdEmpleado"] = GetTrabajadoresToSelect();
            ViewData["IdTiempo"] = new SelectList(_context.Tiempo, "Id", "Id", ausenciasinjustificadas.IdTiempo);
            return View(ausenciasinjustificadas);
        }

        // POST: Ausenciasinjustificadas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdEmpleado,IdTiempo,Notas")] Ausenciasinjustificadas ausenciasinjustificadas)
        {
            if (id != ausenciasinjustificadas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ausenciasinjustificadas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AusenciasinjustificadasExists(ausenciasinjustificadas.Id))
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
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "Id", "Apellido1", ausenciasinjustificadas.IdEmpleado);
            ViewData["IdTiempo"] = new SelectList(_context.Tiempo, "Id", "Id", ausenciasinjustificadas.IdTiempo);
            return View(ausenciasinjustificadas);
        }

        // GET: Ausenciasinjustificadas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ausenciasinjustificadas = await _context.Ausenciasinjustificadas
                .Include(a => a.IdEmpleadoNavigation)
                .Include(a => a.IdTiempoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ausenciasinjustificadas == null)
            {
                return NotFound();
            }

            return View(ausenciasinjustificadas);
        }

        // POST: Ausenciasinjustificadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int idTiempo)
        {
            var ausenciasinjustificadas = await _context.Ausenciasinjustificadas.FindAsync(id);
            var ausenciasinjustificadasTiempo = await _context.Tiempo.FindAsync(idTiempo);
            _context.Ausenciasinjustificadas.Remove(ausenciasinjustificadas);
            _context.Tiempo.Remove(ausenciasinjustificadasTiempo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AusenciasinjustificadasExists(int id)
        {
            return _context.Ausenciasinjustificadas.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult GetIdContrato(string idEmpleado)
        {
            if (string.IsNullOrEmpty(idEmpleado))
                return Json(new { idContrato = 0, salarioHora = 0 });

            int id = int.Parse(idEmpleado);
            int idContrato = GetContrato(id);
            return Json(new { idContrato = idContrato });
        }
        private int GetContrato(int idEmpleado)
        {
            return _context.Ingresocontrato.SingleOrDefault(
                i => (i.IdEmpleado == idEmpleado && i.Fincontrato == null)
            ).Id;
        }
        private IEnumerable<SelectListItem> GetTrabajadoresToSelect()
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
                .Include(e => e.Ingresocontrato)
                    .ThenInclude(p => p.IdPuestoNavigation)
                .Include(f => f.Ingresocontrato)
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

        private int InsertVacations(Ausenciasinjustificadas model, Tiempo modelTiempo)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.Tiempo.Add(modelTiempo);
                _context.SaveChanges();

                // Agregar el id tiempo registrado
                //Se le asigna el id aquí porque hasta hacer el SaveChanges() se le crea el ID
                model.IdTiempo = modelTiempo.Id;

                _context.Ausenciasinjustificadas.Add(model);
                _context.SaveChanges();

                transaction.Commit();

                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private Ausenciasinjustificadas BindModel(AIFormViewModel model)
        {
            Ausenciasinjustificadas AIModel = new Ausenciasinjustificadas();
            AIModel.IdEmpleado = model.IdEmpleado;
            AIModel.Notas = model.Notas;
            return AIModel;
        }

        private Tiempo BindTiempoModel(AIFormViewModel model)
        {
            return new Tiempo
            {
                IdEmpleado = model.IdEmpleado,
                EsInjustificado = true,
                IdContrato = model.IdContrato,
                FechaInicio = model.FechaInicio,
                FechaFin = model.FechaFin
            };
        }

        private IEnumerable<AITableViewModel> GetAIList()
        {
            List<AITableViewModel> list = new List<AITableViewModel>();
            var queryResult = _context.Ausenciasinjustificadas
                .Include(v => v.IdEmpleadoNavigation)
                .Include(v => v.IdTiempoNavigation)
                .ToList();

            foreach (var AI in queryResult)
            {
                list.Add(new AITableViewModel
                {
                    Id = AI.Id,
                    Cedula = AI.IdEmpleadoNavigation.Cedula,
                    Nombre = AI.IdEmpleadoNavigation.Nombre,
                    Apellido1 = AI.IdEmpleadoNavigation.Apellido1,
                    Apellido2 = AI.IdEmpleadoNavigation.Apellido2,
                    Desde = AI.IdTiempoNavigation.FechaInicio,
                    Hasta = AI.IdTiempoNavigation.FechaFin,
                    Notas = AI.Notas,
                    IdTiempo = AI.IdTiempo
                });
            }
            return list;
        }

        public enum AIMessageId
        {
            AddAISuccess,
            Error
        }

    }
}
