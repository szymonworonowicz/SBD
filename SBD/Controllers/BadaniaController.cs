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
    public class BadaniaController : Controller
    {
        private readonly ModelContext _context;

        public BadaniaController(ModelContext context)
        {
            _context = context;
        }

        // GET: Badania
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Badania.Include(b => b.Karta);
            return View(await modelContext.ToListAsync());
        }

        // GET: Badania/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var badania = await _context.Badania
                .Include(b => b.Karta)
                .FirstOrDefaultAsync(m => m.Badaniaid == id);
            if (badania == null)
            {
                return NotFound();
            }

            return View(badania);
        }

        // GET: Badania/Create
        public IActionResult Create()
        {
            ViewData["Kartaid"] = new SelectList(_context.Kartazdrowia, "Kartaid", "Kartaid");
            return View();
        }

        // POST: Badania/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Badaniaid,Hemoglobina,Temperatura,Cisnienie,Tetno,Kartaid")] Badania badania)
        {
            if (ModelState.IsValid)
            {
                _context.Add(badania);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Kartaid"] = new SelectList(_context.Kartazdrowia, "Kartaid", "Kartaid", badania.Kartaid);
            return View(badania);
        }

        // GET: Badania/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var badania = await _context.Badania.FindAsync(id);
            if (badania == null)
            {
                return NotFound();
            }
            ViewData["Kartaid"] = new SelectList(_context.Kartazdrowia, "Kartaid", "Kartaid", badania.Kartaid);
            return View(badania);
        }

        // POST: Badania/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Badaniaid,Hemoglobina,Temperatura,Cisnienie,Tetno,Kartaid")] Badania badania)
        {
            if (id != badania.Badaniaid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(badania);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BadaniaExists(badania.Badaniaid))
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
            ViewData["Kartaid"] = new SelectList(_context.Kartazdrowia, "Kartaid", "Kartaid", badania.Kartaid);
            return View(badania);
        }

        // GET: Badania/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var badania = await _context.Badania
                .Include(b => b.Karta)
                .FirstOrDefaultAsync(m => m.Badaniaid == id);
            if (badania == null)
            {
                return NotFound();
            }

            return View(badania);
        }

        // POST: Badania/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var badania = await _context.Badania.FindAsync(id);
            _context.Badania.Remove(badania);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BadaniaExists(int id)
        {
            return _context.Badania.Any(e => e.Badaniaid == id);
        }
    }
}
