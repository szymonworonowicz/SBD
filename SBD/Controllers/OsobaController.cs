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
    public class OsobaController : Controller
    {
        private readonly ModelContext _context;

        public OsobaController(ModelContext context)
        {
            _context = context;
        }

        // GET: Osoba
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.Osoba.ToListAsync());
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




            var items = from Osoba in _context.Osoba
                        select Osoba;
            
            if (!String.IsNullOrEmpty(searchString) && items.Any())
            {

                items = items.Where(
                    s =>
                    s.Imie.Contains(searchString)
                    || s.Nazwisko.Contains(searchString)
                    


                  );
            }

            if (items.Any())
                switch (sortOrder)
                {

                    case "Nazw_desc":
                        items = items.OrderByDescending(s => s.Nazwisko);
                        break;
                    case "Nazw":
                        items = items.OrderBy(s => s.Nazwisko);
                        break;

                    default:
                        items = items.OrderBy(s => s.Osobaid);
                        break;
                }


            int pageSize = 10;
            return View(await PaginatedList<Osoba>.CreateAsync(items.AsNoTracking(), pageNumber ?? 1, pageSize));

        }
        // GET: Osoba/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osoba = await _context.Osoba
                .FirstOrDefaultAsync(m => m.Osobaid == id);
            if (osoba == null)
            {
                return NotFound();
            }

            return View(osoba);
        }

        // GET: Osoba/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Osoba/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Osobaid,Imie,Nazwisko,DataUrodzenia")] Osoba osoba)
        {
            if (ModelState.IsValid)
            {
                _context.Add(osoba);
                await _context.SaveChangesAsync();
                _context.Attach(osoba).State = EntityState.Detached;
                return RedirectToAction(nameof(Index));
            }
            return View(osoba);
        }

        // GET: Osoba/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osoba = await _context.Osoba.FindAsync(id);
            if (osoba == null)
            {
                return NotFound();
            }
            return View(osoba);
        }

        // POST: Osoba/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Osobaid,Imie,Nazwisko,DataUrodzenia")] Osoba osoba)
        {
            if (id != osoba.Osobaid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(osoba);
                    await _context.SaveChangesAsync();
                    _context.Attach(osoba).State = EntityState.Detached;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OsobaExists(osoba.Osobaid))
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
            return View(osoba);
        }

        // GET: Osoba/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osoba = await _context.Osoba
                .FirstOrDefaultAsync(m => m.Osobaid == id);
            if (osoba == null)
            {
                return NotFound();
            }

            return View(osoba);
        }

        // POST: Osoba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var osoba = await _context.Osoba.FindAsync(id);
            _context.Osoba.Remove(osoba);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OsobaExists(int id)
        {
            return _context.Osoba.Any(e => e.Osobaid == id);
        }
    }
}
