using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using rlnews.DAL.Models;
using rlnews.Models;
using PagedList;

namespace rlnews.Controllers
{
    public class NewsController : Controller
    {
        //Index Page - News
        public ActionResult Index()
        {
            return RedirectToAction("all");
        }

        //Get All News
        public ActionResult All(int? page)
        {
            ViewData["FeedTitle"] = "All News";

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            var dbContext = new rlnews.DAL.RlnewsDb();
            var dbObj = dbContext.NewsItems.OrderByDescending(x => x.PubDateTime).ToPagedList(pageNumber, pageSize);

            var newsModel = new NewsViewModel
            {
                NewsFeedList = dbObj,
                SidebarList = SidebarHeadlines()
            };

            return View("~/Views/News/Index.cshtml", newsModel);
        }

        //Get Most Popular News
        public ActionResult Popular(int? page)
        {

            ViewData["FeedTitle"] = "Popular News";

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            var dbContext = new rlnews.DAL.RlnewsDb();
            var dbObj = dbContext.NewsItems.OrderByDescending(x => x.Views).ToPagedList(pageNumber, pageSize);

            var newsModel = new NewsViewModel
            {
                NewsFeedList = dbObj,
                SidebarList = SidebarHeadlines()
            };

            return View("~/Views/News/Index.cshtml", newsModel);
        }

        //Get Most Popular News
        public ActionResult Trending(int? page)
        {

            ViewData["FeedTitle"] = "Trending News";

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            DateTime nowMinus48 = DateTime.Now;
            DateTime now = DateTime.Now;
            nowMinus48 = nowMinus48.AddHours(-48);

            var dbContext = new rlnews.DAL.RlnewsDb();
            var dbObj = dbContext.NewsItems.OrderByDescending(x => x.Likes).Where(x => x.PubDateTime > nowMinus48 && x.PubDateTime <= now).ToPagedList(pageNumber, pageSize);

            var newsModel = new NewsViewModel
            {
                NewsFeedList = dbObj,
                SidebarList = SidebarHeadlines()
            };

            return View("~/Views/News/Index.cshtml", newsModel);
        }

        //Get Top News for the sidebar
        public List<NewsItem> SidebarHeadlines()
        {
            DateTime nowMinus48 = DateTime.Now;
            DateTime now = DateTime.Now;
            nowMinus48 = nowMinus48.AddHours(-48);

            var dbContext = new rlnews.DAL.RlnewsDb();

            var topHeadlines = dbContext.NewsItems.OrderByDescending(x => x.Views).Where(x => x.PubDateTime > nowMinus48 && x.PubDateTime <= now).ToList();
            
            return topHeadlines;
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

        public void AddViewToNewsItem(string newsid)
        {
            //Update dislikes field for news items using passed news id 
            var dbContext = new rlnews.DAL.RlnewsDb();

            var dbObj = dbContext.NewsItems.Find(Int32.Parse(newsid));

            dbObj.Views = dbObj.Views + 1;

            dbContext.SaveChanges();
        }
    }

}