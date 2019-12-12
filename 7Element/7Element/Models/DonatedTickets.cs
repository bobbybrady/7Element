using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _7Element.Models
{
    public class DonatedTickets
    {
        [Key]
        public int DonatedTicketsId { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        [Display(Name = "Game")]
        public int PredsGameId { get; set; }

        [Required]
        public PredsGame PredsGame { get; set; }
        
        public bool TransactionComplete { get; set; }
        
    }
}
