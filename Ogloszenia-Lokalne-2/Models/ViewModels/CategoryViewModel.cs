using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ogloszenia_Lokalne_2.Models.ViewModels
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public List<Ad> Ads { get; set; }
        public Ad ExampleAd { get; set; }
    }
}