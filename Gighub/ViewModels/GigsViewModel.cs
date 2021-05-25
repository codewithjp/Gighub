﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.ViewModels
{
    public class GigsViewModel
    {
        [Required]
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
    }
}