using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    }

}