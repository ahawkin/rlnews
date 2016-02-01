using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using rlnews.DAL.Models;
using rlnews.Models;

namespace rlnews.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            var dbContext = new rlnews.DAL.RlnewsDb();

            return View(dbContext.NewsItems.ToList());
        }

        [HttpPost]
        public ActionResult LikeNewsItem(string newsid)
        {
            //Update likes field for news items using passed news id 
            var dbContext = new rlnews.DAL.RlnewsDb();

            var dbObj = dbContext.NewsItems.Find(Int32.Parse(newsid));

            dbObj.Likes = dbObj.Likes + 1;

            dbContext.SaveChanges();

            //Return the new score for this news item
            return Json(new { success = true, message = dbObj.Likes - dbObj.Dislikes }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DislikeNewsItem(string newsid)
        {
            //Update dislikes field for news items using passed news id 
            var dbContext = new rlnews.DAL.RlnewsDb();

            var dbObj = dbContext.NewsItems.Find(Int32.Parse(newsid));

            dbObj.Dislikes = dbObj.Dislikes + 1;

            dbContext.SaveChanges();

            //Return the new score for this news item
            return Json(new { success = true, message = dbObj.Likes - dbObj.Dislikes }, JsonRequestBehavior.AllowGet);
        }
    }

}