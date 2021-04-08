using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROJECT_MVC.Models
{
    public class Poster
    {
        public int PosterID { get; set; }           // id ogloszenia
        public string Title { get; set; }           // tytul ogloszenia
        public bool IsActive { get; set; }          // dostepnosc ogloszenia w portalu
        public bool IsValuable { get; set; }        // odblokowanie lub blokowanie pola Price
        public decimal Price { get; set; }          // mozliwa cena wyswietlana przy ogloszeniu
        public string Content { get; set; }         // tresc ogloszenia
        public string References { get; set; }      // dodatkowe ważne informacje podane przez autowa pod ogłoszeniem 
        public string Views { get; set; }           // ilość odsłon ogłoszenia
        public string OwnerID { get; set; }         // id wlasciciela ogloszenia


        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}