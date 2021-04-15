using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PROJECT_MVC.Models
{
    public class Category
    {

        [Display(Name = "ID")] [Required]
        public int CategoryID { get; set; }         // id kategorii

        [Display(Name = "Nazwa")] [Required]
        public string Name { get; set; }            // nazwa kategorii



        [Display(Name = "Kategoria Nadrzędna")]
        public int? CategoryParentID { get; set; }  // id nadrzednej kategorii (struktura drzewa) // null, jesli nie ma nadrzednej //

        [ForeignKey("CategoryParentID")]
        public virtual Category CategoryParent { get; set; }

        [Display(Name = "Ogłoszenia danej kategorii")]
        public virtual ICollection<PosterCategory> PostersCategories { get; set; }

    }
}