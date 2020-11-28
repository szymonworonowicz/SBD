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
    public class TransfuzjaController : Controller
    {
        private readonly ModelContext _context;

        public TransfuzjaController(ModelContext context)
        {
            _context = context;
        }

        // GET: Transfuzja
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Transfuzja
                .Include(t => t.Badania)
                .Include(t => t.Pacjent)
                .ThenInclude(x => x.Osoba)
                .Include(t => t.Pielegniarka)
                .ThenInclude(x => x.Osoba);
            return View(await modelContext.ToListAsync());
        }

        // GET: Transfuzja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transfuzja = await _context.Transfuzja
                .Include(t => t.Badania)
                .Include(t => t.Pacjent)
                .ThenInclude(x => x.Osoba)
                .Include(t => t.Pielegniarka)
                .ThenInclude(x => x.Osoba)
                .FirstOrDefaultAsync(m => m.Transfuzjaid == id);
            if (transfuzja == null)
            {
                return NotFound();
            }

            return View(transfuzja);
        }

        // GET: Transfuzja/Create
        public IActionResult Create()
        {
            var pacjent = _context.Pacjent.Include(x => x.Osoba);
            var pielegniarka = _context.Pielegniarka.Include(x => x.Osoba);

            ViewData["Badaniaid"] = new SelectList(_context.Badania, "Badaniaid", "Badaniaid");
            ViewData["Pacjentid"] = new SelectList(pacjent, "Pacjentid", "Info");
            ViewData["Pielegniarkaid"] = new SelectList(pielegniarka, "Pielegniarkaid", "Info");
            return View();
        }

        // POST: Transfuzja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Transfuzjaid,Pielegniarkaid,Badaniaid,Pacjentid,PotrzebnaIlosc,DataTransfuzji")] Transfuzja transfuzja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transfuzja);
                await _context.SaveChangesAsync();
                _context.Attach(transfuzja).State = EntityState.Detached;
                return RedirectToAction(nameof(Index));
            }
            ViewData["Badaniaid"] = new SelectList(_context.Badania, "Badaniaid", "Badaniaid", transfuzja.Badaniaid);
            ViewData["Pacjentid"] = new SelectList(_context.Pacjent, "Pacjentid", "Info", transfuzja.Pacjentid);
            ViewData["Pielegniarkaid"] = new SelectList(_context.Pielegniarka, "Pielegniarkaid", "Info", transfuzja.Pielegniarkaid);
            return View(transfuzja);
        }

        // GET: Transfuzja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transfuzja = await _context.Transfuzja.FindAsync(id);
            if (transfuzja == null)
            {
                return NotFound();
            }

            var pacjent = _context.Pacjent.Include(x => x.Osoba);
            var pielegniarka = _context.Pielegniarka.Include(x => x.Osoba);

            ViewData["Badaniaid"] = new SelectList(_context.Badania, "Badaniaid", "Badaniaid", transfuzja.Badaniaid);
            ViewData["Pacjentid"] = new SelectList(pacjent, "Pacjentid", "Info", transfuzja.Pacjentid);
            ViewData["Pielegniarkaid"] = new SelectList(pielegniarka, "Pielegniarkaid", "Info", transfuzja.Pielegniarkaid);
            return View(transfuzja);
        }

        // POST: Transfuzja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Transfuzjaid,Pielegniarkaid,Badaniaid,Pacjentid,PotrzebnaIlosc,DataTransfuzji")] Transfuzja transfuzja)
        {
            if (id != transfuzja.Transfuzjaid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transfuzja);
                    await _context.SaveChangesAsync();
                    _context.Attach(transfuzja).State = EntityState.Detached;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransfuzjaExists(transfuzja.Transfuzjaid))
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
            ViewData["Badaniaid"] = new SelectList(_context.Badania, "Badaniaid", "Badaniaid", transfuzja.Badaniaid);
            ViewData["Pacjentid"] = new SelectList(_context.Pacjent, "Pacjentid", "Pacjentid", transfuzja.Pacjentid);
            ViewData["Pielegniarkaid"] = new SelectList(_context.Pielegniarka, "Pielegniarkaid", "Pielegniarkaid", transfuzja.Pielegniarkaid);
            return View(transfuzja);
        }

        // GET: Transfuzja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transfuzja = await _context.Transfuzja
                .Include(t => t.Badania)
                .Include(t => t.Pacjent)
                .Include(t => t.Pielegniarka)
                .FirstOrDefaultAsync(m => m.Transfuzjaid == id);
            if (transfuzja == null)
            {
                return NotFound();
            }

            return View(transfuzja);
        }

        // POST: Transfuzja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transfuzja = await _context.Transfuzja.FindAsync(id);
            _context.Transfuzja.Remove(transfuzja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransfuzjaExists(int id)
        {
            return _context.Transfuzja.Any(e => e.Transfuzjaid == id);
        }
    }
}
