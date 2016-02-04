using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using rlnews.DAL.Models;

namespace rlnews.Models
{
    public class NewsViewModel
    {
        public List<NewsItem> NewsFeedList { get; set; }
        public List<NewsItem> SidebarList { get; set; }
    }
}