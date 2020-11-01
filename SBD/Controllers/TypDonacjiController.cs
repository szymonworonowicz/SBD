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
    public class TypDonacjiController : Controller
    {
        private readonly ModelContext _context;

        public TypDonacjiController(ModelContext context)
        {
            _context = context;
        }

        // GET: TypDonacji
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypDonacji.ToListAsync());
        }

        // GET: TypDonacji/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typDonacji = await _context.TypDonacji
                .FirstOrDefaultAsync(m => m.Typid == id);
            if (typDonacji == null)
            {
                return NotFound();
            }

            return View(typDonacji);
        }

        // GET: TypDonacji/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypDonacji/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Typid,Typ,Czestotliwosc")] TypDonacji typDonacji)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typDonacji);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typDonacji);
        }

        // GET: TypDonacji/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typDonacji = await _context.TypDonacji.FindAsync(id);
            if (typDonacji == null)
            {
                return NotFound();
            }
            return View(typDonacji);
        }

        // POST: TypDonacji/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Typid,Typ,Czestotliwosc")] TypDonacji typDonacji)
        {
            if (id != typDonacji.Typid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typDonacji);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypDonacjiExists(typDonacji.Typid))
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
            return View(typDonacji);
        }

        // GET: TypDonacji/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typDonacji = await _context.TypDonacji
                .FirstOrDefaultAsync(m => m.Typid == id);
            if (typDonacji == null)
            {
                return NotFound();
            }

            return View(typDonacji);
        }

        // POST: TypDonacji/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typDonacji = await _context.TypDonacji.FindAsync(id);
            _context.TypDonacji.Remove(typDonacji);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypDonacjiExists(int id)
        {
            return _context.TypDonacji.Any(e => e.Typid == id);
        }
    }
}
