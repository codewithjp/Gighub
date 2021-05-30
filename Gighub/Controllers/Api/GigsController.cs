using Gighub.Data;
using Gighub.Models;
using Gighub.Service;
using Gighub.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Gighub.Utility.Helper;

namespace Gighub.Controllers.Api
{

    [ApiController]
    [Authorize]
    [Route("/api/{controller}")]
    public class GigsController : Controller
    {
        private readonly IGigHubService _gigHubService;
        private readonly UserManager<AppUser> _userManager;
        public GigsController(IGigHubService gigHubService, UserManager<AppUser> userManager)
        {
            _gigHubService = gigHubService;
            _userManager = userManager;
        }

        [HttpDelete]
        [Route("Cancel/{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            CommonResponse<int> commonResponse = new();
            try
            {
                var gigInDb = _gigHubService.GetGigsByGigIdAndUserId(id, _userManager.GetUserId(User));
               if(gigInDb.IsCanceled)
                {
                    commonResponse.Status = Helper.failure_code;
                    commonResponse.Message = Helper.cancelGigFail;
                }
                else
                {
                    gigInDb.IsCanceled = true;
                    await _gigHubService.SaveChangesAsync();

                    // Send Notification to Attendee
                    var notification = new Notification
                    {
                        DateTime = DateTime.Now,
                       Type =(int)NotificationType.GigCanceled,
                        Gig = gigInDb
                    };

                    await _gigHubService.SendNotification(notification, id);
                    commonResponse.Status = Helper.success_code;
                    commonResponse.Message = Helper.cancelGigSuccess;
                }
            }
            catch (Exception e)
            {
                commonResponse.Status = Helper.failure_code;
                commonResponse.Message = e.Message;
            }

            return Ok(commonResponse);
        }
    }
}
