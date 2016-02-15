using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using rlnews.DAL.Models;
using rlnews.Models;

namespace rlnews.Controllers
{
    public class MyTeamController : Controller
    {
        public ActionResult Index(int? page)
        {
            ViewData["TeamName"] = "Castleford Tigers";
            ViewData["FeedTitle"] = "Latest News";

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            var dbContext = new rlnews.DAL.RlnewsDb();
            var dbObj = dbContext.NewsItems
                        .OrderByDescending(x => x.PubDateTime)
                        .Where(x => x.Title.Contains("castleford") || x.Description.Contains("castleford"))
                        .ToPagedList(pageNumber, pageSize);

            var newsModel = new FeedViewModel
            {
                NewsFeedList = dbObj,
                SidebarList = SidebarHeadlines()
            };

            return View(newsModel);
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
    }
}