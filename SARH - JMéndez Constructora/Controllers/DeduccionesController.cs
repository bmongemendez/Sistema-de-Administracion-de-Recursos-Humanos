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
    public class DeduccionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeduccionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Deducciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Deducciones.ToListAsync());
        }

        // GET: Deducciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deducciones = await _context.Deducciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deducciones == null)
            {
                return NotFound();
            }

            return View(deducciones);
        }

        // GET: Deducciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Deducciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Grupo,Concepto,Patrono,Trabajador")] Deducciones deducciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deducciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deducciones);
        }

        // GET: Deducciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deducciones = await _context.Deducciones.FindAsync(id);
            if (deducciones == null)
            {
                return NotFound();
            }
            return View(deducciones);
        }

        // POST: Deducciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Grupo,Concepto,Patrono,Trabajador")] Deducciones deducciones)
        {
            if (id != deducciones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deducciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeduccionesExists(deducciones.Id))
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
            return View(deducciones);
        }

        // GET: Deducciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deducciones = await _context.Deducciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deducciones == null)
            {
                return NotFound();
            }

            return View(deducciones);
        }

        // POST: Deducciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deducciones = await _context.Deducciones.FindAsync(id);
            _context.Deducciones.Remove(deducciones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeduccionesExists(int id)
        {
            return _context.Deducciones.Any(e => e.Id == id);
        }
    }
}
