using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SARH___JMéndez_Constructora.Data;
using SARH___JMéndez_Constructora.Models;

namespace SARH___JMéndez_Constructora.Controllers
{
    public class EvaluacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Evaluaciones
        public async Task<IActionResult> Index(AIMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                 message == AIMessageId.AddEvaluacionesSuccess ? "Se ha agregado el reporte de evaluaciones"
                 : message == AIMessageId.Error ? "Ha ocurrido un error."
                 : "";
            var applicationDbContext = _context.Evaluaciones.Include(e => e.IdEmpleadoNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Evaluaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluaciones = await _context.Evaluaciones
                .Include(e => e.IdEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluaciones == null)
            {
                return NotFound();
            }

            return View(evaluaciones);
        }

        // GET: Evaluaciones/Create
        public IActionResult Create()
        {
            ViewData["IdEmpleado"] = GetTrabajadoresToSelect();
            return View();
        }

        // POST: Evaluaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdEmpleado,Calificacion,Observaciones")] Evaluaciones evaluaciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluaciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { Message = AIMessageId.AddEvaluacionesSuccess});
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "Id", "Apellido1", evaluaciones.IdEmpleado);
            return RedirectToAction(nameof(Index), new { Message = AIMessageId.Error });
        }

        // GET: Evaluaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluaciones = await _context.Evaluaciones.FindAsync(id);
            if (evaluaciones == null)
            {
                return NotFound();
            }
            ViewData["IdEmpleado"] = GetTrabajadoresToSelect();
            return View(evaluaciones);
        }

        // POST: Evaluaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdEmpleado,Calificacion,Observaciones")] Evaluaciones evaluaciones)
        {
            if (id != evaluaciones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluaciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluacionesExists(evaluaciones.Id))
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
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "Id", "Apellido1", evaluaciones.IdEmpleado);
            return View(evaluaciones);
        }

        // GET: Evaluaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluaciones = await _context.Evaluaciones
                .Include(e => e.IdEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluaciones == null)
            {
                return NotFound();
            }

            return View(evaluaciones);
        }

        // POST: Evaluaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evaluaciones = await _context.Evaluaciones.FindAsync(id);
            _context.Evaluaciones.Remove(evaluaciones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluacionesExists(int id)
        {
            return _context.Evaluaciones.Any(e => e.Id == id);
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
        public enum AIMessageId
        {
            AddEvaluacionesSuccess,
            Error
        }

    }
}
