using Ogloszenia_Lokalne.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PROJECT_MVC.Models
{
    public class Poster
    {

        [Display(Name = "ID ogłoszenia")]
        public int PosterID { get; set; }           // id ogloszenia

        [Display(Name = "Data publikacji")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AddingDate { get; set; }    // data dodania ogloszenia

        [Display(Name = "Tytuł")] [Required]
        public string Title { get; set; }           // tytul ogloszenia

        [DataType(DataType.Currency)]
        [Display(Name = "Kwota")]
        [Range(0,10000000)]
        public decimal Price { get; set; }          // mozliwa cena wyswietlana przy ogloszeniu

        [Display(Name = "Wejścia")]
        public int Views { get; set; }           // ilość odsłon ogłoszenia

        [Display(Name = "Pokazuj")]
        public bool IsActive { get; set; }          // dostepnosc ogloszenia w portalu

        [Display(Name = "Ogłoszenie wycenialne")]
        public bool IsValuable { get; set; }        // odblokowanie lub blokowanie pola Price

        [DataType(DataType.MultilineText)]
        [Display(Name = "Treść")] [Required]
        public string Content { get; set; }         // tresc ogloszenia

        [Display(Name = "Dopiski")]
        public string References { get; set; }      // dodatkowe ważne informacje podane przez autowa pod ogłoszeniem 



        [Display(Name = "ID Autora")]
        public string OwnerID { get; set; }         // id wlasciciela ogloszenia

        [Display(Name = "Autor")]
        public virtual ApplicationUser Owner { get; set; }

        [Display(Name = "Kategorie danego ogłoszenia")]
        public virtual ICollection<PosterCategory> PostersCategories { get; set; }
    }
}