using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core_Istat.Models;

namespace Core_Istat.Controllers
{
    public class ProvincieController : Controller
    {
        private readonly IstatContext _context;

        public ProvincieController(IstatContext context)
        {
            _context = context;
        }

        // GET: Provincie
        public async Task<IActionResult> Index()
        {
            var istatContext = _context.Provincia.Include(p => p.IdRegioneNavigation);
            return View(await istatContext.ToListAsync());
        }

        // GET: Provincie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Provincia == null)
            {
                return NotFound();
            }

            var provincium = await _context.Provincia
                .Include(p => p.IdRegioneNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provincium == null)
            {
                return NotFound();
            }

            return View(provincium);
        }

        // GET: Provincie/Create
        public IActionResult Create()
        {
            ViewData["IdRegione"] = new SelectList(_context.Regiones, "Id", "Id");
            return View();
        }

        // POST: Provincie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Denominazione,Sigla,CodiceCittaMetropolitana,IdRegione")] Provincium provincium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provincium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRegione"] = new SelectList(_context.Regiones, "Id", "Id", provincium.IdRegione);
            return View(provincium);
        }

        // GET: Provincie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Provincia == null)
            {
                return NotFound();
            }

            var provincium = await _context.Provincia.FindAsync(id);
            if (provincium == null)
            {
                return NotFound();
            }
            ViewData["IdRegione"] = new SelectList(_context.Regiones, "Id", "Id", provincium.IdRegione);
            return View(provincium);
        }

        // POST: Provincie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Denominazione,Sigla,CodiceCittaMetropolitana,IdRegione")] Provincium provincium)
        {
            if (id != provincium.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provincium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvinciumExists(provincium.Id))
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
            ViewData["IdRegione"] = new SelectList(_context.Regiones, "Id", "Id", provincium.IdRegione);
            return View(provincium);
        }

        // GET: Provincie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Provincia == null)
            {
                return NotFound();
            }

            var provincium = await _context.Provincia
                .Include(p => p.IdRegioneNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provincium == null)
            {
                return NotFound();
            }

            return View(provincium);
        }

        // POST: Provincie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Provincia == null)
            {
                return Problem("Entity set 'IstatContext.Provincia'  is null.");
            }
            var provincium = await _context.Provincia.FindAsync(id);
            if (provincium != null)
            {
                _context.Provincia.Remove(provincium);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvinciumExists(int id)
        {
          return (_context.Provincia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
