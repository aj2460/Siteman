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
    public class LabourReceipts : Controller
    {
        private readonly AppDbContext _context;

        public LabourReceipts(AppDbContext context)
        {
            _context = context;
        }

        // GET: LabourReceipts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.labourReceipts.Include(l => l.Labour);
            return View(await appDbContext.ToListAsync());
        }

        // GET: LabourReceipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labourReceipt = await _context.labourReceipts
                .Include(l => l.Labour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labourReceipt == null)
            {
                return NotFound();
            }

            return View(labourReceipt);
        }

        // GET: LabourReceipts/Create
        public IActionResult Create()
        {
            ViewData["LabourId"] = new SelectList(_context.labours, "Id", "Name");
            return View();
        }

        // POST: LabourReceipts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LabourId,Date,Amount,Dscription")] LabourReceipt labourReceipt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labourReceipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LabourId"] = new SelectList(_context.labours, "Id", "Name", labourReceipt.LabourId);
            return View(labourReceipt);
        }

        // GET: LabourReceipts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labourReceipt = await _context.labourReceipts.FindAsync(id);
            if (labourReceipt == null)
            {
                return NotFound();
            }
            ViewData["LabourId"] = new SelectList(_context.labours, "Id", "Category", labourReceipt.LabourId);
            return View(labourReceipt);
        }

        // POST: LabourReceipts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LabourId,Date,Amount,Dscription")] LabourReceipt labourReceipt)
        {
            if (id != labourReceipt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labourReceipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabourReceiptExists(labourReceipt.Id))
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
            ViewData["LabourId"] = new SelectList(_context.labours, "Id", "Name", labourReceipt.LabourId);
            return View(labourReceipt);
        }

        // GET: LabourReceipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labourReceipt = await _context.labourReceipts
                .Include(l => l.Labour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labourReceipt == null)
            {
                return NotFound();
            }

            return View(labourReceipt);
        }

        // POST: LabourReceipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labourReceipt = await _context.labourReceipts.FindAsync(id);
            _context.labourReceipts.Remove(labourReceipt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabourReceiptExists(int id)
        {
            return _context.labourReceipts.Any(e => e.Id == id);
        }
    }
}
