using Gighub.Data;
using Gighub.Models;
using Gighub.Service;
using Gighub.Utility;
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
using static Gighub.Utility.Helper;

namespace Gighub.Controllers
{
   
    [Authorize]
    public class GigsController : Controller
    {
        private readonly IGigHubService _gigHubService;
        private readonly UserManager<AppUser> _userManager;
       
        public GigsController(IGigHubService gigHubService, UserManager<AppUser> userManager)
        {
            _gigHubService = gigHubService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Mine()
        {
            var gigs = await _gigHubService.GetGigsByUserId(_userManager.GetUserId(User));
            return View(gigs);
        }


        public async Task<IActionResult> Attending()
        {
            var gigs = await _gigHubService.GetGigsAttending(_userManager.GetUserId(User));
            var gigViewModel = new GigsViewModel()
            {
                UpComingGigs = gigs,
                Heading = Helper.hGigsIamAttending
                
            };
            return View("Gigs",gigViewModel);
        }

       

        // GET: GigsController/Create
        public  IActionResult Create()
        {
            ViewBag.GenreList = _gigHubService.GetGenres().GetAwaiter().GetResult();
            var gigViewModel = new GigsViewModel
            {
                Heading = Helper.hAddGig
            };
            return View("GigsForm",gigViewModel);
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
                ArtistId = User.FindFirstValue(ClaimTypes.NameIdentifier),
              
            };

            _gigHubService.SaveGig(gig); //Save a Gig

            return RedirectToAction(nameof(Mine));
        }

        // GET: GigsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
           
            ViewBag.GenreList = await _gigHubService.GetGenres();
            var gig = _gigHubService.GetGigsByGigId(id, _userManager.GetUserId(User));
            var gigViewModel = new GigsViewModel
            {
                GenreId = gig.GenreId,
                Venue = gig.Venue,
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                GigId=gig.Id,
                Heading=Helper.hEditGig
            };

            return View("GigsForm", gigViewModel);
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigsViewModel model)
        {
            ViewBag.GenreList =  _gigHubService.GetGenres().GetAwaiter().GetResult();

            if (!ModelState.IsValid)
                return View(model);
            var gig = new Gig
            {
                Venue = model.Venue,
                DateTime = model.GetDateTime(),   
                GenreId = model.GenreId,
                ArtistId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Id = model.GigId
            };
            // Notification
            var notification = new Notification
            {
                DateTime = DateTime.Now,
                Type = (int)NotificationType.GigUpdated,
                Gig = gig
            };
             _gigHubService.SendNotification(notification, model.GigId);

            _gigHubService.UpdateGig(gig); //Update a gig
            return RedirectToAction(nameof(Mine));
        }

       
       
    }
}
