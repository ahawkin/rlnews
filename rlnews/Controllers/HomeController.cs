using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using rlnews.Models;

namespace rlnews.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new RssModel();
            string strFeed = "http://feeds.bbci.co.uk/sport/0/rugby-league/rss.xml?edition=uk";
            using (XmlReader reader = XmlReader.Create(strFeed))
            {
                SyndicationFeed rssData = SyndicationFeed.Load(reader);
 
                model.RssFeed = rssData;
            }

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}