using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteManagement.DbLayer;
using SiteManagement.Models;

namespace SiteManagement.wwwroot
{
    public class Employee : Controller
    {
        private readonly AppDbContext _context;

        public Employee(AppDbContext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            return View(await _context.labours.ToListAsync());
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labour = await _context.labours
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labour == null)
            {
                return NotFound();
            }

            return View(labour);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Category,Phone,Wage")] Labour labour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labour);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labour = await _context.labours.FindAsync(id);
            if (labour == null)
            {
                return NotFound();
            }
            return View(labour);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Category,Phone,Wage")] Labour labour)
        {
            if (id != labour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabourExists(labour.Id))
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
            return View(labour);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labour = await _context.labours
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labour == null)
            {
                return NotFound();
            }

            return View(labour);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labour = await _context.labours.FindAsync(id);
            _context.labours.Remove(labour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabourExists(int id)
        {
            return _context.labours.Any(e => e.Id == id);
        }
    }
}
