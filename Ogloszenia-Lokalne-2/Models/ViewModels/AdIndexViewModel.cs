using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ogloszenia_Lokalne_2.Models.ViewModels
{
    public class AdIndexViewModel
    {
        public Ad ExpampleAd { get; set; }

        public int SiteID { get; set; }
        public int SiteCount { get; set; }
        public int AdCount { get; set; }
        public bool Filtr { get; set; }

        [Display(Name = "Wyszykaj:")]
        public string Search { get; set; }

        [Display(Name = "Cena od:")]
        public decimal FromPrice { get; set; }

        [Display(Name = "Cena do:")]
        public decimal ToPrice { get; set; }

        [Display(Name = "Kategorie:")]
        public virtual List<Category> Categories { get; set; }
        public List<int> SelectedCategories { get; set; }

    }
}