using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using rlnews.DAL.Models;

namespace rlnews.DAL
{
    public class RlnewsDb : DbContext
    {
        public RlnewsDb() : base("rlnews")
        {
            Database.SetInitializer<RlnewsDb>(null);
        }    
        
        public DbSet<NewsItem> NewsItems { get; set; }

    }
}
