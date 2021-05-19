using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ogloszenia_Lokalne_2.Models
{
    public class AdCategory
    {
        public int AdCategoryID { get; set; }


        [ForeignKey("AdID")]
        public virtual Ad Ad { get; set; }
        public int AdID { get; set; }


        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
        public int CategoryID { get; set; }
    }
}