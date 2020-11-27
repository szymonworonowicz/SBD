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
    public class DonacjaController : Controller
    {
        private readonly ModelContext _context;

        public DonacjaController(ModelContext context)
        {
            _context = context;
        }

        // GET: Donacja
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Donacja.Include(d => d.Badania).Include(d => d.Donator).Include(d => d.Pielegniarka).Include(d => d.Typ);
            return View(await modelContext.ToListAsync());
        }

        // GET: Donacja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donacja = await _context.Donacja
                .Include(d => d.Badania)
                .Include(d => d.Donator)
                .Include(d => d.Pielegniarka)
                .Include(d => d.Typ)
                .FirstOrDefaultAsync(m => m.Donacjaid == id);
            if (donacja == null)
            {
                return NotFound();
            }

            return View(donacja);
        }

        // GET: Donacja/Create
        public IActionResult Create()
        {
            ViewData["Badaniaid"] = new SelectList(_context.Badania, "Badaniaid", "Badaniaid");
            ViewData["Donatorid"] = new SelectList(_context.Donator, "Donatorid", "Donatorid");
            ViewData["Pielegniarkaid"] = new SelectList(_context.Pielegniarka, "Pielegniarkaid", "Pielegniarkaid");
            ViewData["Typid"] = new SelectList(_context.TypDonacji, "Typid", "Typid");
            return View();
        }

        // POST: Donacja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Donacjaid,Donatorid,Badaniaid,Pielegniarkaid,Typid,IloscDonacji,Datadonacji")] Donacja donacja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donacja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Badaniaid"] = new SelectList(_context.Badania, "Badaniaid", "Badaniaid", donacja.Badaniaid);
            ViewData["Donatorid"] = new SelectList(_context.Donator, "Donatorid", "Donatorid", donacja.Donatorid);
            ViewData["Pielegniarkaid"] = new SelectList(_context.Pielegniarka, "Pielegniarkaid", "Pielegniarkaid", donacja.Pielegniarkaid);
            ViewData["Typid"] = new SelectList(_context.TypDonacji, "Typid", "Typid", donacja.Typid);
            return View(donacja);
        }

        // GET: Donacja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donacja = await _context.Donacja.FindAsync(id);
            if (donacja == null)
            {
                return NotFound();
            }
            ViewData["Badaniaid"] = new SelectList(_context.Badania, "Badaniaid", "Badaniaid", donacja.Badaniaid);
            ViewData["Donatorid"] = new SelectList(_context.Donator, "Donatorid", "Donatorid", donacja.Donatorid);
            ViewData["Pielegniarkaid"] = new SelectList(_context.Pielegniarka, "Pielegniarkaid", "Pielegniarkaid", donacja.Pielegniarkaid);
            ViewData["Typid"] = new SelectList(_context.TypDonacji, "Typid", "Typid", donacja.Typid);
            return View(donacja);
        }

        // POST: Donacja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Donacjaid,Donatorid,Badaniaid,Pielegniarkaid,Typid,IloscDonacji,Datadonacji")] Donacja donacja)
        {
            if (id != donacja.Donacjaid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donacja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonacjaExists(donacja.Donacjaid))
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
            ViewData["Badaniaid"] = new SelectList(_context.Badania, "Badaniaid", "Badaniaid", donacja.Badaniaid);
            ViewData["Donatorid"] = new SelectList(_context.Donator, "Donatorid", "Donatorid", donacja.Donatorid);
            ViewData["Pielegniarkaid"] = new SelectList(_context.Pielegniarka, "Pielegniarkaid", "Pielegniarkaid", donacja.Pielegniarkaid);
            ViewData["Typid"] = new SelectList(_context.TypDonacji, "Typid", "Typid", donacja.Typid);
            return View(donacja);
        }

        // GET: Donacja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donacja = await _context.Donacja
                .Include(d => d.Badania)
                .Include(d => d.Donator)
                .Include(d => d.Pielegniarka)
                .Include(d => d.Typ)
                .FirstOrDefaultAsync(m => m.Donacjaid == id);
            if (donacja == null)
            {
                return NotFound();
            }

            return View(donacja);
        }

        // POST: Donacja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donacja = await _context.Donacja.FindAsync(id);
            _context.Donacja.Remove(donacja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonacjaExists(int id)
        {
            return _context.Donacja.Any(e => e.Donacjaid == id);
        }
    }
}
