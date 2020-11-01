using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SBD.Models;

namespace SBD.Controllers
{
    public class PielegniarkaController : Controller
    {
        private readonly ModelContext _context;

        public PielegniarkaController(ModelContext context)
        {
            _context = context;
        }

        // GET: Pielegniarka
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Pielegniarka.Include(p => p.Osoba);
            return View(await modelContext.ToListAsync());
        }

        // GET: Pielegniarka/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pielegniarka = await _context.Pielegniarka
                .Include(p => p.Osoba)
                .FirstOrDefaultAsync(m => m.Pielegniarkaid == id);
            if (pielegniarka == null)
            {
                return NotFound();
            }

            return View(pielegniarka);
        }

        // GET: Pielegniarka/Create
        public IActionResult Create()
        {
            ViewData["Osobaid"] = new SelectList(_context.Osoba, "Osobaid", "Osobaid");
            return View();
        }

        // POST: Pielegniarka/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pielegniarkaid,Doswiadczenie,Osobaid")] Pielegniarka pielegniarka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pielegniarka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Osobaid"] = new SelectList(_context.Osoba, "Osobaid", "Osobaid", pielegniarka.Osobaid);
            return View(pielegniarka);
        }

        // GET: Pielegniarka/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pielegniarka = await _context.Pielegniarka.FindAsync(id);
            if (pielegniarka == null)
            {
                return NotFound();
            }
            ViewData["Osobaid"] = new SelectList(_context.Osoba, "Osobaid", "Osobaid", pielegniarka.Osobaid);
            return View(pielegniarka);
        }

        // POST: Pielegniarka/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Pielegniarkaid,Doswiadczenie,Osobaid")] Pielegniarka pielegniarka)
        {
            if (id != pielegniarka.Pielegniarkaid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pielegniarka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PielegniarkaExists(pielegniarka.Pielegniarkaid))
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
            ViewData["Osobaid"] = new SelectList(_context.Osoba, "Osobaid", "Osobaid", pielegniarka.Osobaid);
            return View(pielegniarka);
        }

        // GET: Pielegniarka/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pielegniarka = await _context.Pielegniarka
                .Include(p => p.Osoba)
                .FirstOrDefaultAsync(m => m.Pielegniarkaid == id);
            if (pielegniarka == null)
            {
                return NotFound();
            }

            return View(pielegniarka);
        }

        // POST: Pielegniarka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pielegniarka = await _context.Pielegniarka.FindAsync(id);
            _context.Pielegniarka.Remove(pielegniarka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PielegniarkaExists(int id)
        {
            return _context.Pielegniarka.Any(e => e.Pielegniarkaid == id);
        }
    }
}
