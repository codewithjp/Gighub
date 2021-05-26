using Gighub.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
      
        public int GigId { get; set; }

        [ForeignKey("GigId")]
        public Gig Gig { get; set; }

        public string Userid { get; set; }

        [ForeignKey("Userid")]
        public AppUser  AppUser { get; set; }
    }
}
