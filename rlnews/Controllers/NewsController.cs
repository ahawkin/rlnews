﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using rlnews.DAL.Models;
using rlnews.Models;

namespace rlnews.Controllers
{
    public class NewsController : Controller
    {
        //Index Page - News
        public ActionResult Index()
        {
            var dbContext = new rlnews.DAL.RlnewsDb();
            var dbObj = dbContext.NewsItems.OrderByDescending(x => x.PubDateTime).ToList();

            var newsModel = new NewsViewModel
            {
                NewsFeedList = dbObj,
                SidebarList = SidebarHeadlines()
            };

            return View("~/Views/News/Index.cshtml", newsModel);
        }

        //Get All News
        public ActionResult All()
        {
            var dbContext = new rlnews.DAL.RlnewsDb();
            var dbObj = dbContext.NewsItems.OrderByDescending(x => x.PubDateTime).ToList();

            var newsModel = new NewsViewModel
            {
                NewsFeedList = dbObj,
                SidebarList = SidebarHeadlines()
            };

            return View("~/Views/News/Index.cshtml", newsModel);
        }

        //Get Most Popular News
        public ActionResult Popular()
        {
            var dbContext = new rlnews.DAL.RlnewsDb();
            var dbObj = dbContext.NewsItems.OrderByDescending(x => x.Views).ToList();

            var newsModel = new NewsViewModel
            {
                NewsFeedList = dbObj,
                SidebarList = SidebarHeadlines()
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