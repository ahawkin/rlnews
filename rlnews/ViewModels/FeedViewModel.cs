using System.Collections.Generic;
using PagedList;
using rlnews.DAL.Models;

namespace rlnews.ViewModels
{
    public class FeedViewModel
    {
        public IPagedList<NewsItem> NewsFeedList { get; set; }
        public List<NewsItem> SidebarList { get; set; }
        public List<RelatedNews> RelatedNewsList { get; set; }
    }
}