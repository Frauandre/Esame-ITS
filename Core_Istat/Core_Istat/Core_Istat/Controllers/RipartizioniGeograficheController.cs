using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core_Istat.Models;
using Microsoft.AspNetCore.Authorization;

namespace Core_Istat.Controllers
{
    public class RipartizioniGeograficheController : Controller
    {
        private readonly IstatContext _context;

        public RipartizioniGeograficheController(IstatContext context)
        {
            _context = context;
        }

        // GET: RipartizioniGeografiche
        public async Task<IActionResult> Index()
        {
              return _context.RipartizioneGeograficas != null ? 
                          View(await _context.RipartizioneGeograficas.ToListAsync()) :
                          Problem("Entity set 'IstatContext.RipartizioneGeograficas'  is null.");
        }

        // GET: RipartizioniGeografiche/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RipartizioneGeograficas == null)
            {
                return NotFound();
            }

            var ripartizioneGeografica = await _context.RipartizioneGeograficas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ripartizioneGeografica == null)
            {
                return NotFound();
            }

            return View(ripartizioneGeografica);
        }

        [Authorize]
        // GET: RipartizioniGeografiche/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RipartizioniGeografiche/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Denominazione")] RipartizioneGeografica ripartizioneGeografica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ripartizioneGeografica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ripartizioneGeografica);
        }

        [Authorize]
        // GET: RipartizioniGeografiche/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RipartizioneGeograficas == null)
            {
                return NotFound();
            }

            var ripartizioneGeografica = await _context.RipartizioneGeograficas.FindAsync(id);
            if (ripartizioneGeografica == null)
            {
                return NotFound();
            }
            return View(ripartizioneGeografica);
        }

        // POST: RipartizioniGeografiche/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Denominazione")] RipartizioneGeografica ripartizioneGeografica)
        {
            if (id != ripartizioneGeografica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ripartizioneGeografica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RipartizioneGeograficaExists(ripartizioneGeografica.Id))
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
            return View(ripartizioneGeografica);
        }

        [Authorize]
        // GET: RipartizioniGeografiche/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RipartizioneGeograficas == null)
            {
                return NotFound();
            }

            var ripartizioneGeografica = await _context.RipartizioneGeograficas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ripartizioneGeografica == null)
            {
                return NotFound();
            }

            return View(ripartizioneGeografica);
        }

        // POST: RipartizioniGeografiche/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RipartizioneGeograficas == null)
            {
                return Problem("Entity set 'IstatContext.RipartizioneGeograficas'  is null.");
            }
            var ripartizioneGeografica = await _context.RipartizioneGeograficas.FindAsync(id);
            if (ripartizioneGeografica != null)
            {
                _context.RipartizioneGeograficas.Remove(ripartizioneGeografica);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RipartizioneGeograficaExists(int id)
        {
          return (_context.RipartizioneGeograficas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
