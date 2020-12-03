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
    public class BadaniaController : Controller
    {
        private readonly ModelContext _context;

        public BadaniaController(ModelContext context)
        {
            _context = context;
        }

        // GET: Badania
        /*public async Task<IActionResult> Index()
        {
            var modelContext = await _context.Badania.AsNoTracking().Include(b => b.Karta).ToListAsync();

            return View(modelContext);
        }*/
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["TempSortParm"] = sortOrder == "Temp" ? "Temp_desc" : "Temp";
            ViewData["CisnSortParm"] = sortOrder == "Cisn" ? "Cisn_desc" : "Cisn";
            ViewData["TetnoSortParm"] = sortOrder == "Tetno" ? "Tetno_desc" : "Tetno";
            ViewData["HemoSortParm"] = sortOrder == "Hemo" ? "Hemo_desc" : "Hemo";
            
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;




            var items = _context.Badania.Include(x => x.Karta).AsQueryable(); //jak ci nie podciaga to zamieniasz linq na cos takiego
            if (!String.IsNullOrEmpty(searchString) && items.Any())
            {

                items = items.Where(s => s.Cisnienie.Contains(searchString)
                || s.Temperatura.ToString().Contains(searchString)
                || s.Tetno.ToString().Contains(searchString)
                || s.Hemoglobina.ToString().Contains(searchString)
                );
            }
            
           
            switch (sortOrder)
            {
                case "Hemo_desc":
                    items = items.OrderByDescending(s => s.Hemoglobina);
                    break;
                case "Temp":
                    items = items.OrderBy(s => s.Temperatura);
                    break;
                case "Temp_desc":
                    items = items.OrderByDescending(s => s.Temperatura);
                    break;
                case "Cisn_desc":
                    items = items.OrderByDescending(s => s.Cisnienie);
                    break;
                case "Cisn":
                    items = items.OrderBy(s => s.Cisnienie);
                    break;
                case "Tetno":
                    items = items.OrderBy(s => s.Tetno);
                    break;
                case "Tetno_desc":
                    items = items.OrderByDescending(s => s.Tetno);
                    break;
                case "Hemo":
                    items = items.OrderBy(s => s.Hemoglobina);
                    break;
                default:
                    items = items.OrderBy(s => s.Badaniaid);
                    break;
            }


            int pageSize = 10;
            return View(await PaginatedList<Badania>.CreateAsync(items.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        // GET: Badania/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var badania = await _context.Badania.AsNoTracking()
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
                _context.Attach(badania).State = EntityState.Detached;
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

            var badania = await _context.Badania.FirstOrDefaultAsync(x => x.Badaniaid == id);
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
                    var badanie = await _context.Badania.AsNoTracking().FirstOrDefaultAsync(x => x.Badaniaid == badania.Badaniaid);

                    badanie.Badaniaid = badania.Badaniaid;
                    badanie.Kartaid = badania.Kartaid; 
                    badanie.Cisnienie = badania.Cisnienie; 
                    badanie.Hemoglobina = badania.Hemoglobina; 
                    badanie.Temperatura = badania.Temperatura; 
                    badanie.Tetno = badania.Tetno;

                    _context.Update(badanie);
                    //_context.Entry(badanie).State = EntityState.Modified;

                    await _context.SaveChangesAsync();

                    _context.Entry(badanie).State = EntityState.Detached;

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
            _context.Entry(badania).State = EntityState.Deleted;
            //_context.Badania.Remove(badania);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BadaniaExists(int id)
        {
            return _context.Badania.Any(e => e.Badaniaid == id);
        }
    }
}
