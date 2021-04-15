using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROJECT_MVC.Models.ModelsView
{
    public class PosterCreate
    {
        public Poster Poster { get; set; }
        [Required]
        [Display(Name = "Kategoria")]
        public List<int> CategoriesID { get; set; }
    }
}