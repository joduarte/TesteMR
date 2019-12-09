using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TesteMR.Data;
using TesteMR.Models;
using PagedList;

namespace TesteMR.Controllers
{
    public class ArtigosController : Controller
    {
        private readonly DataContext _context;

        public ArtigosController(DataContext context)
        {
            _context = context;
        }

        // GET: Artigos
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Artigos.ToListAsync());
        //}

        

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewData["CodeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "code_desc" : "";
            ViewData["DescriptionSortParm"] = sortOrder == "Desc" ? "desc_desc" : "Desc";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var artigos = from s in _context.Artigos
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                artigos = artigos.Where(s => s.cCode.Contains(searchString)
                                       || s.cDescription.Contains(searchString)
                                       || s.nUnitPrice.ToString().Contains(searchString));
            }

                switch (sortOrder)
            {
                case "code_desc":
                    artigos = artigos.OrderByDescending(s => s.cCode);
                    break;
                case "Desc":
                    artigos = artigos.OrderBy(s => s.cDescription);
                    break;
                case "desc_desc":
                    artigos = artigos.OrderByDescending(s => s.cDescription);
                    break;
                default:
                    artigos = artigos.OrderBy(s => s.cCode);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Artigo>.CreateAsync(artigos.AsNoTracking(), pageNumber ?? 1, pageSize));


        }

        // GET: Artigos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigo = await _context.Artigos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artigo == null)
            {
                return NotFound();
            }

            return View(artigo);
        }

        // GET: Artigos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artigos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("cCode,cDescription,cUnitCode,nUnitPrice,dCreateDateTime,uId")] Artigo artigo)
        {
            if (ModelState.IsValid)
            {
                artigo.dCreateDateTime = DateTime.Now;
                artigo.uId = Guid.NewGuid();
                _context.Add(artigo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artigo);
        }

        // GET: Artigos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigo = await _context.Artigos.FindAsync(id);
            if (artigo == null)
            {
                return NotFound();
            }
            return View(artigo);
        }

        // POST: Artigos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,cCode,cDescription,cUnitCode,nUnitPrice,dCreateDateTime,dChangedDateTime,uId")] Artigo artigo)
        {
            if (id != artigo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    artigo.dChangedDateTime = DateTime.Now;
                    _context.Update(artigo);
                    _context.Entry(artigo).State = EntityState.Modified;
                    _context.Entry(artigo).Property(p => p.dCreateDateTime).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtigoExists(artigo.Id))
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
            return View(artigo);
        }

        // GET: Artigos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigo = await _context.Artigos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artigo == null)
            {
                return NotFound();
            }

            return View(artigo);
        }

        // POST: Artigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artigo = await _context.Artigos.FindAsync(id);
            _context.Artigos.Remove(artigo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtigoExists(int id)
        {
            return _context.Artigos.Any(e => e.Id == id);
        }
    }

    
}
