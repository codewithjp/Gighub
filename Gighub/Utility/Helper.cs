using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Utility
{
    public static class Helper
    {
        
        public const string Artist= "Artist";
        public const string Admin = "Admin";
        public const string Follower = "Follower";


        public static IEnumerable<SelectListItem> GetRoles(bool isAdmin)
        {
            if (isAdmin)
                return new List<SelectListItem> {
                    new SelectListItem { Text=Admin ,Value=Admin}
                };
            else
                return new List<SelectListItem> { 
                new SelectListItem{Text=Artist,Value=Artist},
                new SelectListItem{Text=Follower,Value=Follower},
                };
        }
    }
}
