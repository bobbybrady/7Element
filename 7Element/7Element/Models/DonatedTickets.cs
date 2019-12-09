using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _7Element.Models
{
    public class DonatedTickets
    {
        [Key]
        public int DonatedTicketsId { get; set; }
        [Key]
        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public string PredsGameId { get; set; }

        [Required]
        public PredsGame PredsGame { get; set; }
    }
}
