using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;

namespace rlnews.Models
{
    public class RssModel
    {
        public SyndicationFeed RssFeed { get; set; }
    }
}