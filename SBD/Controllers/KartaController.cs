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
    public class KartaController : Controller
    {
        private readonly ModelContext _context;

        public KartaController(ModelContext context)
        {
            _context = context;
        }

        // GET: Karta
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kartazdrowia.ToListAsync());
        }

        // GET: Karta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kartazdrowia = await _context.Kartazdrowia
                .FirstOrDefaultAsync(m => m.Kartaid == id);
            if (kartazdrowia == null)
            {
                return NotFound();
            }

            return View(kartazdrowia);
        }

        // GET: Karta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Karta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Kartaid,Syfilis,Zapaleniewatrobyb,Zapaleniewatrobyc,Hiv,Malaria")] Kartazdrowia kartazdrowia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kartazdrowia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kartazdrowia);
        }

        // GET: Karta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kartazdrowia = await _context.Kartazdrowia.FindAsync(id);
            if (kartazdrowia == null)
            {
                return NotFound();
            }
            return View(kartazdrowia);
        }

        // POST: Karta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Kartaid,Syfilis,Zapaleniewatrobyb,Zapaleniewatrobyc,Hiv,Malaria")] Kartazdrowia kartazdrowia)
        {
            if (id != kartazdrowia.Kartaid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kartazdrowia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KartazdrowiaExists(kartazdrowia.Kartaid))
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
            return View(kartazdrowia);
        }

        // GET: Karta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kartazdrowia = await _context.Kartazdrowia
                .FirstOrDefaultAsync(m => m.Kartaid == id);
            if (kartazdrowia == null)
            {
                return NotFound();
            }

            return View(kartazdrowia);
        }

        // POST: Karta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kartazdrowia = await _context.Kartazdrowia.FindAsync(id);
            _context.Kartazdrowia.Remove(kartazdrowia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KartazdrowiaExists(int id)
        {
            return _context.Kartazdrowia.Any(e => e.Kartaid == id);
        }
    }
}
