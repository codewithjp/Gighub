using Gighub.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Models
{
    public class UserNotification
    {
       
        public string UserId { get; set; }
        public int NotificationId { get; set; }

        [ForeignKey("UserId")]
        public AppUser   AppUser { get; set; }
        public Notification Notification { get; set; }
        public bool IsRead { get; set; }
    }
}
