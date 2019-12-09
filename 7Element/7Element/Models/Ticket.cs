using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _7Element.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        [Required]
        public int DonatedTicketsId { get; set; }

        [Required]
        public DonatedTickets DonatedTickets { get; set; }
        [Required]
        public string Section { get; set; }
        [Required]
        public string Row { get; set; }
        [Required]
        public string Seat { get; set; }
    }
}
