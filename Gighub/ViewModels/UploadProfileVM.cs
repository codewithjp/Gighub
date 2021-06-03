using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.ViewModels
{
    public class UploadProfileVM
    {
        [Required(ErrorMessage ="Please select a picture.")]
        [DisplayName("Profile Picture")]
        public IFormFile ProfileImage { get; set; }
        public string FilePath{ get; set; }
        public string FileName{ get; set; }
        public bool Isuploaded { get; set; }
    }
}
