using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IEnumerable<Site> GetAllSite()
        {
            return context.Sites;
        }

        public Site GetSite(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
 