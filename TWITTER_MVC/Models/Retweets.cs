using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWITTER_MVC.Models
{
    public class Retweets
    {
        [Key]
        public int RetweetId { get; set; }
        public int KullanıcıId { get; set; }
        public int TweetID { get; set; }
    }
}
