using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _7Element.Models
{
    public class PlayerStats
    {
        [Key]
        public int PlayerStatsId { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public string PickupGameId { get; set; }

        [Required]
        public PickupGame PickupGame { get; set; }
        public int Shots { get; set; }
        public int PIM { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        [NotMapped]
        public int Points { get { return Goals + Assists; } }
        public double TOI { get; set; }
        public int ShotsFaced { get; set; }
        public int GoalsAllowed { get; set; }
        [NotMapped]
        public int SavePercentage { get { return ShotsFaced / (ShotsFaced - GoalsAllowed); } }

    }
}
