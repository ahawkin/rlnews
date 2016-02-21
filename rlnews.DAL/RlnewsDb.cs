using System;
using System.Collections.Generic;
using System.Data;
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
            //If database doesn't exist - create it
            //Database.SetInitializer<RlnewsDb>(CreateDatabaseIfNotExists<RlnewsDb>);

            //Can be used to drop and create the database if the model changes
            Database.SetInitializer<RlnewsDb>(new DropCreateDatabaseIfModelChanges<RlnewsDb>());

            //Can be used to drop and create the database every time the application is run
            //Database.SetInitializer<RlnewsDb>(new DropCreateDatabaseAlways<RlnewsDb>());

            //Manage database manually
            //Database.SetInitializer<RlnewsDb>(null);
        }

        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
