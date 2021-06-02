using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ogloszenia_Lokalne_2.Models
{
    public class Ad
    {
        public Ad()
        {
            Image = "~/Images/Empty.png";
            IsValuable = true;
            IsActive = true;
        }


        [Display(Name = "ID ogłoszenia")]
        public int AdID { get; set; }           // id ogloszenia


        [Required]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }           // tytul ogloszenia


        [Display(Name = "Dodaj cenę")]
        public bool IsValuable { get; set; }        // odblokowanie lub blokowanie pola Price


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [DataType(DataType.Currency)]
        [Display(Name = "Kwota")]
        [Range(0, 99999999.99)]
        public double Price { get; set; }          // mozliwa cena wyswietlana przy ogloszeniu


        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Treść")]
        public string Content { get; set; }         // tresc ogloszenia


        [Display(Name = "Dopiski")]
        public string References { get; set; }      // dodatkowe ważne informacje podane przez autowa pod ogłoszeniem 


        [Display(Name = "Wyświetlaj ogłoszenie")]
        public bool IsActive { get; set; }          // dostepnosc ogloszenia w portalu


        [Display(Name = "Data publikacji")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AddingDate { get; set; }    // data dodania ogloszenia


        [Display(Name = "Liczba odwiedzin")]
        public int Views { get; set; }           // ilość odsłon ogłoszenia


        [Display(Name = "Autor")]
        public virtual ApplicationUser Owner { get; set; }
        public string OwnerID { get; set; }         // id wlasciciela ogloszenia


        [Display(Name = "Zdjecie")]
        public string Image { get; set; }


        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }


        [Display(Name = "Kategorie ogłoszenia")]
        public virtual ICollection<AdCategory> AdsCategories { get; set; }
    }
}