using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace rlnews.DAL
{
    public class DbInit : DropCreateDatabaseAlways<RlnewsDb>
    {
        protected override void Seed(RlnewsDb context)
        {
        }
    }
}
