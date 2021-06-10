using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ogloszenia_Lokalne_2.Models
{
    public class Report
    {
        public int ReportID { get; set; }
        [Display(Name = "ID ogłoszenia")]
        public int AdID { get; set; }
        [Display(Name = "ID zgłaszającego")]
        public string UserReportedID { get; set; }
        [ForeignKey("UserReportedID")]
        public virtual ApplicationUser UserReported { get; set; }
        [Required]
        [Display(Name = "Treść zgłoszenia")]
        public string ReportMessage { get; set; }
    }
}