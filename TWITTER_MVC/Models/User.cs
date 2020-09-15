using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TWITTER_MVC.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Adın nedir?")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Adın nedir?")]
        public string Name { get; set; }
        [Required(ErrorMessage = "İçinde en az 6 rakam ,harf ve noktalama işareti bulunan bir şifre gir.")]
        public string Password { get; set; }
        public string ProfilePicture { get; set; }
        public string BackgroundImg { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateTime { get; set; }
      

    }
}
