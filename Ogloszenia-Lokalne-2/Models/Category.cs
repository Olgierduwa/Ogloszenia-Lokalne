using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ogloszenia_Lokalne_2.Models
{
    public class Category
    {
        [Display(Name = "ID")]
        public int CategoryID { get; set; }         // id kategorii


        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }            // nazwa kategorii


        [Display(Name = "Kategoria Nadrzędna")]
        public int? CategoryParentID { get; set; }  // id nadrzednej kategorii (struktura drzewa) // null, jesli nie ma nadrzednej //


        [ForeignKey("CategoryParentID")]
        public virtual Category CategoryParent { get; set; }


        [Display(Name = "Ogłoszenia danej kategorii")]
        public virtual ICollection<AdCategory> AdsCategories { get; set; }
    }
}