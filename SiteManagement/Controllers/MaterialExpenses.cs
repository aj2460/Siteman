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
            var appDbContext = _context.MaterialExpenses.Include(m => m.Labour).Include(m => m.Site);
            return View(await appDbContext.ToListAsync());
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
            ViewData["LabourId"] = new SelectList(_context.labours, "Id", "Name");
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Name");
            return View();
        }

        // POST: MaterialExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SiteId,LabourId,Date,Particular,Description,Amount")] MaterialExpense materialExpense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materialExpense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,SiteId,LabourId,Date,Particular,Description,Amount")] MaterialExpense materialExpense)
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
