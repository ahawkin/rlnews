using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Xml.Linq;
using rlnews.Models;

namespace rlnews.Controllers
{
    public class NewsController : Controller
    {

        public static IEnumerable<Rss> GetRssFeed()
        {
            string rssUrl = "http://feeds.bbci.co.uk/sport/0/rugby-league/rss.xml?edition=int";

            XDocument feedXml = XDocument.Load(rssUrl);
            XNamespace media = XNamespace.Get("http://search.yahoo.com/mrss/");
            var feeds = from feed in feedXml.Descendants("item")
                        select new Rss
                        {
                            Title = feed.Element("title").Value,
                            Link = feed.Element("link").Value,
                            Description = Regex.Match(feed.Element("description").Value, @"^.{1,180}\b(?<!\s)").Value,
                            Image = feed.Elements(media + "thumbnail")
                                    .Where(i => i.Attribute("width").Value == "144" && i.Attribute("height").Value == "81")
                                    .Select(i => i.Attribute("url").Value).SingleOrDefault(),
                            PubDate = feed.Element("pubDate").Value
                        };

            return feeds;

        }

        public ActionResult Index()
        {
            return View(GetRssFeed());
        }
    }
}