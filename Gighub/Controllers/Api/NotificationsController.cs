using AutoMapper;
using Gighub.Data;
using Gighub.Dtos;
using Gighub.Models;
using Gighub.Service;
using Gighub.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly IGigHubService _gigHubService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public NotificationsController(IGigHubService gigHubService,
            UserManager<AppUser> userManager,IMapper mapper)
        {
            _gigHubService = gigHubService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("GetNotification")]
        public async Task<IActionResult> GetNotification()
        {
            CommonResponse<IEnumerable<NotificationDto>> commonResponse = new();
            var userId =  _userManager.GetUserId(User);

            var notification = await _gigHubService.GetUserNotificationAsync(userId);

            commonResponse.Dataenum = notification.Select(_mapper.Map<Notification, NotificationDto>);
           
            return Ok(commonResponse);
        }


        [HttpPost("MarkAsRead")]
        public async Task<IActionResult> MarkAsRead()
        {
          var userId = _userManager.GetUserId(User);
           await _gigHubService.ReadNotificationAsync(userId);
            return Ok();
        }
    }
}
