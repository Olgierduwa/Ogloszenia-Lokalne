using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ogloszenia_Lokalne_2.Models.ViewModels
{
    public class AdViewModel
    {
        public Ad Ad { get; set; }
        [Required]
        [Display(Name = "Kategoria")]
        public List<int> SelectedCategories { get; set; }
        public virtual List<Category> Categories { get; set; }
    }
}