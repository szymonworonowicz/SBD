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
    public class PacjentController : Controller
    {
        private readonly ModelContext _context;

        public PacjentController(ModelContext context)
        {
            _context = context;
        }

        // GET: Pacjent
        /*public async Task<IActionResult> Index()
        {
            var modelContext = _context.Pacjent.Include(p => p.Osoba);
            return View(await modelContext.ToListAsync());
        }*/
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["PriorSortParm"] = sortOrder == "Prior" ? "Prior_desc" : "Prior";
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




            var items = from Pacjent in _context.Pacjent
                        select Pacjent;
            items = items.Include(p => p.Osoba);
            if (!String.IsNullOrEmpty(searchString) && items.Any())
            {

                items = items.Where(
                    s=>
                    s.Osoba.Imie.Contains(searchString)
                    || s.Osoba.Nazwisko.Contains(searchString)
                    || s.Priorytet.Contains(searchString)

                  );
            }

            if (items.Any())
                switch (sortOrder)
                {
                    case "Prior_desc":
                        items = items.OrderByDescending(s => s.Priorytet);
                        break;
                    case "Prior":
                        items = items.OrderBy(s => s.Priorytet);
                        break;
                    case "Nazw_desc":
                        items = items.OrderByDescending(s => s.Osoba.Nazwisko);
                        break;
                    case "Nazw":
                        items = items.OrderBy(s => s.Osoba.Nazwisko);
                        break;
                   
                    default:
                        items = items.OrderBy(s => s.Pacjentid);
                        break;
                }


            int pageSize = 10;
            return View(await PaginatedList<Pacjent>.CreateAsync(items.AsNoTracking(), pageNumber ?? 1, pageSize));

        }
        // GET: Pacjent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacjent = await _context.Pacjent
                .Include(p => p.Osoba)
                .FirstOrDefaultAsync(m => m.Pacjentid == id);
            if (pacjent == null)
            {
                return NotFound();
            }

            return View(pacjent);
        }

        // GET: Pacjent/Create
        public IActionResult Create()
        {
            ViewData["Osobaid"] = new SelectList(_context.Osoba, "Osobaid", "Info");
            return View();
        }

        // POST: Pacjent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pacjentid,GrupaKrwi,Priorytet,Waga,Osobaid")] Pacjent pacjent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacjent);
                await _context.SaveChangesAsync();
                _context.Attach(pacjent).State = EntityState.Detached;
                return RedirectToAction(nameof(Index));
            }
            ViewData["Osobaid"] = new SelectList(_context.Osoba, "Osobaid", "Info", pacjent.Osobaid);
            return View(pacjent);
        }

        // GET: Pacjent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacjent = await _context.Pacjent.FindAsync(id);
            if (pacjent == null)
            {
                return NotFound();
            }
            ViewData["Osobaid"] = new SelectList(_context.Osoba, "Osobaid", "Info", pacjent.Osobaid);
            return View(pacjent);
        }

        // POST: Pacjent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Pacjentid,GrupaKrwi,Priorytet,Waga,Osobaid")] Pacjent pacjent)
        {
            if (id != pacjent.Pacjentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacjent);
                    await _context.SaveChangesAsync();
                    _context.Attach(pacjent).State = EntityState.Detached;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacjentExists(pacjent.Pacjentid))
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
            ViewData["Osobaid"] = new SelectList(_context.Osoba, "Osobaid", "Osobaid", pacjent.Osobaid);
            return View(pacjent);
        }

        // GET: Pacjent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacjent = await _context.Pacjent
                .Include(p => p.Osoba)
                .FirstOrDefaultAsync(m => m.Pacjentid == id);
            if (pacjent == null)
            {
                return NotFound();
            }

            return View(pacjent);
        }

        // POST: Pacjent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pacjent = await _context.Pacjent.FindAsync(id);
            _context.Pacjent.Remove(pacjent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacjentExists(int id)
        {
            return _context.Pacjent.Any(e => e.Pacjentid == id);
        }
    }
}
