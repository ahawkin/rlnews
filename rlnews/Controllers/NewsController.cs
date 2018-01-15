using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using rlnews.DAL.Models;
using rlnews.ViewModels;
using PagedList;

namespace rlnews.Controllers
{
    public class NewsController : Controller
    {
        //Index Page - Default to all news
        public ActionResult Index()
        {
            return RedirectToAction("latest");
        }

        //Get All News
        public ActionResult Latest(int? page)
        {
            ViewData["FeedTitle"] = "Latest News";

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            var dbContext = new rlnews.DAL.RlnewsDb();
            var dbObj = dbContext.NewsItems.OrderByDescending(x => x.PubDateTime).ToPagedList(pageNumber, pageSize);

            var newsModel = new FeedViewModel
            {
                NewsFeedList = dbObj,
                SidebarList = SidebarHeadlines(),
                RelatedNewsList = RealtedNewsList()
            };

            return View("~/Views/News/Index.cshtml", newsModel);
        }

        //Get Most Popular News
        public ActionResult Popular(int? page)
        {
            ViewData["FeedTitle"] = "Popular News";

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            DateTime dateTime = DateTime.Now;
            DateTime now = DateTime.Now;
            dateTime = dateTime.AddHours(-24);

            var dbContext = new rlnews.DAL.RlnewsDb();
            var dbObj = dbContext.NewsItems.OrderByDescending(x => x.Views)
                                 .Where(x => x.PubDateTime > dateTime && x.PubDateTime <= now)
                                 .Where(x => x.Views > 0)
                                 .ToPagedList(pageNumber, pageSize);

            var newsModel = new FeedViewModel
            {
                NewsFeedList = dbObj,
                SidebarList = SidebarHeadlines(),
                RelatedNewsList = RealtedNewsList()
            };

            return View("~/Views/News/Index.cshtml", newsModel);
        }

        //Get Trending News
        public ActionResult Trending(int? page)
        {
            ViewData["FeedTitle"] = "Trending News";

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            DateTime dateTime = DateTime.Now;
            DateTime now = DateTime.Now;
            dateTime = dateTime.AddHours(-24);

            var dbContext = new rlnews.DAL.RlnewsDb();
            var dbObj = dbContext.NewsItems.OrderByDescending(x => x.LikeTotal)
                        .Where(x => x.PubDateTime > dateTime && x.PubDateTime <= now)
                        .Where(x => x.LikeTotal > 0)
                        .ToPagedList(pageNumber, pageSize);

            var newsModel = new FeedViewModel
            {
                NewsFeedList = dbObj,
                SidebarList = SidebarHeadlines(),
                RelatedNewsList = RealtedNewsList()
            };

            return View("~/Views/News/Index.cshtml", newsModel);
        }

        //Get Most Discussed News
        public ActionResult Discussed(int? page)
        {
            ViewData["FeedTitle"] = "Most Discussed News";

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            DateTime dateTime = DateTime.Now;
            DateTime now = DateTime.Now;
            dateTime = dateTime.AddHours(-24);

            var dbContext = new rlnews.DAL.RlnewsDb();
            var dbObj = dbContext.NewsItems.OrderByDescending(x => x.CommentTotal)
                        .Where(x => x.PubDateTime > dateTime && x.PubDateTime <= now)
                        .Where(x => x.CommentTotal > 0)
                        .ToPagedList(pageNumber, pageSize);

            var newsModel = new FeedViewModel
            {
                NewsFeedList = dbObj,
                SidebarList = SidebarHeadlines(),
                RelatedNewsList = RealtedNewsList()
            };

            return View("~/Views/News/Index.cshtml", newsModel);
        }

        public List<NewsItem> SidebarHeadlines()
        {
            DateTime nowMinus24 = DateTime.Now;
            DateTime now = DateTime.Now;
            nowMinus24 = nowMinus24.AddHours(-48);

            var dbContext = new rlnews.DAL.RlnewsDb();

            var topHeadlines = dbContext.NewsItems.OrderByDescending(x => x.Views).Where(x => x.PubDateTime > nowMinus24 && x.PubDateTime <= now).ToList();
            
            return topHeadlines;
        }

        public List<RelatedNews> RealtedNewsList()
        {
            var dbContext = new rlnews.DAL.RlnewsDb();

            var relatedNews = dbContext.RelatedNews.ToList();

            return relatedNews;
        }

        [HttpPost]
        public ActionResult LikeNewsItem(string newsid)
        {
            if (Session["UserId"] != null)
            {
                //Initalize dbContext
                var dbContext = new rlnews.DAL.RlnewsDb();

                //Create newsitem object and add to like total
                var dbNews = dbContext.NewsItems.Find(Int32.Parse(newsid));

                dbNews.LikeTotal = dbNews.LikeTotal + 1;

                //Create a dislike activity object and insert it into the database
                var dbActvity = new rlnews.DAL.Models.Activity
                {
                    NewsId = Int32.Parse(newsid),
                    UserId = Int32.Parse(Session["UserId"].ToString()),
                    ActivityType = "Like",
                    ActivityDate = DateTime.Now,
                    ActivityContent = Session["Username"] + " liked a news article"
                };

                dbContext.Activity.Add(dbActvity);

                dbContext.SaveChanges();

                //Return the new score for this news item
                return Json(new { success = true, message = dbNews.LikeTotal - dbNews.DislikeTotal }, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        [HttpPost]
        public ActionResult DislikeNewsItem(string newsid, string userid)
        {
            if (Session["UserId"] != null)
            {
                //Initalize dbContext
                var dbContext = new rlnews.DAL.RlnewsDb();

                //Create newsitem object and add to dislike total
                var dbNews = dbContext.NewsItems.Find(Int32.Parse(newsid));

                dbNews.DislikeTotal = dbNews.DislikeTotal + 1;

                //Create a dislike activity object and insert it into the database
                var dbActvity = new rlnews.DAL.Models.Activity
                {
                    NewsId = Int32.Parse(newsid),
                    UserId = Int32.Parse(Session["UserId"].ToString()),
                    ActivityType = "Dislike",
                    ActivityDate = DateTime.Now,
                    ActivityContent = Session["Username"] + " disliked a news article"
                };

                dbContext.Activity.Add(dbActvity);

                dbContext.SaveChanges();

                //Return the new score for this news item
                return Json(new { success = true, message = dbNews.LikeTotal - dbNews.DislikeTotal }, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        public void AddViewToNewsItem(string newsid)
        {
            var dbContext = new rlnews.DAL.RlnewsDb();

            var dbObj = dbContext.NewsItems.Find(Int32.Parse(newsid));

            dbObj.Views = dbObj.Views + 1;

            dbContext.SaveChanges();
        }
    }

}