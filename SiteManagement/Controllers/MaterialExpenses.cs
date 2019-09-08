using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteManagement.DbLayer;
using SiteManagement.Models;

namespace SiteManagement.Controllers
{
    public class MaterialExpenses : Controller
    {
        private readonly AppDbContext _context;

        public MaterialExpenses(AppDbContext context)
        {
            _context = context;
        }

        // GET: MaterialExpenses
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MaterialExpenses.Include(m => m.Labour).Include(m => m.Site).OrderByDescending(o => o.Id); 
            PopulateSiteDropDownList();
            PopulateLabourDropDownList();
            return View(await appDbContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(int id)
        {

            var lab = _context.MaterialExpenses.Include(o => o.Labour).ThenInclude(o => o.EmployeeCategory).Include("Site");
            if (Request.Form["SiteId"] != "")
            {
                int s = Convert.ToInt32(Request.Form["SiteId"]);
                PopulateSiteDropDownList(s);
                lab = _context.MaterialExpenses.Include(o => o.Labour).ThenInclude(o => o.EmployeeCategory).Include("Site").Where(o => o.SiteId == s);
            }
            else
            {
                PopulateSiteDropDownList();
            }

            if (Request.Form["LabourId"] != "")
            {
                int l = Convert.ToInt32(Request.Form["LabourId"]);
                PopulateLabourDropDownList(l);
                lab = lab.Where(o => o.LabourId == l);
            }
            else
            {
                PopulateLabourDropDownList();
            }

           
            return View(await lab.ToListAsync());
        }


        public void PopulateLabourDropDownList(object selectedLabour = null)
        {
            var LaboursQuery = (from labour in _context.labours
                                orderby labour.Name
                                select new { labour.Id, labour.Name });
            ViewBag.LabourId = new SelectList(LaboursQuery.AsNoTracking(), "Id", "Name", selectedLabour);
        }

        public void PopulateSiteDropDownList(object selectedSite = null)
        {
            if (selectedSite != null)
            {
                var ss = (from s in _context.Sites where s.Id == Convert.ToInt32(selectedSite) select s.Name);
                ViewBag.SelectedSite = ss.FirstOrDefault().ToString();
            }
            else
            {
                ViewBag.SelectedSite = null;
            }
            var SitesQuery = (from site in _context.Sites
                              orderby site.Name
                              select new { site.Id, site.Name });
            ViewBag.SiteId = new SelectList(SitesQuery.AsNoTracking(), "Id", "Name", selectedSite);
        }








        // GET: MaterialExpenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialExpense = await _context.MaterialExpenses
                .Include(m => m.Labour)
                .Include(m => m.Site)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materialExpense == null)
            {
                return NotFound();
            }

            return View(materialExpense);
        }

        // GET: MaterialExpenses/Create
        public IActionResult Create()
        {

            ViewBag.mm =  _context.MaterialExpenses.Include(m => m.Labour).Include(m => m.Site).OrderByDescending(o => o.Id).Take(5);



            ViewData["LabourId"] = new SelectList(_context.labours, "Id", "Name");
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Name");
            return View();
        }

        // POST: MaterialExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SiteId,LabourId,Date,InvoiceNo,Supplier,Description,Amount")] MaterialExpense materialExpense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materialExpense);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Success!";
                return RedirectToAction(nameof(Create));
            }
            ViewData["LabourId"] = new SelectList(_context.labours, "Id", "Name", materialExpense.LabourId);
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Name", materialExpense.SiteId);
            return View(materialExpense);
        }

        // GET: MaterialExpenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialExpense = await _context.MaterialExpenses.FindAsync(id);
            if (materialExpense == null)
            {
                return NotFound();
            }
            ViewData["LabourId"] = new SelectList(_context.labours, "Id", "Name", materialExpense.LabourId);
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Name", materialExpense.SiteId);
            return View(materialExpense);
        }

        // POST: MaterialExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SiteId,LabourId,Date,InvoiceNo,Supplier,Description,Amount")] MaterialExpense materialExpense)
        {
            if (id != materialExpense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materialExpense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialExpenseExists(materialExpense.Id))
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
            ViewData["LabourId"] = new SelectList(_context.labours, "Id", "Name", materialExpense.LabourId);
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Name", materialExpense.SiteId);
            return View(materialExpense);
        }

        // GET: MaterialExpenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialExpense = await _context.MaterialExpenses
                .Include(m => m.Labour)
                .Include(m => m.Site)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materialExpense == null)
            {
                return NotFound();
            }

            return View(materialExpense);
        }

        // POST: MaterialExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materialExpense = await _context.MaterialExpenses.FindAsync(id);
            _context.MaterialExpenses.Remove(materialExpense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialExpenseExists(int id)
        {
            return _context.MaterialExpenses.Any(e => e.Id == id);
        }
    }
}
