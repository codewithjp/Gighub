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
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gighub.Controllers.Api
{
    [ApiController]
    [Authorize]
    [Route("/api/{controller}")]
    public class AttendancesController : Controller
    {
        private readonly IGigHubService _gigHubService;
        private readonly UserManager<AppUser> _userManager;
        public AttendancesController(IGigHubService gigHubService,  UserManager<AppUser> userManager)
        {
            _gigHubService = gigHubService;
            _userManager = userManager;
        }

        [HttpGet("GetGenres")]
        public async Task<IActionResult> GetGenres()
        {
           var genres= await _gigHubService.GetGenres();
            return Ok(genres);
        }


        [HttpPost("Attend/{id}")]
        public async Task<IActionResult> Attend(int id)
        {
            CommonResponse<int> commonResponse = new();
           
            try
            {
                var attendance = new Attendance
                {
                    GigId = id,
                    Userid = _userManager.GetUserId(User)  //User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                commonResponse.Status = await _gigHubService.ToggleAttendance(attendance);
                commonResponse.Message = commonResponse.Status==1 ? Helper.attendanceAdded : Helper.attendanceDeleted;
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }
    }
}
