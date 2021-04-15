using PROJECT_MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ogloszenia_Lokalne.Models
{
    public class UserPoster
    {
        public int UserPosterID { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Poster")]
        public int PosterID { get; set; }
        public Poster Poster { get; set; }
    }
}