
using Gighub.Models;
using Gighub.Service;
using Gighub.Utility;
using Gighub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGigHubService _gigHubService;
       public HomeController(ILogger<HomeController> logger, IGigHubService gigHubService)
        {
            _logger = logger;
            _gigHubService = gigHubService;
        }

        [HttpPost]
        public ActionResult Search(string query)
        {

            return RedirectToAction("Index", new { query });
        }


        public IActionResult Index(string query=null)
        {
           var gigs = _gigHubService.GetGigs();

            if (!String.IsNullOrEmpty(query))
            {
                gigs = _gigHubService.Search(query);
                ViewBag.Query = query;
            }
            var gigViewModel = new GigsViewModel()
            {
                UpComingGigs = gigs,
                Heading = query == null ? Helper.hUpcomingGigs : Helper.hSearch,
                Query=query
            };
            return View("Gigs",gigViewModel); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
