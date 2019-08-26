using Microsoft.AspNetCore.Mvc;
using SiteManagement.DbLayer;
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
        public ViewResult Index()
        {
            return View(_siteRepository.GetAllSite());
        }
    }
}
