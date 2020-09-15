using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TWITTER_MVC.ViewModel
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateTime { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }
        public IFormFile BackgroundImg { get; set; }
    }
}
