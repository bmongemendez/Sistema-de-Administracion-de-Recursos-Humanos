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
    public class TiempoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TiempoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tiempoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tiempo.Include(t => t.IdContratoNavigation).Include(t => t.IdEmpleadoNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tiempoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiempo = await _context.Tiempo
                .Include(t => t.IdContratoNavigation)
                .Include(t => t.IdEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tiempo == null)
            {
                return NotFound();
            }

            return View(tiempo);
        }

        // GET: Tiempoes/Create
        public IActionResult Create()
        {
            ViewData["IdContrato"] = new SelectList(_context.Ingresocontrato, "Id", "Id");
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "Id", "Apellido1");
            return View();
        }

        // POST: Tiempoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdEmpleado,FechaInicio,FechaFin,IdContrato,EsLaborado,EsInjustificado,EsVacaciones,EsIncapacidad")] Tiempo tiempo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tiempo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdContrato"] = new SelectList(_context.Ingresocontrato, "Id", "Id", tiempo.IdContrato);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "Id", "Apellido1", tiempo.IdEmpleado);
            return View(tiempo);
        }

        // GET: Tiempoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiempo = await _context.Tiempo.FindAsync(id);
            if (tiempo == null)
            {
                return NotFound();
            }
            ViewData["IdContrato"] = new SelectList(_context.Ingresocontrato, "Id", "Id", tiempo.IdContrato);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "Id", "Apellido1", tiempo.IdEmpleado);
            return View(tiempo);
        }

        // POST: Tiempoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdEmpleado,FechaInicio,FechaFin,IdContrato,EsLaborado,EsInjustificado,EsVacaciones,EsIncapacidad")] Tiempo tiempo)
        {
            if (id != tiempo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiempo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiempoExists(tiempo.Id))
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
            ViewData["IdContrato"] = new SelectList(_context.Ingresocontrato, "Id", "Id", tiempo.IdContrato);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "Id", "Apellido1", tiempo.IdEmpleado);
            return View(tiempo);
        }

        // GET: Tiempoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiempo = await _context.Tiempo
                .Include(t => t.IdContratoNavigation)
                .Include(t => t.IdEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tiempo == null)
            {
                return NotFound();
            }

            return View(tiempo);
        }

        // POST: Tiempoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tiempo = await _context.Tiempo.FindAsync(id);
            _context.Tiempo.Remove(tiempo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiempoExists(int id)
        {
            return _context.Tiempo.Any(e => e.Id == id);
        }
    }
}
