using SiteManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.DbLayer
{
    public interface ISiteRepository
    {
        Site GetSite(int Id);
        IOrderedQueryable<Site> GetAllSite();
    }
}
