using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TesteMR.Data;
using TesteMR.Models;
using System.Data.Common;


namespace TesteMR.Controllers
{
    public class UnidadesArtigoController : Controller
    {
        private readonly DataContext _context;

        public UnidadesArtigoController(DataContext context)
        {
            _context = context;
        }

        // GET: UnidadesArtigo
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

            var unidadeartigo = from s in _context.UnidadesArtigos
                           select s;

            
            if (!String.IsNullOrEmpty(searchString))
            {
                unidadeartigo = unidadeartigo.Where(s => s.Artigo.cCode.Contains(searchString)
                                       || s.Artigo.cDescription.Contains(searchString)
                                       || s.Artigo.cCode.Contains(searchString)
                                       || s.Artigo.cDescription.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "code_desc":
                    unidadeartigo = unidadeartigo.OrderByDescending(s => s.Artigo.cCode);
                    break;
                case "Desc":
                    unidadeartigo = unidadeartigo.OrderBy(s => s.Artigo.cDescription);
                    break;
                case "desc_desc":
                    unidadeartigo = unidadeartigo.OrderByDescending(s => s.Unidade.cDescription);
                    break;
                default:
                    unidadeartigo = unidadeartigo.OrderBy(s => s.Unidade.cCode);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<UnidadeArtigo>.CreateAsync(unidadeartigo, pageNumber ?? 1, pageSize));


        }

        // GET: UnidadesArtigo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeArtigo = await _context.UnidadesArtigos
                .FirstOrDefaultAsync(m => m.nId == id);
            if (unidadeArtigo == null)
            {
                return NotFound();
            }

            return View(unidadeArtigo);
        }

        // GET: UnidadesArtigo/Create
        public IActionResult Create()
        {
            ViewBag.Unidades = GetUnidades().Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.cDescription
            }).ToList();

            ViewBag.Artigos = GetArtigos().Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.cDescription
            }).ToList();

            return View();
        }

        // POST: UnidadesArtigo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nId,ArtigoId,UnidadeId,nBaseUnitQty,nPrice,dCreateDateTime,dChangedDateTime,uId")] UnidadeArtigo unidadeArtigo)
        {
            if (ModelState.IsValid)
            {
                unidadeArtigo.dCreateDateTime = DateTime.Now;
                unidadeArtigo.uId = Guid.NewGuid();
                _context.Add(unidadeArtigo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unidadeArtigo);
        }

        // GET: UnidadesArtigo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeArtigo = await _context.UnidadesArtigos.FindAsync(id);

            ViewBag.Unidades = GetUnidades().Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.cDescription
            }).ToList();

            ViewBag.Artigos = GetArtigos().Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.cDescription
            }).ToList();

            if (unidadeArtigo == null)
            {
                return NotFound();
            }
            return View(unidadeArtigo);
        }

        // POST: UnidadesArtigo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("nId,ArtigoId,UnidadeId,nBaseUnitQty,nPrice,dCreateDateTime,dChangedDateTime,uId")] UnidadeArtigo unidadeArtigo)
        {
            if (id != unidadeArtigo.nId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    unidadeArtigo.dChangedDateTime = DateTime.Now;
                    _context.Update(unidadeArtigo);
                    _context.Entry(unidadeArtigo).State = EntityState.Modified;
                    _context.Entry(unidadeArtigo).Property(p => p.dCreateDateTime).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadeArtigoExists(unidadeArtigo.nId))
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
            return View(unidadeArtigo);
        }

        // GET: UnidadesArtigo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeArtigo = await _context.UnidadesArtigos
                .FirstOrDefaultAsync(m => m.nId == id);
            if (unidadeArtigo == null)
            {
                return NotFound();
            }

            return View(unidadeArtigo);
        }

        // POST: UnidadesArtigo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unidadeArtigo = await _context.UnidadesArtigos.FindAsync(id);
            _context.UnidadesArtigos.Remove(unidadeArtigo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadeArtigoExists(int id)
        {
            return _context.UnidadesArtigos.Any(e => e.nId == id);
        }


        public List<Artigo> GetArtigos()
        {
            return _context.Artigos.ToList();
        }

        public List<Unidade> GetUnidades()
        {
            return _context.Unidades.ToList();
        }

        
    }
}
