using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rlnews.DAL.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        public int NewsId { get; set; }

        public int UserId { get; set; }

        public string ActivityType { get; set; }

        public string ActivityContent { get; set; }

        public DateTime ActivityDate { get; set; }
    }
}
