using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Dtos
{
    public class NotificationDto
    {
       
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Type { get; set; }
        public DateTime? OrignalDateTime { get; set; }
        public string OrignalVenue { get; set; }
        
        public GigDto GigDto { get; set; }
    }
}
