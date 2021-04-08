using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROJECT_MVC.Models
{
    public class Category
    {
        public int CategoryID { get; set; }         // id kategorii
        public int ?CategoryParentID { get; set; }  // id nadrzednej kategorii (struktura drzewa) // null, jesli nie ma nadrzednej //
        public string Name { get; set; }            // nazwa kategorii

        public virtual ICollection<Poster> Posters { get; set; }

    }
}