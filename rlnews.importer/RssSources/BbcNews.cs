using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using rlnews.DAL.Models;

namespace rlnews.importer.RssSources
{

    public class BbcNews
    {
        private int _importCount; 
        private string _importMessage;
        private IEnumerable<NewsItem> _feeds;
        private List<DAL.Models.NewsItem> _lastestNewsItem;
        private DateTime _lastestNewsItemDate;
        private readonly ValidateItem _validate = new ValidateItem();

        /// <summary>
        /// Starts the rss data import for BBC News
        /// </summary>
        public void StartImport()
        {
            //Start fetch
            FetchRssFeed();
            GetLatestNewsItemDate();
            if (_feeds != null)
            {
                InsertRssData();
            }
        }

        /// <summary>
        /// Returns the number of news items imported into the database
        /// </summary>
        /// <returns></returns>
        public int ReturnImportNumber()
        {
            return _importCount;
        }

        /// <summary>
        /// Returns import status message
        /// </summary>
        /// <returns></returns>
        public string ReturnImportMessage()
        {
            return _importMessage;
        }

        /// <summary>
        /// Fetches rss feeds data from BBC News
        /// </summary>
        private void FetchRssFeed()
        {
            try
            {
                string rssUrl = "http://feeds.bbci.co.uk/sport/0/rugby-league/rss.xml?edition=int";

                XDocument feedXml = XDocument.Load(rssUrl);
                XNamespace media = XNamespace.Get("http://search.yahoo.com/mrss/");

                //Fetch RSS feed
                _feeds = from feed in feedXml.Descendants("item")
                    let title = feed.Element("title")
                    where title != null
                    let description = feed.Element("description")
                    where description != null
                    let sourceUrl = feed.Element("link")
                    where sourceUrl != null
                    let pubDateTime = feed.Element("pubDate")
                    where pubDateTime != null
                    select new NewsItem()
                            {
                            Title = _validate.LimitLength(title.Value),
                            Description = _validate.LimitLength(description.Value),
                            SourceUrl = sourceUrl.Value,
                                SourceName = "BBC Sport",
                                ImageUrl = feed.Elements(media + "thumbnail")
                                    .Where(i => i.Attribute("width").Value == "144" && i.Attribute("height").Value == "81")
                                    .Select(i => i.Attribute("url").Value).SingleOrDefault(),
                                PubDateTime = DateTime.Parse(pubDateTime.Value)
                            };

                //Reserve fetched feed so the newest news item is last the be inserted
                _feeds = _feeds.OrderBy(x => x.PubDateTime);
            }
            catch (Exception ex)
            {
                _importMessage = "Unable to fetch BBC News feeds: " + ex;
            }
        }

        private void GetLatestNewsItemDate()
        {
            var dbContext = new rlnews.DAL.RlnewsDb();
            _lastestNewsItem = dbContext.NewsItems
                                   .OrderByDescending(x => x.NewsId)
                                   .Where(x => x.SourceName == "BBC Sport")
                                   .Take(1)
                                   .ToList();

            foreach (var item in _lastestNewsItem)
            {
                _lastestNewsItemDate = item.PubDateTime;
            }
        }

        private bool IsPostNew(DateTime rssPublish)
        {
            if (_lastestNewsItemDate < rssPublish)
            {
                return true;
            }

            return false;
        }    

        /// <summary>
        /// Inserts BBC News rss feed data into the database
        /// </summary>
        private void InsertRssData()
        {
            Distance distance = new Distance();

            try
            {
                // create database context
                var dbContext = new rlnews.DAL.RlnewsDb();


                foreach (var newsItem in _feeds)
                {
                    if (IsPostNew(newsItem.PubDateTime))
                    {
                        string clusterType = null;
                        int parentId = 0;

                        parentId = distance.CheckRelated(newsItem.Title);

                        if (parentId > 0)
                        {
                            clusterType = "Child";
                        }
                        else
                        {
                            clusterType = "Parent";
                        }

                        //Increment import counter
                        _importCount++;

                        //create object to add to database
                        var dbObj = new rlnews.DAL.Models.NewsItem
                        {
                            Title = _validate.RemoveHtml(newsItem.Title),
                            SourceName = newsItem.SourceName,
                            Description = _validate.RemoveHtml(newsItem.Description),
                            SourceUrl = newsItem.SourceUrl,
                            ImageUrl = newsItem.ImageUrl,
                            PubDateTime = newsItem.PubDateTime,
                            Likes = 0,
                            Dislikes = 0,
                            Comments = 0,
                            Favourites = 0,
                            Views = 0,
                            ClusterType = clusterType,
                            ParentNewsId = parentId
                        };

                        dbContext.NewsItems.Add(dbObj);

                        //Save database changes
                        dbContext.SaveChanges();
                    }
                }

                if (_importCount == 0)
                {
                    _importMessage = "No new posts found from BBC News.";
                }
                else
                {
                    _importMessage = "Sucessfully imported '" + _importCount + "' news items from BBC News.";
                }
            }
            catch (Exception ex)
            {
                _importMessage = "BBC News import failed: " + ex;
            }
        }
    }
}
