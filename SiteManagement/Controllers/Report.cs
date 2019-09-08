using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteManagement.DbLayer;
using SiteManagement.Models;
using SiteManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Controllers
{
    public class Report:Controller
    {

        private readonly AppDbContext _context;
        private List<EmployeeCategory> Cat;
        public Report(AppDbContext context)
        {
            _context = context;

        }


        [HttpGet]
        public IActionResult LabourBalance()
        {
            IEnumerable<LabourReport> LabExp = _context.labours
               .Include(p => p.MaterilaExpenses)
               .Include(p => p.LabourExpenses).ThenInclude(o => o.ExpenseType)
               .Include(p => p.LabourReceipts)
               .Select(p => new LabourReport()
               {
                   labour = p,
                   materialExpense = p.MaterilaExpenses,
                   labourReceipt = p.LabourReceipts,
                   labourExpense = p.LabourExpenses.Where(o => o.ExpenseType.ExType != "Wage").ToList()
               }
               ).ToList();
            return View(LabExp);

        }



        [HttpGet]
        public IActionResult LabourReport()
        {
            var m = _context.LabourExpenses.Include(o => o.ExpenseType).Include(o => o.Labour).ThenInclude(o => o.EmployeeCategory).Include("Site");

            PopulateLabourDropDownList();
            PopulateSiteDropDownList();
            PopulateCategoryDropDownList();
            return View(m);
            
        }

        [HttpPost]
        public IActionResult LabourReport(LabourReport labourReport)
        {
            var lab = _context.LabourExpenses.Include(o=>o.ExpenseType).Include(o => o.Labour).ThenInclude(o => o.EmployeeCategory).Include("Site");
            if (Request.Form["SiteId"] != "")
            {
                int s = Convert.ToInt32(Request.Form["SiteId"]);
                PopulateSiteDropDownList(s);
                lab = _context.LabourExpenses.Include(o => o.ExpenseType).Include(o => o.Labour).ThenInclude(o => o.EmployeeCategory).Include("Site").Where(o => o.SiteId == s);
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

            PopulateCategoryDropDownList();
           

            return View(lab);

        }




        public IActionResult SiteSummary()
        {
            //IEnumerable<LabourReport> x = _context.labours.GroupJoin(_context.MaterialExpenses, d => d.Id, o => o.LabourId, (labour, expense) => new LabourReport() { labour = labour,  materialExpense = expense });
            //IEnumerable<LabourReport> x = _context.labours.GroupJoin(_context.MaterialExpenses.Include(s=>s.Site), d => d.Id, o => o.LabourId, (labour, expense) => new LabourReport() { labour = labour, materialExpense = expense });
            //IEnumerable<LabourReport> x = _context.labours.Include(o => o.LabourExpenses).GroupJoin(_context.MaterialExpenses.Include(s => s.Site), d => d.Id, o => o.LabourId, (labour, expense) => new LabourReport() { labour = labour, materialExpense = expense });
            //IEnumerable<LabourReport> x = _context.labours.Include(o => o.LabourExpenses).GroupJoin(_context.MaterialExpenses.Include(s => s.Site), d => d.Id, o => o.LabourId, (labour, expense) => new LabourReport() { labour = labour, materialExpense = expense });

            // IEnumerable<MaterialExpense> m= _context.MaterialExpenses.Where(o => o.SiteId == 2);
            //  IEnumerable<LabourExpense> l = _context.LabourExpenses.Where(o => o.SiteId == 2);
            //  IEnumerable<LabourReport> x = _context.labours.Include(o => o.LabourExpenses).GroupJoin(m, d => d.Id, o => o.LabourId, (labour, expense) => new LabourReport() { labour = labour, materialExpense = expense });

            //var x = _context.Sites
            //    .Include(a => a.MaterialExpenses)
            //    .ThenInclude(b => b.Labour).Where(p=>p.Id==1)
            //    .Include(l => l.LabourExpenses)
            //   .ThenInclude(b => b.Labour).First(o => o.Id == 1);

            //var lab = _context.labours.FirstOrDefault<Labour>();
            //var xx = _context.Entry(lab)
            //    .Collection(s => s.MaterilaExpenses)
            //    .Query()
            //    .Where(s => s.SiteId == 1);

            IEnumerable<LabourReport> x = _context.labours
                .Include(p => p.MaterilaExpenses).ThenInclude(o=>o.Site)
                .Include(p => p.LabourExpenses).ThenInclude(o => o.Site)
                .Select(p => new LabourReport()
                {
                    labour=p,
                    materialExpense = p.MaterilaExpenses.Where(s => s.SiteId == 2),
                    labourExpense = p.LabourExpenses.Where(s=>s.SiteId==2)
                }
                ).ToList();

            PopulateSiteDropDownList();



            return View(x);
        }

        [HttpPost]
        public IActionResult SiteSummary(LabourReport labourReport)
        {
            IEnumerable<LabourReport> LabSum = _context.labours
               .Include(p => p.MaterilaExpenses).ThenInclude(o => o.Site)
               .Include(p => p.LabourExpenses).ThenInclude(o => o.Site)
               .Select(p => new LabourReport()
               {
                   labour = p,
                   materialExpense = p.MaterilaExpenses,
                   labourExpense = p.LabourExpenses
               }
               ).ToList();
            if (Request.Form["SiteId"] != "")
            {
                int siteId = Convert.ToInt32(Request.Form["SiteId"]);
                 LabSum = _context.labours
                .Include(p => p.MaterilaExpenses).ThenInclude(o => o.Site)
                .Include(p => p.LabourExpenses).ThenInclude(o => o.Site)
                .Select(p => new LabourReport()
                {
                    labour = p,
                    materialExpense = p.MaterilaExpenses.Where(s => s.SiteId == siteId),
                    labourExpense = p.LabourExpenses.Where(s => s.SiteId == siteId)
                }
                ).ToList();
                PopulateSiteDropDownList(siteId);
            }
            else
            {
               
                PopulateSiteDropDownList();
            }
            return View(LabSum);
        }

            public IActionResult MaterialReport()
        {
            var m = _context.MaterialExpenses.Include(o=>o.Labour).ThenInclude(o=>o.EmployeeCategory).Include("Site");
            
            PopulateLabourDropDownList();
            PopulateSiteDropDownList();
            PopulateCategoryDropDownList();
            return View(m);
        }

        [HttpPost]
        public IActionResult MaterialReport(LabourReport labourReport)
        {
            var lab  = _context.MaterialExpenses.Include(o => o.Labour).ThenInclude(o => o.EmployeeCategory).Include("Site");
            if (Request.Form["SiteId"] != ""){
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

            PopulateCategoryDropDownList();
            //if (Request.Form["CategoryId"] != "")
            //{
            //    int c = Convert.ToInt32(Request.Form["CategoryId"]);
            //    PopulateLabourDropDownList(c);
            //    lab = lab.Where(o => o.Labour.Category == c);
            //}

            return View(lab);
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
                var ss = (from s in _context.Sites where s.Id == Convert.ToInt32(selectedSite) select  s.Name );
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



        public void PopulateCategoryDropDownList(object selectedCategory = null)
        {
            var CategoryQuery = (from category in _context.EmployeeCategories
                                 orderby category.Name
                                 select new { category.Id, category.Name });
            ViewBag.CategoryId = new SelectList(CategoryQuery.AsNoTracking(), "Id", "Name", selectedCategory);
        }
    }
}
