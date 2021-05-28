using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Models
{
    public class Notification
    {   
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Type { get; set; }
        public DateTime? OrignalDateTime { get; set; }
        public string OrignalVenue { get; set; }

        [Required]
        public Gig Gig { get; set; }
    }
}
