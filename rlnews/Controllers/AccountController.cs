using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using rlnews.DAL.Models;

namespace rlnews.Controllers
{
    public class AccountController : Controller
    {
        //Account
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return View();
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


        // Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                using (var dbContext = new rlnews.DAL.RlnewsDb())
                {
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = "Thank you " + user.Username + ", your account has been successfully registered.";
            }
            
            return View();
        }

        //Login 
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            using (var dbContext = new rlnews.DAL.RlnewsDb())
            {
                var loginUser = dbContext.Users.FirstOrDefault(
                                    x => x.Email == user.Email 
                                    && x.Password == user.Password
                                );

                if (loginUser != null)
                {
                    Session["UserId"] = loginUser.UserId.ToString();
                    Session["Username"] = loginUser.Username.ToString();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Sorry, Email or Password was incorrect.");
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
    }
}