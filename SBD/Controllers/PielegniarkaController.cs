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
    public class PielegniarkaController : Controller
    {
        private readonly ModelContext _context;

        public PielegniarkaController(ModelContext context)
        {
            _context = context;
        }

        // GET: Pielegniarka
        /*public async Task<IActionResult> Index()
        {
            var modelContext = _context.Pielegniarka.Include(p => p.Osoba);
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




            var items = from Pielegniarka in _context.Pielegniarka
                        select Pielegniarka;
            items = items.Include(p => p.Osoba);
            if (!String.IsNullOrEmpty(searchString) && items.Any())
            {

                items = items.Where(
                    s =>
                    s.Osoba.Imie.Contains(searchString)
                    || s.Osoba.Nazwisko.Contains(searchString)
                    || s.Doswiadczenie.ToString().Contains(searchString)

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
                        items = items.OrderBy(s => s.Pielegniarkaid);
                        break;
                }


            int pageSize = 10;
            return View(await PaginatedList<Pielegniarka>.CreateAsync(items.AsNoTracking(), pageNumber ?? 1, pageSize));

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
            ViewData["Osobaid"] = new SelectList(_context.Osoba, "Osobaid", "Info");
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
                _context.Attach(pielegniarka).State = EntityState.Detached;
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
            ViewData["Osobaid"] = new SelectList(_context.Osoba, "Osobaid", "Info", pielegniarka.Osobaid);
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
                    _context.Attach(pielegniarka).State = EntityState.Detached;
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
            ViewData["Osobaid"] = new SelectList(_context.Osoba, "Osobaid", "Info", pielegniarka.Osobaid);
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
