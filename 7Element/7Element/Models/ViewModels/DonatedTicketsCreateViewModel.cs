using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _7Element.Models.ViewModels
{
    public class DonatedTicketsCreateViewModel
    {
        public DonatedTickets DonatedTickets { get; set; }
        public virtual List<Ticket> Tickets { get; set; } = new List<Ticket>();
        [Display(Name = "Number of Tickets")]
        public string NumberOfTickets { get; set; }

        public string Section { get; set; }
        public string Row { get; set; }
        [Display(Name = "Seat")]
        public List<string> Seats { get; set; }

        public void TicketManager()
        {
            for (int i = 0; i < Int32.Parse(NumberOfTickets); i++)
            {
                Tickets.Add(new Ticket() { 
                    DonatedTicketsId = DonatedTickets.DonatedTicketsId,
                    DonatedTickets = DonatedTickets,
                    Section = Section,
                    Row = Row,
                    Seat = Seats[i]
                });
            }

        }
    }
}
