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
        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public int PredsGameId { get; set; }

        [Required]
        public PredsGame PredsGame { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        [Required]
        public string EmailAddress {get; set;}
        [Required]
        public string EmailTitle { get; set; }
        [Required]
        public string EmailBody { get; set; }
        public bool TransactionComplete { get; set; }
    }
}
