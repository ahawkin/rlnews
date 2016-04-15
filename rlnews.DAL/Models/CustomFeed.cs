using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rlnews.DAL.Models
{
    public class CustomFeed
    {
        [Key]
        public int CustomFeedId { get; set; }
        
        public int UserId { get; set; }

        public string FeedName { get; set; }

        public string Source1 { get; set; }

        public string Source2 { get; set; }

        public string Source3 { get; set; }

        public string Source4 { get; set; }

        public string Source5 { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
