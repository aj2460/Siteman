using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Models;

namespace SiteManagement.DbLayer
{
    public class SQLSiteRepository : ISiteRepository
    {
        private readonly AppDbContext context;

        public SQLSiteRepository(AppDbContext context)
        {
            this.context = context;
        }

        public ICollection<Site> GetAllSite()
        {
            // var st=context.Sites.GroupJoin(context.MaterialExpenses.Select(e=>e.Id),s=>s.Id,m=>m.)

            var st = context.Sites
                .Include(a => a.MaterialExpenses)
                .Include(l=>l.LabourExpenses)
               .ThenInclude(b => b.Labour).ToList();

           

            return st;
        }

        public Site GetSite(int Id)
        {
            var st = context.Sites
                .Include(a => a.MaterialExpenses)
                .ThenInclude(b => b.Labour)
                .Include(l => l.LabourExpenses)
               .ThenInclude(b => b.Labour).First(o => o.Id == Id);



            return st;
        }
    }
}
 