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
    public class LabourExpenses : Controller
    {
        private readonly AppDbContext _context;

        public LabourExpenses(AppDbContext context)
        {
            _context = context;
        }

        // GET: LabourExpenses
        public async Task<IActionResult> Index(int page=1)
        {
            var appDbContext = _context.LabourExpenses.Include(l => l.ExpenseType).Include(l => l.Labour).Include(l => l.Site).OrderByDescending(o=>o.Id);
            PopulateSiteDropDownList();
            PopulateLabourDropDownList();


            return View(await appDbContext.ToListAsync());
        }



        [HttpPost]
        public async Task<IActionResult> Index()
        {

            var lab = _context.LabourExpenses.Include(l => l.ExpenseType).Include(l => l.Labour).Include("Site");
            if (Request.Form["SiteId"] != "")
            {
                int s = Convert.ToInt32(Request.Form["SiteId"]);
                PopulateSiteDropDownList(s);
                lab = _context.LabourExpenses.Include(l => l.ExpenseType).Include(l => l.Labour).Include("Site").Where(o => o.SiteId == s);
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






        // GET: LabourExpenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labourExpense = await _context.LabourExpenses
                .Include(l => l.ExpenseType)
                .Include(l => l.Labour)
                .Include(l => l.Site)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labourExpense == null)
            {
                return NotFound();
            }

            return View(labourExpense);
        }

        // GET: LabourExpenses/Create
        public IActionResult Create()
        {
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpType, "Id", "ExType");
            ViewData["LabourId"] = new SelectList(_context.labours, "Id", "Name");
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Name");
            ViewBag.mm =   _context.LabourExpenses.Include(l => l.ExpenseType).Include(l => l.Labour).Include(l => l.Site).OrderByDescending(o => o.Id).Take(5);
            return View();
        }

        // POST: LabourExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SiteId,LabourId,ExpenseTypeId,ExpDate,Particular,Day,Wage,Description")] LabourExpense labourExpense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labourExpense);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Success!";
                return RedirectToAction(nameof(Create));
            }
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpType, "Id", "ExType", labourExpense.ExpenseTypeId);
            ViewData["LabourId"] = new SelectList(_context.labours, "Id", "Name", labourExpense.LabourId);
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Name", labourExpense.SiteId);
            return View(labourExpense);
        }

        // GET: LabourExpenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labourExpense = await _context.LabourExpenses.FindAsync(id);
            if (labourExpense == null)
            {
                return NotFound();
            }
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpType, "Id", "ExType", labourExpense.ExpenseTypeId);
            ViewData["LabourId"] = new SelectList(_context.labours, "Id", "Name", labourExpense.LabourId);
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Name", labourExpense.SiteId);
            return View(labourExpense);
        }

        // POST: LabourExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SiteId,LabourId,ExpenseTypeId,ExpDate,Particular,Day,Wage,Description")] LabourExpense labourExpense)
        {
            if (id != labourExpense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labourExpense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabourExpenseExists(labourExpense.Id))
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
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpType, "Id", "ExType", labourExpense.ExpenseTypeId);
            ViewData["LabourId"] = new SelectList(_context.labours, "Id", "Name", labourExpense.LabourId);
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Name", labourExpense.SiteId);
            return View(labourExpense);
        }

        // GET: LabourExpenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labourExpense = await _context.LabourExpenses
                .Include(l => l.ExpenseType)
                .Include(l => l.Labour)
                .Include(l => l.Site)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labourExpense == null)
            {
                return NotFound();
            }

            return View(labourExpense);
        }

        // POST: LabourExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labourExpense = await _context.LabourExpenses.FindAsync(id);
            _context.LabourExpenses.Remove(labourExpense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabourExpenseExists(int id)
        {
            return _context.LabourExpenses.Any(e => e.Id == id);
        }

        [HttpPost]
        public float GetWage(int LabourId)
        {
            int id = 0;
            if (Request.Form["LabourId"] != "")
            {
                id = Convert.ToInt32(Request.Form["LabourId"]);
            }
            Console.Write(LabourId);
                var lab = _context.labours.Where(o=>o.Id == LabourId).Select(o=> new { id=o.Id,wage=o.Wage}).ToList();
            var re = lab[0].wage;
            return re;
        }
    }
}
