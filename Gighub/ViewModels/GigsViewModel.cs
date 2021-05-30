using Gighub.Controllers;
using Gighub.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gighub.ViewModels
{
    public class GigsViewModel
    {
        public int GigId { get; set; }

        [Required]
        [FutureDates(ErrorMessage ="Only future dates allowed")]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        [MaxLength(length:50,ErrorMessage ="Maximum allowed length is 50"),MinLength(length:10,ErrorMessage ="Minimum 10 characthers required")]
        //[StringLength(100, ErrorMessage = "The {0} must be atleast {2} characters long", MinimumLength = 6)]
        public string Venue { get; set; }

        [Required]
        [Display(Name ="Genre")]
        public int GenreId { get; set; }

        public IEnumerable<Gig> UpComingGigs { get;  set; }

        public string Heading { get; set; }


        public string Action { get
            {
               Expression< Func<GigsController,IActionResult>> create = (c => c.Create(this));
               Expression< Func<GigsController,IActionResult>> update = (c => c.Update(this));
                var action = GigId != 0 ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            } }

        public string Query { get;  set; }
        public ILookup<int, Attendance> Attendances { get;  set; }
        public IEnumerable<Following> Followings { get;  set; }

        public DateTime GetDateTime() 
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}
