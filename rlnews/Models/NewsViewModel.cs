using System.Collections.Generic;
using PagedList;
using rlnews.DAL.Models;

namespace rlnews.Models
{
    public class NewsViewModel
    {
        public IPagedList<NewsItem> NewsFeedList { get; set; }
        public List<NewsItem> SidebarList { get; set; }
    }
}