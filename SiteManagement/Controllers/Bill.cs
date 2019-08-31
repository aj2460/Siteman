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
    public class Bill : Controller
    {
        private readonly AppDbContext _context;
      
        public Bill(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            //var LabourList = (from labour in _context.labours
            //                  select new { labour.Id, labour.Name }).ToList();
            //ViewBag.ListOfLabour = LabourList;

            //var SiteList = (from site in _context.Sites
            //                  select new { site.Id, site.Name }).ToList();
            //ViewBag.ListOfSite = SiteList;

            
            PopulateLabourDropDownList();
            PopulateSiteDropDownList();
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( MaterialExpense materialExpense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materialExpense);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Success!";

                return RedirectToAction(nameof(Create));

            }
            // PopulateDepartmentsDropDownList(course.DepartmentID);
            PopulateLabourDropDownList(materialExpense.LabourId);
            PopulateSiteDropDownList(materialExpense.SiteId);
           
            return View(materialExpense);
        }



        public void  PopulateLabourDropDownList(object selectedLabour = null)
        {
            var LaboursQuery = (from labour in _context.labours
                                orderby labour.Name
                                select new { labour.Id, labour.Name });
            ViewBag.LabourId = new SelectList(LaboursQuery.AsNoTracking(), "Id", "Name", selectedLabour);
        }

        public void PopulateSiteDropDownList(object selectedSite = null)
        {
            var SitesQuery = (from site in _context.Sites
                                orderby site.Name
                                select new { site.Id, site.Name });
            ViewBag.SiteId = new SelectList(SitesQuery.AsNoTracking(), "Id", "Name", selectedSite);
        }


    }
}