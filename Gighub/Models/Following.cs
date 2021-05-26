using Gighub.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Models
{
    public class Following
    {
        public string FollowerId { get; set; }


        [ForeignKey("FollowerId")]
        public AppUser Follower { get; set; }
        public string FolloweeId { get; set; }

        [ForeignKey("FolloweeId")]  
        public AppUser Followee { get; set; }
    }
}
