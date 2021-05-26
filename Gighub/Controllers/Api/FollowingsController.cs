using Gighub.Dtos;
using Gighub.Models;
using Gighub.Service;
using Gighub.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gighub.Controllers.Api
{
    [Route("/api/{controller}")]
    [ApiController]
    [Authorize]
    public class FollowingsController : ControllerBase
    {

        private readonly IGigHubService _gigHubService;

        public FollowingsController(IGigHubService gigHubService)
        {
            _gigHubService = gigHubService;
        }

        [HttpPost("Follow")]
        public async Task<IActionResult> Follow(FollowingsDto dto)
        {
            CommonResponse<int> commonResponse = new();
            try
            {
                var following = new Following
                {
                    FolloweeId = dto.FolloweeId,
                    FollowerId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                commonResponse.Status = await _gigHubService.SaveFollowing(following);
                commonResponse.Message = commonResponse.Status == 1 ? Helper.followerAdded : Helper.followerExist;
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
