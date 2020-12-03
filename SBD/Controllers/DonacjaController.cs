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
    public class DonacjaController : Controller
    {
        private readonly ModelContext _context;

        public DonacjaController(ModelContext context)
        {
            _context = context;
        }

        // GET: Donacja
        /*public async Task<IActionResult> Index()
        {
            var modelContext = _context.Donacja.
                    Include(d => d.Badania)
                    .Include(d => d.Donator)
                    .ThenInclude(d => d.Osoba)
                    .Include(d => d.Pielegniarka)
                    .ThenInclude(d => d.Osoba)
                    .Include(d => d.Typ);

            return View(await modelContext.ToListAsync());
        }*/
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["IloscSortParm"] = sortOrder == "Ilosc" ? "Ilosc_desc" : "Ilosc";
            ViewData["TypSortParm"] = sortOrder == "Typ" ? "Typ_desc" : "Typ";
            ViewData["DataSortParm"] = sortOrder == "Data" ? "Data_desc" : "Data";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;




            var items = from Donacja in _context.Donacja
                        select Donacja;
            items = items.Include(p => p.Typ).Include(p=>p.Pielegniarka).ThenInclude(p=>p.Osoba).Include(p=>p.Donator).ThenInclude(p=>p.Osoba).Include(p=>p.Badania);
            if (!String.IsNullOrEmpty(searchString) && items.Any())
            {

                items = items.Where(s => s.Typ.Typ.Contains(searchString)
                  || s.IloscDonacji.ToString().Contains(searchString)
                  || s.Datadonacji.ToString().Contains(searchString)
                  
                  );
            }

            if (items.Any())
                switch (sortOrder)
                {
                    case "Ilosc_desc":
                        items = items.OrderByDescending(s => s.IloscDonacji.ToString());
                        break;
                    case "Ilosc":
                        items = items.OrderBy(s => s.IloscDonacji.ToString());
                        break;
                    case "Typ_desc":
                        items = items.OrderByDescending(s => s.Typ.Typ);
                        break;
                    case "Typ":
                        items = items.OrderBy(s => s.Typ.Typ);
                        break;
                    case "Data_desc":
                        items = items.OrderByDescending(s => s.Datadonacji);
                        break;
                    case "Data":
                        items = items.OrderBy(s => s.Datadonacji);
                        break;
                    default:
                        items = items.OrderBy(s => s.Donacjaid);
                        break;
                }


            int pageSize = 10;
            return View(await PaginatedList<Donacja>.CreateAsync(items.AsNoTracking(), pageNumber ?? 1, pageSize));

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
                .ThenInclude(d => d.Osoba)
                .Include(d => d.Pielegniarka)
                .ThenInclude(x => x.Osoba)
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
            var donator = _context.Donator.Include(x => x.Osoba).Where(x => x.Osobaid!=null);
            var pielegniarki = _context.Pielegniarka.Include(x => x.Osoba);

            ViewData["Badaniaid"] = new SelectList(_context.Badania, "Badaniaid", "Badaniaid");
            ViewData["Donatorid"] = new SelectList(donator, "Donatorid", "Info");
            ViewData["Pielegniarkaid"] = new SelectList(pielegniarki,"Pielegniarkaid", "Info");
            ViewData["Typid"] = new SelectList(_context.TypDonacji, "Typid", "Typ");
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
                _context.Attach(donacja).State = EntityState.Detached;
                return RedirectToAction(nameof(Index));
            }

            var donator = _context.Donator.Include(x => x.Osoba).Where(x => x.Osobaid != null);
            var pielegniarki = _context.Pielegniarka.Include(x => x.Osoba).Where(x => x.Osobaid != null);

            ViewData["Badaniaid"] = new SelectList(_context.Badania, "Badaniaid", "Badaniaid", donacja.Badaniaid);
            ViewData["Donatorid"] = new SelectList(donator, "Donatorid", "Info", donacja.Donatorid);
            ViewData["Pielegniarkaid"] = new SelectList(pielegniarki, "Pielegniarkaid", "Info", donacja.Pielegniarkaid);
            ViewData["Typid"] = new SelectList(_context.TypDonacji, "Typid", "Typ", donacja.Typid);
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

            var donator = _context.Donator.Include(x => x.Osoba);
            var pielegniarki = _context.Pielegniarka.Include(x => x.Osoba);
            ViewData["Badaniaid"] = new SelectList(_context.Badania, "Badaniaid", "Badaniaid", donacja.Badaniaid);
            ViewData["Donatorid"] = new SelectList(donator, "Donatorid", "Info", donacja.Donatorid);
            ViewData["Pielegniarkaid"] = new SelectList(pielegniarki, "Pielegniarkaid", "Info", donacja.Pielegniarkaid);
            ViewData["Typid"] = new SelectList(_context.TypDonacji, "Typid", "Typ", donacja.Typid);
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
                    _context.Attach(donacja).State = EntityState.Detached;
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
                .ThenInclude(x => x.Osoba)
                .Include(d => d.Pielegniarka)
                .ThenInclude(d => d.Osoba)
                .Include(d => d.Typ)
                .FirstOrDefaultAsync(m => m.Donacjaid == id);
            if (donacja == null)
            {
                return NotFound();
            }

            return View(donacja);
        }

        //TODO 
        // POST: Donacja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donacja = await _context.Donacja.FindAsync(id);
            _context.Attach(donacja).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool DonacjaExists(int id)
        {
            return _context.Donacja.Any(e => e.Donacjaid == id);
        }
    }
}
