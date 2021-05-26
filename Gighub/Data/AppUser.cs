using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Data
{
    public class AppUser:IdentityUser
    {

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public bool IsPhotoUploaded { get; set; }
    }
}
