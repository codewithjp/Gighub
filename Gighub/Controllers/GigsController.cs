using Gighub.Models;
using Gighub.Service;
using Gighub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gighub.Controllers
{
   
    [Authorize]
    public class GigsController : Controller
    {
        private readonly IGigHubService _gigHubService;
       
        public GigsController(IGigHubService gigHubService)
        {
            _gigHubService = gigHubService;
          
        }
        
        // GET: GigsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GigsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GigsController/Create
        public  IActionResult Create()
        {
            ViewBag.GenreList = _gigHubService.GetGenres().GetAwaiter().GetResult();
            return View();
        }

        // POST: GigsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigsViewModel model)
        {
            ViewBag.GenreList = _gigHubService.GetGenres().GetAwaiter().GetResult();
            if (!ModelState.IsValid)
                return View(model);
            var gig = new Gig
            {
                Venue = model.Venue,
                DateTime =model.GetDateTime() ,    //DateTime.Parse(string.Format("{0} {1}", model.Date,model.Time)),
                GenreId = model.GenreId,
                ArtistId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            _gigHubService.SaveGig(gig);

            return RedirectToAction(nameof(Index));
        }

        // GET: GigsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GigsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GigsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GigsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
