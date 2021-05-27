using Gighub.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Models
{
    public class Gig
    {
        [Key]
        public int Id { get; set; }
        public bool IsCanceled { get; set; }
        public string ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public AppUser AppUser { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public int GenreId { get; set; }
       

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }
    }
}
