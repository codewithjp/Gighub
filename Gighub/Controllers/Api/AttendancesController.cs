using Gighub.Service;
using Gighub.Utility;
using Microsoft.AspNetCore.Authorization;
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

        public AttendancesController(IGigHubService gigHubService)
        {
            _gigHubService = gigHubService;
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
                commonResponse.Status = await _gigHubService.SaveAttendance(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
                commonResponse.Message = commonResponse.Status==1 ? Helper.attendanceAdded : Helper.attendanceExists;
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
