using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWITTER_MVC.Models
{
    public class Tweets
    {
        [Key]
        public int TweetID { get; set; }
        public string tweet { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate{ get; set; }
        public string UserName { get; set; }
        public string UsName { get; set; }
        public User User { get; set; }
        public string ProfilePicture { get; set; }

        public int Likes { get; set; }
        public int Retweets { get; set; }
    }
}
