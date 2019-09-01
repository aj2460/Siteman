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
    public class EmployeeCategories : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeCategories(AppDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmployeeCategories.ToListAsync());
        }

        // GET: EmployeeCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeCategory = await _context.EmployeeCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeCategory == null)
            {
                return NotFound();
            }

            return View(employeeCategory);
        }

        // GET: EmployeeCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] EmployeeCategory employeeCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeCategory);
        }

        // GET: EmployeeCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeCategory = await _context.EmployeeCategories.FindAsync(id);
            if (employeeCategory == null)
            {
                return NotFound();
            }
            return View(employeeCategory);
        }

        // POST: EmployeeCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] EmployeeCategory employeeCategory)
        {
            if (id != employeeCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeCategoryExists(employeeCategory.Id))
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
            return View(employeeCategory);
        }

        // GET: EmployeeCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeCategory = await _context.EmployeeCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeCategory == null)
            {
                return NotFound();
            }

            return View(employeeCategory);
        }

        // POST: EmployeeCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeCategory = await _context.EmployeeCategories.FindAsync(id);
            _context.EmployeeCategories.Remove(employeeCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeCategoryExists(int id)
        {
            return _context.EmployeeCategories.Any(e => e.Id == id);
        }
    }
}
