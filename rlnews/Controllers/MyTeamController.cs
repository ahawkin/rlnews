using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using rlnews.DAL.Models;
using rlnews.ViewModels;

namespace rlnews.Controllers
{
    public class MyTeamController : Controller
    {
        //Index Page - Default to all news
        public ActionResult Index()
        {
            return RedirectToAction("latest");
        }

        //Get the latest news for My Team
        public ActionResult Latest(int? page)
        {
            var userTeam = GetUserTeam();

            if (userTeam != null)
            {
                ViewData["TeamName"] = userTeam;
                ViewData["FeedTitle"] = "Latest News";

                string[] teamNameSegments = userTeam.Split(' ');

                var teamSegment1 = teamNameSegments[0];
                var teamSegment2 = teamNameSegments[1];

                int pageSize = 15;
                int pageNumber = (page ?? 1);

                var dbContext = new rlnews.DAL.RlnewsDb();
                var dbObj = dbContext.NewsItems
                    .OrderByDescending(x => x.PubDateTime)
                    .Where(x => x.Title.Contains(teamSegment1) ||
                                x.Description.Contains(teamSegment1) ||
                                x.Title.Contains(teamSegment2) ||
                                x.Description.Contains(teamSegment2))
                    .ToPagedList(pageNumber, pageSize);

                var newsModel = new FeedViewModel
                {
                    NewsFeedList = dbObj,
                    SidebarList = SidebarHeadlines()
                };

                return View("~/Views/MyTeam/Index.cshtml", newsModel);
            }
            else
            {
                ViewData["TeamName"] = "Castleford Tigers";
                ViewData["FeedTitle"] = "Castleford Tigers";

                int pageSize = 15;
                int pageNumber = (page ?? 1);

                var dbContext = new rlnews.DAL.RlnewsDb();
                var dbObj = dbContext.NewsItems
                    .OrderByDescending(x => x.PubDateTime)
                    .Where(x => x.Title.Contains("Castleford") ||
                                x.Description.Contains("Castleford") ||
                                x.Title.Contains("Tigers") ||
                                x.Description.Contains("Tigers"))
                    .ToPagedList(pageNumber, pageSize);

                var newsModel = new FeedViewModel
                {
                    NewsFeedList = dbObj,
                    SidebarList = SidebarHeadlines()
                };

                return View("~/Views/MyTeam/Index.cshtml", newsModel);
            }
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

        public string GetUserTeam()
        {
            if (Session["UserId"] != null)
            {
                var userId = Int32.Parse(Session["UserId"].ToString());

                using (var dbContext = new rlnews.DAL.RlnewsDb())
                {
                    var user = dbContext.Users.FirstOrDefault(x => x.UserId == userId);

                    if (user != null && user.TeamName != null)
                    {
                        return user.TeamName;
                    }
                }
            }

            return null;
        }
    }
}