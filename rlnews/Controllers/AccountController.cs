using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using rlnews.DAL.Models;
using rlnews.ViewModels;

namespace rlnews.Controllers
{
    public class AccountController : Controller
    {
        //Account
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {

                var userId = Int32.Parse(Session["UserId"].ToString());

                using (var dbContext = new rlnews.DAL.RlnewsDb())
                {
                    var user = dbContext.Users.FirstOrDefault(x => x.UserId == userId);

                    var userActivity = dbContext.Activity.Where(x => x.UserId == userId).ToList();

                    if (user != null)
                    {
                        var userDetails = new UserViewModel()
                        {
                            Email = user.Email,
                            Username = user.Username,
                            TeamName = user.TeamName,
                            ActivityList = userActivity
                        };

                        return View(userDetails);
                    }
                }
            }

            return RedirectToAction("Login");
        }

        //Account
        public ActionResult Settings()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Settings(UserViewModel settingsForm)
        {
            var teamList = new UserViewModel()
            {
                TeamList = TeamList()
            };

            if (ModelState.IsValid)
            {
                using (var dbContext = new rlnews.DAL.RlnewsDb())
                {
                    //Check email address and username are unique
                    var takenEmail = dbContext.Users.FirstOrDefault(x => x.Email == settingsForm.Email);
                    var takenUsername = dbContext.Users.FirstOrDefault(x => x.Username == settingsForm.Username);

                    if (takenEmail != null)
                    {
                        ModelState.AddModelError("", "Sorry - Email address already registered to an account.");
                    }

                    if (takenUsername != null)
                    {
                        ModelState.AddModelError("", "Sorry - Username has already been taken.");
                    }

                    //If validation is passed register user
                    if (ModelState.IsValid)
                    {
                        var dbUser = new rlnews.DAL.Models.User();

                        var salt = GenerateSalt(10);
                        var hashedPass = GenerateHash(settingsForm.Password, salt);

                        dbUser.Username = settingsForm.Username;
                        dbUser.Email = settingsForm.Email;
                        dbUser.Password = hashedPass;
                        dbUser.PassSalt = salt;
                        dbUser.TeamName = settingsForm.TeamName;

                        dbContext.Users.Add(dbUser);

                        dbContext.SaveChanges();

                        ModelState.Clear();

                        ViewBag.Message = "Thank you " + settingsForm.Username + ", your account has been successfully registered.";
                    }
                }
            }

            return View(teamList);
        }

        // Register
        public ActionResult Register()
        {
            var teamList = new UserViewModel()
            {
                TeamList = TeamList()
            };  

            return View(teamList);
        }

        [HttpPost]
        public ActionResult Register(UserViewModel registerForm)
        {

            var teamList = new UserViewModel()
            {
                TeamList = TeamList()
            };

            if (ModelState.IsValid)
            {
                using (var dbContext = new rlnews.DAL.RlnewsDb())
                {
                    //Check email address and username are unique
                    var takenEmail = dbContext.Users.FirstOrDefault(x => x.Email == registerForm.Email);
                    var takenUsername = dbContext.Users.FirstOrDefault(x => x.Username == registerForm.Username);

                    if (takenEmail != null)
                    {
                        ModelState.AddModelError("", "Sorry - Email address already registered to an account.");
                    }

                    if (takenUsername != null)
                    {
                        ModelState.AddModelError("", "Sorry - Username has already been taken.");
                    }

                    //If validation is passed register user
                    if (ModelState.IsValid)
                    {
                        var dbUser = new rlnews.DAL.Models.User();

                        var salt = GenerateSalt(10);
                        var hashedPass = GenerateHash(registerForm.Password, salt);

                        dbUser.Username = registerForm.Username;
                        dbUser.Email = registerForm.Email;
                        dbUser.Password = hashedPass;
                        dbUser.PassSalt = salt;
                        dbUser.TeamName = registerForm.TeamName;

                        dbContext.Users.Add(dbUser);

                        dbContext.SaveChanges();

                        ModelState.Clear();

                        ViewBag.Message = "Thank you " + registerForm.Username + ", your account has been successfully registered.";
                    }
                }
            }
            
            return View(teamList);
        }

        //Login 
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel loginForm)
        {
            using (var dbContext = new rlnews.DAL.RlnewsDb())
            {
                var user = dbContext.Users.FirstOrDefault(x => x.Email == loginForm.Email);

                if (user != null)
                {
                    var salt = user.PassSalt;
                    var hashedPass = GenerateHash(loginForm.Password, salt);

                    if (hashedPass != user.Password)
                    {
                        ModelState.AddModelError("", "Sorry, the email/password you entered was incorrect");
                    }
                    else
                    {
                        Session["UserId"] = user.UserId.ToString();
                        Session["Username"] = user.Username;

                        ModelState.Clear();

                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Sorry, the email/password you entered was incorrect");
                }
            }

            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            if (Session["UserId"] != null)
            {
                Session.Abandon();
                return Redirect("~/news/latest");
            }

            return RedirectToAction("Login");
        }

        #region Helper Methods

        /// <summary>
        /// Convert byte array to hex string
        /// Chris Duran (2014) C# MD5 & SHA 256 Hash Demo [Online video], 9 March. Available from: <https://www.youtube.com/watch?v=JyfgHxe7BL4> [Accessed 22 April 2012].
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public string ByteArrayToHexString(byte[] byteArray)
        {
            StringBuilder hex = new StringBuilder(byteArray.Length*2);

            foreach (var b in byteArray)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        /// <summary>
        /// Generate Random Salt
        /// Chris Duran (2014) C# Salting & Hashing Passwords [Online video], 18 March. Available from: <https://www.youtube.com/watch?v=AU-4oLUV5VU> [Accessed 22 April 2012].
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public string GenerateSalt(int size)
        {
            var rand = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rand.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Generate hash from password
        /// Chris Duran (2014) C# Salting & Hashing Passwords [Online video], 18 March. Available from: <https://www.youtube.com/watch?v=AU-4oLUV5VU> [Accessed 22 April 2012].
        /// </summary>
        /// <param name="input"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string GenerateHash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed shaHashString = new SHA256Managed();
            byte[] hash = shaHashString.ComputeHash(bytes);

            return ByteArrayToHexString(hash);
        }

        //Rugby League Team List
        public List<SelectListItem> TeamList()
        {
            List<SelectListItem> teamList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Castleford Tigers",
                    Value = "Castleford Tigers",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Warrington Wolves",
                    Value = "Warrington Wolves",
                },
                new SelectListItem
                {
                    Text = "Wigan Warriors",
                    Value = "Wigan Warriors"
                },
                new SelectListItem
                {
                    Text = "Catalans Dragons",
                    Value = "Catalans Dragons"
                },
                new SelectListItem
                {
                    Text = "Hull FC",
                    Value = "Hull FC"
                },
                new SelectListItem
                {
                    Text = "St Helens",
                    Value = "St Helens"
                },
                new SelectListItem
                {
                    Text = "Widnes Vikings",
                    Value = "Widnes Vikings"
                },
                new SelectListItem
                {
                    Text = "Salford Red Devils",
                    Value = "Salford Devils"
                },
                new SelectListItem
                {
                    Text = "Wakefield Wildcats",
                    Value = "Wakefield Wildcats"
                },
                new SelectListItem
                {
                    Text = "Leeds Rhinos",
                    Value = "Leeds Rhinos"
                },
                new SelectListItem
                {
                    Text = "Hull KR",
                    Value = "Hull KR"
                },
                new SelectListItem
                {
                    Text = "Huddersfield Giants",
                    Value = "Huddersfield Giants"
                }
            };

            return teamList;
        }

        #endregion
    }
}