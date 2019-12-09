using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _7Element.Models
{
    public class PickupGame
    {
        [Key]
        public int PickupGameId { get; set; }
        [Required]
        public int MaxSkaters { get; set; }
        [Required]
        public int MaxGoalies { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
