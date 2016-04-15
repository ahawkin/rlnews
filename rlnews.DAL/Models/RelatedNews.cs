using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rlnews.DAL.Models
{
    public class RelatedNews
    {
        [Key]
        public int RelatedNewsId { get; set; }

        public int ParentNewsId { get; set; }

        public int ChildNewsId { get; set; }
    }
}
