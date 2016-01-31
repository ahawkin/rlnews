using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rlnews.importer
{
    public class NewsItem
    {
        public string Title { get; set; }

        public string SourceName { get; set; }

        public string Description { get; set; }

        public string SourceUrl { get; set; }

        public string ImageUrl { get; set; }

        public DateTime PubDateTime { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public int Comments { get; set; }

        public int Favourites { get; set; }

        public int Views { get; set; }
    }
}
