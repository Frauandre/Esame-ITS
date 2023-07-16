using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core_Istat.Models;
using System.Drawing;

namespace Core_Istat.Controllers
{
    public class RegioniController : Controller
    {
        private readonly IstatContext _context;

        public RegioniController(IstatContext context)
        {
            _context = context;
        }

        // GET: Regioni
        public async Task<IActionResult> Index(string searchRipartizione)
        {
            var istatContext = _context.Regiones.Include(r => r.IdRipartizioneNavigation);

            if (searchRipartizione != null)
                istatContext = _context.Regiones                    
                    .Where(r => r.IdRipartizioneNavigation.Denominazione.Contains(searchRipartizione))
                    .Include(r => r.IdRipartizioneNavigation);


            return View(await istatContext.ToListAsync());
        }

        //GET: Provincie in regione
        public async Task<IActionResult> ElencoProvinciePerRegione(int id)
        {
            //id = id regione
            if (id == null || _context.Provincia == null)
            {
                return NotFound();
            }
            
            var elenco = _context.Provincia.Where(p => p.IdRegione == id);               
                
            if (elenco == null)
            {
                return NotFound();
            }

            return View(elenco);
        }

        // GET: Regioni/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Regiones == null)
            {
                return NotFound();
            }

            var regione = await _context.Regiones
                .Include(r => r.IdRipartizioneNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (regione == null)
            {
                return NotFound();
            }

            return View(regione);
        }

        // GET: Regioni/Create
        public IActionResult Create()
        {
            ViewData["IdRipartizione"] = new SelectList(_context.RipartizioneGeograficas, "Id", "Id");
            return View();
        }

        // POST: Regioni/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Denominazione,IdRipartizione")] Regione regione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(regione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRipartizione"] = new SelectList(_context.RipartizioneGeograficas, "Id", "Id", regione.IdRipartizione);
            return View(regione);
        }

        // GET: Regioni/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Regiones == null)
            {
                return NotFound();
            }

            var regione = await _context.Regiones.FindAsync(id);
            if (regione == null)
            {
                return NotFound();
            }
            ViewData["IdRipartizione"] = new SelectList(_context.RipartizioneGeograficas, "Id", "Id", regione.IdRipartizione);
            return View(regione);
        }

        // POST: Regioni/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Denominazione,IdRipartizione")] Regione regione)
        {
            if (id != regione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegioneExists(regione.Id))
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
            ViewData["IdRipartizione"] = new SelectList(_context.RipartizioneGeograficas, "Id", "Id", regione.IdRipartizione);
            return View(regione);
        }

        // GET: Regioni/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Regiones == null)
            {
                return NotFound();
            }

            var regione = await _context.Regiones
                .Include(r => r.IdRipartizioneNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (regione == null)
            {
                return NotFound();
            }

            return View(regione);
        }

        // POST: Regioni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Regiones == null)
            {
                return Problem("Entity set 'IstatContext.Regiones'  is null.");
            }
            var regione = await _context.Regiones.FindAsync(id);
            if (regione != null)
            {
                _context.Regiones.Remove(regione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegioneExists(int id)
        {
          return (_context.Regiones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
