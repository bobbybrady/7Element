using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _7Element.Models
{
    public class PredsGame
    {
        [Key]
        public int PredsGameId { get; set; }
        public DateTime DateTime { get; set; }
        public string Opponent { get; set; }
        public bool Open { get; set; }
        public virtual ICollection<DonatedTickets> DonatedTickets { get; set; }
        public virtual ICollection<UserPredsGame> UserPredsGames { get; set; }
    }
}
