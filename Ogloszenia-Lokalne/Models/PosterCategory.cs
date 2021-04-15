using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PROJECT_MVC.Models
{
    public class PosterCategory
    {
        public int PosterCategoryID { get; set; }

        public int PosterID { get; set; }
        [ForeignKey("PosterID")]
        public virtual Poster Poster { get; set; }

        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
    }
}