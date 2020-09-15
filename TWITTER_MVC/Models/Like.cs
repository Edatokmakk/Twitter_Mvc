using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TWITTER_MVC.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public int UserID { get; set; }
        public int TweetID { get; set; }

    }
}
