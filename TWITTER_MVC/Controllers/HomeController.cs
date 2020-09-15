using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;
using TWITTER_MVC.Data;
using TWITTER_MVC.Models;

using Microsoft.EntityFrameworkCore;
using TWITTER_MVC.ViewModel;

namespace TWITTER_MVC.Controllers
{
    public class HomeController : Controller
    {

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly TWITTER_MVCContext _context;
        [Obsolete]
        private readonly IHostingEnvironment webHostEnvironment;

        [Obsolete]
        public HomeController(TWITTER_MVCContext context, IHostingEnvironment hostenvironment)
        {
            _context = context;
            webHostEnvironment = hostenvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login");
            }
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.UserName ="@"+ HttpContext.Session.GetString("UserName");
            ViewBag.Password = HttpContext.Session.GetString("Password");
            ViewBag.ProfilePicture = HttpContext.Session.GetString("ProfilePicture");
            var users = (from m in _context.Users
                        where m.UserName != HttpContext.Session.GetString("UserName")
                        select m).ToList();
            

            var twits = (from m in _context.Tweets
                         orderby m.CreateDate descending
                         select m);
            User ownUser = (from s in _context.Users
                            where s.UserName == HttpContext.Session.GetString("UserName")
                            select s).FirstOrDefault<User>();

            var followed = (from f in _context.Follower
                            where f.UserID == ownUser.UserID
                            select f).ToList();
            var followedList = new List<User>();

            foreach (var item in followed)
            {
                followedList.AddRange(from s in _context.Users
                                      where s.UserID == item.FollowID
                                      select s);
            }
            ViewBag.fw = followedList;
            ViewBag.Users = users.Except(followedList);
            ViewBag.Twet = twits.ToList();
            
            return View();
        }
        [HttpPost]
        public IActionResult Tweet(Tweets tweets)
        {
            tweets.UserName = HttpContext.Session.GetString("UserName");
            tweets.User = (from s in _context.Users
                        where s.UserName == tweets.UserName
                        select s).FirstOrDefault<User>();
            tweets.UsName = tweets.User.Name;
            _context.Add(tweets);
            tweets.UserName = "@" + HttpContext.Session.GetString("UserName");
            tweets.ProfilePicture = tweets.User.ProfilePicture;
            Response.Cookies.Append("LastLoggedInTime", DateTime.Now.ToString());
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public IActionResult Notifications()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            User ownUser = (from s in _context.Users
                            where s.UserName == HttpContext.Session.GetString("UserName")
                            select s).FirstOrDefault<User>();

            ViewBag.UserName = "@" + HttpContext.Session.GetString("UserName");
            ViewBag.Password = ownUser.Password;
            ViewBag.ProfilePicture = ownUser.ProfilePicture;
            ViewBag.BackgroundImg = ownUser.BackgroundImg;
            var users = (from m in _context.Users
                         where m.UserName != HttpContext.Session.GetString("UserName")
                         select m).ToList();

            var twits = (from t in _context.Tweets
                         where t.UserName == "@" + ownUser.UserName
                         orderby t.CreateDate descending
                         select t).ToList();
            ViewBag.UserTwet = twits;

            //get followed

            var followed = (from f in _context.Follower
                            where f.UserID == ownUser.UserID
                            select f).ToList();
            var followedList = new List<User>();
            foreach (var item in followed)
            {
                followedList.AddRange(from s in _context.Users
                                      where s.UserID == item.FollowID
                                      select s);
            }

            ViewBag.followedList = followedList;
            ViewBag.Users = users.Except(followedList);

            var followedCount = (from f in _context.Follower
                                 where f.UserID == ownUser.UserID
                                 select f).Count();
            ViewBag.followedCount = followedCount;

            var follower = (from f in _context.Follower
                            where f.FollowID == ownUser.UserID
                            select f.UserID).ToList();
            var followerList = new List<User>();
            foreach (var item in follower)
            {
                followerList.AddRange(from s in _context.Users
                                      where s.UserID == item
                                      select s);
            }

            ViewBag.follower = followerList;
            ViewBag.followerCount = followerList.Count();


            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.UserName = "@" + HttpContext.Session.GetString("UserName");
            ViewBag.Password = HttpContext.Session.GetString("Password");
            return View();
        }
        public IActionResult Messages()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            User ownUser = (from s in _context.Users
                            where s.UserName == HttpContext.Session.GetString("UserName")
                            select s).FirstOrDefault<User>();

            ViewBag.UserName = "@" + HttpContext.Session.GetString("UserName");
            ViewBag.Password = ownUser.Password;
            ViewBag.ProfilePicture = ownUser.ProfilePicture;
            ViewBag.BackgroundImg = ownUser.BackgroundImg;
            var users = (from m in _context.Users
                         where m.UserName != HttpContext.Session.GetString("UserName")
                         select m).ToList();

            var twits = (from t in _context.Tweets
                         where t.UserName == "@" + ownUser.UserName
                         orderby t.CreateDate descending
                         select t).ToList();
            ViewBag.UserTwet = twits;

            //get followed

            var followed = (from f in _context.Follower
                            where f.UserID == ownUser.UserID
                            select f).ToList();
            var followedList = new List<User>();
            foreach (var item in followed)
            {
                followedList.AddRange(from s in _context.Users
                                      where s.UserID == item.FollowID
                                      select s);
            }

            ViewBag.followedList = followedList;
            ViewBag.Users = users.Except(followedList);

            var followedCount = (from f in _context.Follower
                                 where f.UserID == ownUser.UserID
                                 select f).Count();
            ViewBag.followedCount = followedCount;

            var follower = (from f in _context.Follower
                            where f.FollowID == ownUser.UserID
                            select f.UserID).ToList();
            var followerList = new List<User>();
            foreach (var item in follower)
            {
                followerList.AddRange(from s in _context.Users
                                      where s.UserID == item
                                      select s);
            }

            ViewBag.follower = followerList;
            ViewBag.followerCount = followerList.Count();

            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.UserName = "@" + HttpContext.Session.GetString("UserName");
            ViewBag.Password = HttpContext.Session.GetString("Password");
            return View();
        }
        public IActionResult Explore()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.UserName = "@" + HttpContext.Session.GetString("UserName");
            ViewBag.Password = HttpContext.Session.GetString("Password");
            HttpContext.Session.Clear();
            return View();
        }
        public IActionResult UserExplore()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            User ownUser = (from s in _context.Users
                            where s.UserName == HttpContext.Session.GetString("UserName")
                            select s).FirstOrDefault<User>();

            ViewBag.UserName = "@" + HttpContext.Session.GetString("UserName");
            ViewBag.Password = ownUser.Password;
            ViewBag.ProfilePicture = ownUser.ProfilePicture;
            ViewBag.BackgroundImg = ownUser.BackgroundImg;
            var users = (from m in _context.Users
                         where m.UserName != HttpContext.Session.GetString("UserName")
                         select m).ToList();

            var twits = (from t in _context.Tweets
                         where t.UserName == "@" + ownUser.UserName
                         orderby t.CreateDate descending
                         select t).ToList();
            ViewBag.UserTwet = twits;

            //get followed

            var followed = (from f in _context.Follower
                            where f.UserID == ownUser.UserID
                            select f).ToList();
            var followedList = new List<User>();
            foreach (var item in followed)
            {
                followedList.AddRange(from s in _context.Users
                                      where s.UserID == item.FollowID
                                      select s);
            }

            ViewBag.followedList = followedList;
            ViewBag.Users = users.Except(followedList);

            var followedCount = (from f in _context.Follower
                                 where f.UserID == ownUser.UserID
                                 select f).Count();
            ViewBag.followedCount = followedCount;

            var follower = (from f in _context.Follower
                            where f.FollowID == ownUser.UserID
                            select f.UserID).ToList();
            var followerList = new List<User>();
            foreach (var item in follower)
            {
                followerList.AddRange(from s in _context.Users
                                      where s.UserID == item
                                      select s);
            }

            ViewBag.follower = followerList;
            ViewBag.followerCount = followerList.Count();

            ViewBag.Name =  HttpContext.Session.GetString("Name");
            ViewBag.UserName = "@" + HttpContext.Session.GetString("UserName");
            ViewBag.Password = HttpContext.Session.GetString("Password");
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            User loggedInUser = _context.Users.Where(x => x.UserName == user.UserName&&x.Password==user.Password).FirstOrDefault();
            if(loggedInUser==null)
            {
                ViewBag.Message = "Girdiğin kullanıcı adı ve şifre kayıtlarımızdakiyle eşleşmedi. Lütfen doğru girdiğinden emin ol ve tekrar dene.";
                return View();
            }
            HttpContext.Session.SetString("Name", loggedInUser.Name);
            HttpContext.Session.SetString("UserName", loggedInUser.UserName);
            HttpContext.Session.SetString("Password", loggedInUser.Password);
            HttpContext.Session.SetString("ProfilePicture", loggedInUser.ProfilePicture);
            HttpContext.Session.SetString("BackgroundImg", loggedInUser.BackgroundImg);
            Response.Cookies.Append("LastLoggedInTime", DateTime.Now.ToString());
            return RedirectToAction("Index");
            
        }
       
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(User user)
        {
            var exist = (from s in _context.Users
                         where s.UserName == user.UserName
                         select s).FirstOrDefault<User>();
            if (exist == null)
            {
                _context.Add(user);
                _context.SaveChanges();
                HttpContext.Session.SetString("Name", user.Name);
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("Password", user.Password);
                return RedirectToAction("Index");
        }
            else
            {
                ViewBag.Message = "Bu kullanıcı adına sahip bir kullanıcı zaten var";
            }
            return View();
}
        [HttpGet]
        public IActionResult Followed()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.UserName = "@" + HttpContext.Session.GetString("UserName");
            ViewBag.Password = HttpContext.Session.GetString("Password");
            ViewBag.ProfilePicture = HttpContext.Session.GetString("ProfilePicture");
            ViewBag.BackgroundImg = HttpContext.Session.GetString("BackgroundImg");
            var users = (from m in _context.Users
                        where m.UserName!= HttpContext.Session.GetString("UserName")
                        select m).ToList();
            

            User ownUser = (from s in _context.Users
                            where s.UserName == HttpContext.Session.GetString("UserName")
                            select s).FirstOrDefault<User>();

            var followed = (from f in _context.Follower
                            where f.UserID == ownUser.UserID
                            select f).ToList();
            var followedList = new List<User>();
            foreach (var item in followed)
            {
                followedList.AddRange(from s in _context.Users
                                      where s.UserID == item.FollowID
                                      select s);
            }
            ViewBag.fw = followedList;
            ViewBag.Users = users.Except(followedList);
            return View();
        }
        [HttpGet]
        public IActionResult FollowerView()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.UserName = "@" + HttpContext.Session.GetString("UserName");
            ViewBag.Password = HttpContext.Session.GetString("Password");
            var users = (from m in _context.Users
                         where m.UserName != HttpContext.Session.GetString("UserName")
                         select m).ToList();
            

            User ownUser = (from s in _context.Users
                            where s.UserName == HttpContext.Session.GetString("UserName")
                            select s).FirstOrDefault<User>();

            var follower = (from f in _context.Follower
                            where f.FollowID == ownUser.UserID
                            select f.UserID).ToList();
            var followerList = new List<User>();
            foreach (var item in follower)
            {
                followerList.AddRange(from s in _context.Users
                                      where s.UserID == item
                                      select s);
            }

            ViewBag.follower = followerList;
           
            var followed = (from f in _context.Follower
                            where f.UserID == ownUser.UserID
                            select f).ToList();
            var followedList = new List<User>();
            foreach (var item in followed)
            {
                followedList.AddRange(from s in _context.Users
                                      where s.UserID == item.FollowID
                                      select s);
            }
            ViewBag.Users = users.Except(followedList);

            ViewBag.followerCount = followerList.Count();
            return View();

        }

        [HttpPost]
        [Obsolete]
        public IActionResult Follower(int fId)
        {
            Follower follower = new Follower();
            User followingUser= (from s in _context.Users
                   where s.UserName == HttpContext.Session.GetString("UserName")
                   select s).FirstOrDefault<User>();
            follower.UserID = followingUser.UserID;
            follower.FollowID = fId;
            _context.Add(follower);

            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Follower ON");
            _context.SaveChanges();
            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Follower OFF");

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Logout()
        {
            return View();

        }
        public IActionResult Profile(User user)
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            User ownUser = (from s in _context.Users
                            where s.UserName == HttpContext.Session.GetString("UserName")
                            select s).FirstOrDefault<User>();

            ViewBag.UserName = "@" + HttpContext.Session.GetString("UserName");
            ViewBag.Password = ownUser.Password;
            ViewBag.ProfilePicture = ownUser.ProfilePicture;
            ViewBag.BackgroundImg = ownUser.BackgroundImg;
            var users = (from m in _context.Users
                        where m.UserName!= HttpContext.Session.GetString("UserName")
                        select m).ToList();

            var twits = (from t in _context.Tweets
                         where t.UserName =="@"+ownUser.UserName
                         orderby t.CreateDate descending
                         select t).ToList();
            ViewBag.UserTwet = twits;

            //get followed
            
            var followed = (from f in _context.Follower
                            where f.UserID == ownUser.UserID
                            select f).ToList();
            var followedList = new List<User>();
            foreach (var item in followed)
            {
                followedList.AddRange(from s in _context.Users
                                      where s.UserID == item.FollowID
                                      select s);
            }

            ViewBag.followedList = followedList;
            ViewBag.Users = users.Except(followedList);

            var followedCount = (from f in _context.Follower
                                 where f.UserID == ownUser.UserID
                                 select f).Count();
            ViewBag.followedCount = followedCount;

            var follower = (from f in _context.Follower
                            where f.FollowID == ownUser.UserID
                            select f.UserID).ToList();
            var followerList = new List<User>();
            foreach (var item in follower)
            {
                followerList.AddRange(from s in _context.Users
                                      where s.UserID == item
                                      select s);
            }

            ViewBag.follower = followerList;
            ViewBag.followerCount = followerList.Count();

            return View();
        }
        [HttpGet]
        public IActionResult LikePage()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            User ownUser = (from s in _context.Users
                            where s.UserName == HttpContext.Session.GetString("UserName")
                            select s).FirstOrDefault<User>();

            ViewBag.UserName = "@" + HttpContext.Session.GetString("UserName");
            ViewBag.Password = ownUser.Password;
            ViewBag.ProfilePicture = ownUser.ProfilePicture;
            ViewBag.BackgroundImg = ownUser.BackgroundImg;
            //BEGENILENLER
            var likesIdList = (from m in _context.Likes
                         where m.UserID == ownUser.UserID
                         select m.TweetID).ToList();
            var tweets = new List<Tweets>();
            foreach(var likes in likesIdList)
            {
                tweets.AddRange(from m in _context.Tweets
                                where m.TweetID == likes
                                select m);
            }
            ViewBag.LikeTweets = tweets;
            //BEGENILENLER
            var users = (from m in _context.Users
                         where m.UserName != HttpContext.Session.GetString("UserName")
                         select m).ToList();
            var twits = (from t in _context.Tweets
                         where t.UserName == "@" + ownUser.UserName
                         orderby t.CreateDate descending
                         select t).ToList();
            ViewBag.UserTwet = twits;

            //get followed

            var followed = (from f in _context.Follower
                            where f.UserID == ownUser.UserID
                            select f).ToList();
            var followedList = new List<User>();
            foreach (var item in followed)
            {
                followedList.AddRange(from s in _context.Users
                                      where s.UserID == item.FollowID
                                      select s);
            }

            ViewBag.followedList = followedList;
            ViewBag.Users = users.Except(followedList);

            var followedCount = (from f in _context.Follower
                                 where f.UserID == ownUser.UserID
                                 select f).Count();
            ViewBag.followedCount = followedCount;

            var follower = (from f in _context.Follower
                            where f.FollowID == ownUser.UserID
                            select f.UserID).ToList();
            var followerList = new List<User>();
            foreach (var item in follower)
            {
                followerList.AddRange(from s in _context.Users
                                      where s.UserID == item
                                      select s);
            }

            ViewBag.follower = followerList;
            ViewBag.followerCount = followerList.Count();

            return View();
        }
        [HttpGet]
        public IActionResult Like(Like like,User user,Tweets tweets)
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Password = HttpContext.Session.GetString("Password");
            user.UserName = ViewBag.UserName;
            user = (from s in _context.Users
                    where s.UserName == user.UserName
                    select s).FirstOrDefault();
            like.UserID = user.UserID;
            tweets= (from s in _context.Tweets
                    where s.TweetID == tweets.TweetID
                    select s).FirstOrDefault();
            tweets.Likes++;
            _context.Add(like);
            _context.SaveChanges();
            ViewBag.UserName = "@" + HttpContext.Session.GetString("UserName");

            return RedirectToAction("Index");
        }
        //RETWEET
        [HttpGet]
        public IActionResult Retweet(Retweets rt, User user, Tweets tweets)
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Password = HttpContext.Session.GetString("Password");
            
            User rtlenen = (from s in _context.Users
                    where ("@"+s.UserName) == tweets.UserName
                    select s).FirstOrDefault();

            User rtleyen = (from s in _context.Users
                           where s.UserName ==  HttpContext.Session.GetString("UserName")
                           select s).FirstOrDefault();

            tweets = (from s in _context.Tweets
                      where s.TweetID == tweets.TweetID
                      select s).FirstOrDefault();
            tweets.Retweets++;

            Tweets newtweet = new Tweets();
            newtweet.ProfilePicture = tweets.ProfilePicture;
            newtweet.Likes = tweets.Likes;
            newtweet.Retweets = tweets.Retweets;
            newtweet.tweet = tweets.tweet;
            newtweet.UserName = tweets.UserName;
            newtweet.UsName = tweets.UsName;
            newtweet.User = rtleyen;

            ViewBag.Rt = rtleyen.UserName+" retweetledi";

            rt.KullanıcıId = rtleyen.UserID;
            _context.Add(rt);
            _context.Add(newtweet);
            _context.SaveChanges();
            ViewBag.UserName = "@" + HttpContext.Session.GetString("UserName");

            return RedirectToAction("Index");
        }
     
        [HttpGet]
        public IActionResult EditProfile()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.UserName = "@" + HttpContext.Session.GetString("UserName");
            ViewBag.ProfilePicture = HttpContext.Session.GetString("ProfilePicture");
            ViewBag.BackgroundImg = HttpContext.Session.GetString("BackgroundImg");

            return View();

        }

        //RESIMSTART
        // GET: FileUpload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(UserViewModel model)
        {
            User own = (from s in _context.Users
                            where s.UserName == HttpContext.Session.GetString("UserName")
                            select s).FirstOrDefault<User>();
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                string uniqueFileName2 = UploadedFile2(model);

                User kullanıcı = new User
                {
                    Name = model.Name,
                    UserName = model.UserName,
                    Description=model.Description,
                    Password=own.Password,
                    ProfilePicture = uniqueFileName,
                    BackgroundImg=uniqueFileName2
                };
                own.ProfilePicture = kullanıcı.ProfilePicture;
                own.BackgroundImg = kullanıcı.BackgroundImg;
                HttpContext.Session.SetString("ProfilePicture", kullanıcı.ProfilePicture);
                HttpContext.Session.SetString("BackgroundImg", kullanıcı.BackgroundImg);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Profile));
            }
            
            return View();

        }
        private string UploadedFile(UserViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string UploadedFile2(UserViewModel model)
        {
            string uniqueFileName = null;

            if (model.BackgroundImg != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images2");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.BackgroundImg.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.BackgroundImg.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        //RESIMEND
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
