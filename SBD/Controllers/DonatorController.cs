using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SBD.Models;
using SBD.Pagination;

namespace SBD.Controllers
{
    public class DonatorController : Controller
    {
        private readonly ModelContext _context;

        public DonatorController(ModelContext context)
        {
            _context = context;
        }

        // GET: Donator
       /* public async Task<IActionResult> Index()
        {
            var modelContext = _context.Donator.Include(d => d.Osoba);
            return View(await modelContext.ToListAsync());
        }*/
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";

            ViewData["NazwiskoSortParm"] = sortOrder == "Nazw" ? "Nazw_desc" : "Nazw";


            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;




            var items = from Donator in _context.Donator
                        select Donator;
            items = items.Include(p => p.Osoba);
            if (!String.IsNullOrEmpty(searchString) && items.Any())
            {

                items = items.Where(
                    s =>
                    s.Osoba.Imie.Contains(searchString)
                    || s.Osoba.Nazwisko.Contains(searchString)
                    || s.GrupaKrwi.Contains(searchString)
                    || s.Rh.Contains(searchString)


                  );
            }

            if (items.Any())
                switch (sortOrder)
                {

                    case "Nazw_desc":
                        items = items.OrderByDescending(s => s.Osoba.Nazwisko);
                        break;
                    case "Nazw":
                        items = items.OrderBy(s => s.Osoba.Nazwisko);
                        break;

                    default:
                        items = items.OrderBy(s => s.Donatorid);
                        break;
                }


            int pageSize = 10;
            return View(await PaginatedList<Donator>.CreateAsync(items.AsNoTracking(), pageNumber ?? 1, pageSize));

        }
        // GET: Donator/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donator = await _context.Donator
                .Include(d => d.Osoba)
                .FirstOrDefaultAsync(m => m.Donatorid == id);
            if (donator == null)
            {
                return NotFound();
            }

            return View(donator);
        }

        // GET: Donator/Create
        public IActionResult Create()
        {
            ViewData["Osobaid"] = new SelectList(_context.Osoba, "Osobaid", "Info");
            return View();
        }

        // POST: Donator/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Donatorid,GrupaKrwi,Rh,Waga,Wzrost,Nastepnadonacja,Osobaid")] Donator donator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donator);
                await _context.SaveChangesAsync();
                _context.Attach(donator).State = EntityState.Detached;
                return RedirectToAction(nameof(Index));
            }
            ViewData["Osobaid"] = new SelectList(_context.Osoba, "Osobaid", "Info", donator.Osobaid);
            return View(donator);
        }

        // GET: Donator/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donator = await _context.Donator.FindAsync(id);
            if (donator == null)
            {
                return NotFound();
            }
            ViewData["Osobaid"] = new SelectList(_context.Osoba, "Osobaid", "Info", donator.Osobaid);
            return View(donator);
        }

        // POST: Donator/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Donatorid,GrupaKrwi,Rh,Waga,Wzrost,Nastepnadonacja,Osobaid")] Donator donator)
        {
            if (id != donator.Donatorid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donator);
                    await _context.SaveChangesAsync();
                    _context.Attach(donator).State = EntityState.Detached;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonatorExists(donator.Donatorid))
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
            ViewData["Osobaid"] = new SelectList(_context.Osoba, "Osobaid", "Osobaid", donator.Osobaid);
            return View(donator);
        }

        // GET: Donator/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donator = await _context.Donator
                .Include(d => d.Osoba)
                .FirstOrDefaultAsync(m => m.Donatorid == id);
            if (donator == null)
            {
                return NotFound();
            }

            return View(donator);
        }

        // POST: Donator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donator = await _context.Donator.FindAsync(id);
            _context.Donator.Remove(donator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonatorExists(int id)
        {
            return _context.Donator.Any(e => e.Donatorid == id);
        }
    }
}
