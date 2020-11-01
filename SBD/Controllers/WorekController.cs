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
    public class WorekController : Controller
    {
        private readonly ModelContext _context;

        public WorekController(ModelContext context)
        {
            _context = context;
        }

        // GET: Worek
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Worek.Include(w => w.Bank).Include(w => w.Donacja).Include(w => w.Transfuzja);
            return View(await modelContext.ToListAsync());
        }

        // GET: Worek/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worek = await _context.Worek
                .Include(w => w.Bank)
                .Include(w => w.Donacja)
                .Include(w => w.Transfuzja)
                .FirstOrDefaultAsync(m => m.Worekid == id);
            if (worek == null)
            {
                return NotFound();
            }

            return View(worek);
        }

        // GET: Worek/Create
        public IActionResult Create()
        {
            ViewData["Bankid"] = new SelectList(_context.Bankkrwi, "Bankid", "Bankid");
            ViewData["Donacjaid"] = new SelectList(_context.Donacja, "Donacjaid", "Donacjaid");
            ViewData["Transfuzjaid"] = new SelectList(_context.Transfuzja, "Transfuzjaid", "Transfuzjaid");
            return View();
        }

        // POST: Worek/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Worekid,Bankid,Transfuzjaid,Donacjaid,Wielkosc,Grupakrwi,Rh")] Worek worek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Bankid"] = new SelectList(_context.Bankkrwi, "Bankid", "Bankid", worek.Bankid);
            ViewData["Donacjaid"] = new SelectList(_context.Donacja, "Donacjaid", "Donacjaid", worek.Donacjaid);
            ViewData["Transfuzjaid"] = new SelectList(_context.Transfuzja, "Transfuzjaid", "Transfuzjaid", worek.Transfuzjaid);
            return View(worek);
        }

        // GET: Worek/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worek = await _context.Worek.FindAsync(id);
            if (worek == null)
            {
                return NotFound();
            }
            ViewData["Bankid"] = new SelectList(_context.Bankkrwi, "Bankid", "Bankid", worek.Bankid);
            ViewData["Donacjaid"] = new SelectList(_context.Donacja, "Donacjaid", "Donacjaid", worek.Donacjaid);
            ViewData["Transfuzjaid"] = new SelectList(_context.Transfuzja, "Transfuzjaid", "Transfuzjaid", worek.Transfuzjaid);
            return View(worek);
        }

        // POST: Worek/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Worekid,Bankid,Transfuzjaid,Donacjaid,Wielkosc,Grupakrwi,Rh")] Worek worek)
        {
            if (id != worek.Worekid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(worek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorekExists(worek.Worekid))
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
            ViewData["Bankid"] = new SelectList(_context.Bankkrwi, "Bankid", "Bankid", worek.Bankid);
            ViewData["Donacjaid"] = new SelectList(_context.Donacja, "Donacjaid", "Donacjaid", worek.Donacjaid);
            ViewData["Transfuzjaid"] = new SelectList(_context.Transfuzja, "Transfuzjaid", "Transfuzjaid", worek.Transfuzjaid);
            return View(worek);
        }

        // GET: Worek/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worek = await _context.Worek
                .Include(w => w.Bank)
                .Include(w => w.Donacja)
                .Include(w => w.Transfuzja)
                .FirstOrDefaultAsync(m => m.Worekid == id);
            if (worek == null)
            {
                return NotFound();
            }

            return View(worek);
        }

        // POST: Worek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var worek = await _context.Worek.FindAsync(id);
            _context.Worek.Remove(worek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorekExists(int id)
        {
            return _context.Worek.Any(e => e.Worekid == id);
        }
    }
}
