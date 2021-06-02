using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ogloszenia_Lokalne_2.Models.ViewModels
{
    public class RoleViewModel
    {
        [Display(Name = "Użytkownicy w roli")]
        public List<ApplicationUser> Users { get; set; }
        public ApplicationUser User { get; set; }

        [Display(Name = "Role użytkownika")]
        public virtual List<IdentityRole> Roles { get; set; }
        public IdentityRole Role { get; set; }

        [Display(Name = "Rola")]
        public List<string> SelectedRoles { get; set; }
    }
}