using System;

namespace Gighub.Dtos
{
    public class GigDto
    {
        
        public int Id { get; set; }
        public bool IsCanceled { get; set; }
       
        public AppUserDto Artist { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
       
        public GenreDto GenreDto { get; set; }
    }
}