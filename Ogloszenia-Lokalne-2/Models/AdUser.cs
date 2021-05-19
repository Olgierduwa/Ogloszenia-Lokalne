using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ogloszenia_Lokalne_2.Models
{
    public class AdUser
    {
        public int AdUserID { get; set; }


        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }
        public string UserID { get; set; }


        [ForeignKey("AdID")]
        public Ad Ad { get; set; }
        public int AdID { get; set; }
    }
}