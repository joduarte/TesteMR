using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TesteMR.Data;
using TesteMR.Models;

namespace TesteMR.Controllers
{
    public class UnidadesController : Controller
    {
        private readonly DataContext _context;

        public UnidadesController(DataContext context)
        {
            _context = context;
        }

        // GET: Unidades
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

            var unidades = from s in _context.Unidades
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                unidades = unidades.Where(s => s.cCode.Contains(searchString)
                                       || s.cDescription.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "code_desc":
                    unidades = unidades.OrderByDescending(s => s.cCode);
                    break;
                case "Desc":
                    unidades = unidades.OrderBy(s => s.cDescription);
                    break;
                case "desc_desc":
                    unidades = unidades.OrderByDescending(s => s.cDescription);
                    break;
                default:
                    unidades = unidades.OrderBy(s => s.cCode);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Unidade>.CreateAsync(unidades.AsNoTracking(), pageNumber ?? 1, pageSize));


        }

        // GET: Unidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidade = await _context.Unidades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidade == null)
            {
                return NotFound();
            }

            return View(unidade);
        }

        // GET: Unidades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Unidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,cCode,cDescription,dCreateDateTime,dChangedDateTime,uId")] Unidade unidade)
        {
            if (ModelState.IsValid)
            {
                unidade.dCreateDateTime = DateTime.Now;
                unidade.uId = Guid.NewGuid();
                _context.Add(unidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unidade);
        }

        // GET: Unidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidade = await _context.Unidades.FindAsync(id);
            if (unidade == null)
            {
                return NotFound();
            }
            return View(unidade);
        }

        // POST: Unidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,cCode,cDescription,dCreateDateTime,dChangedDateTime,uId")] Unidade unidade)
        {
            if (id != unidade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unidade.dChangedDateTime = DateTime.Now;
                    _context.Update(unidade);
                    _context.Entry(unidade).State = EntityState.Modified;
                    _context.Entry(unidade).Property(p => p.dCreateDateTime).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadeExists(unidade.Id))
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
            return View(unidade);
        }

        // GET: Unidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidade = await _context.Unidades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidade == null)
            {
                return NotFound();
            }

            return View(unidade);
        }

        // POST: Unidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unidade = await _context.Unidades.FindAsync(id);
            _context.Unidades.Remove(unidade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadeExists(int id)
        {
            return _context.Unidades.Any(e => e.Id == id);
        }
    }
}
