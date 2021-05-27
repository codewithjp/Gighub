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
                var gigInDb = _gigHubService.GetGigsByGigId(id, _userManager.GetUserId(User));
                gigInDb.IsCanceled = true;

              await _gigHubService.SaveChangesAsync();
                commonResponse.Status = Helper.success_code;
                commonResponse.Message = Helper.cancelGigSuccess;
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
