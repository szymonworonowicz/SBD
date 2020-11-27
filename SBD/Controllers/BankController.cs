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
    public class BankController : Controller
    {
        private readonly ModelContext _context;

        public BankController(ModelContext context)
        {
            _context = context;
        }

        // GET: Bank
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Bankkrwi.Include(b => b.Adres);
            return View(await modelContext.ToListAsync());
        }

        // GET: Bank/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankkrwi = await _context.Bankkrwi
                .Include(b => b.Adres)
                .FirstOrDefaultAsync(m => m.Bankid == id);
            if (bankkrwi == null)
            {
                return NotFound();
            }

            return View(bankkrwi);
        }

        // GET: Bank/Create
        public IActionResult Create()
        {
            ViewData["Adresid"] = new SelectList(_context.Adres, "Adresid", "Info");
            return View();
        }

        // POST: Bank/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Bankid,Adresid,Typkrwi")] Bankkrwi bankkrwi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankkrwi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Adresid"] = new SelectList(_context.Adres, "Adresid", "Info", bankkrwi.Adresid);
            return View(bankkrwi);
        }

        // GET: Bank/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankkrwi = await _context.Bankkrwi.FindAsync(id);
            if (bankkrwi == null)
            {
                return NotFound();
            }
            ViewData["Adresid"] = new SelectList(_context.Adres, "Adresid", "Info", bankkrwi.Adresid);
            return View(bankkrwi);
        }

        // POST: Bank/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Bankid,Adresid,Typkrwi")] Bankkrwi bankkrwi)
        {
            if (id != bankkrwi.Bankid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankkrwi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankkrwiExists(bankkrwi.Bankid))
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
            ViewData["Adresid"] = new SelectList(_context.Adres, "Adresid", "Info", bankkrwi.Adresid);
            return View(bankkrwi);
        }

        // GET: Bank/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankkrwi = await _context.Bankkrwi
                .Include(b => b.Adres)
                .FirstOrDefaultAsync(m => m.Bankid == id);
            if (bankkrwi == null)
            {
                return NotFound();
            }

            return View(bankkrwi);
        }

        // POST: Bank/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankkrwi = await _context.Bankkrwi.FindAsync(id);
            _context.Bankkrwi.Remove(bankkrwi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankkrwiExists(int id)
        {
            return _context.Bankkrwi.Any(e => e.Bankid == id);
        }
    }
}
