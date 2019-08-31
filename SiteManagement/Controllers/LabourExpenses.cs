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
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.LabourExpenses.Include(l => l.Labour).Include(l => l.Site);
            return View(await appDbContext.ToListAsync());
        }

        // GET: LabourExpenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labourExpense = await _context.LabourExpenses
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
            ViewData["LabourId"] = new SelectList(_context.labours, "Id", "Name");
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Name");
            return View();
        }

        // POST: LabourExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SiteId,LabourId,Date,Day,Wage,Description")] LabourExpense labourExpense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labourExpense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            ViewData["LabourId"] = new SelectList(_context.labours, "Id", "Name", labourExpense.LabourId);
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Name", labourExpense.SiteId);
            return View(labourExpense);
        }

        // POST: LabourExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SiteId,LabourId,Date,Day,Wage,Description")] LabourExpense labourExpense)
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
    }
}
