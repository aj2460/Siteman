using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using SiteManagement.DbLayer;
using SiteManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Controllers
{
    public class Home:Controller
    {
        private readonly ISiteRepository _siteRepository;

        public Home(ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository;
        }

        public async Task<IActionResult> Index(int page=1)
        {
           var query=_siteRepository.GetAllSite();
            var model = await PagingList.CreateAsync(query,5,page);
            return View(model);
        }



        public IActionResult Details(int Id)
        {
            return View(_siteRepository.GetSite(Id));
        }
    }
}
