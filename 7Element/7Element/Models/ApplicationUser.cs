using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _7Element.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        [Required]
        public string Position { get; set; }
        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }
        [Display(Name = "Veteran")]
        public bool IsVeteran { get; set; }
        
        public virtual ICollection<PlayerStats> PlayerStats { get; set; }
        public virtual ICollection<UserPickupGame> UserPickupGames { get; set; }
        public virtual ICollection<UserPredsGame> UserPredsGames { get; set; }

    }
}
