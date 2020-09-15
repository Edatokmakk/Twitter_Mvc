using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWITTER_MVC.Models
{
    public class Follower
    {
        [Key]
        public int Id { get; set; }
        public int FollowID { get; set; }
        public int UserID { get; set; }
        public User User { get;set; }
    }
}
