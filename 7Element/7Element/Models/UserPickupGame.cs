using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _7Element.Models
{
    public class UserPickupGame
    {
        [Key]
        public int UserPickupGameId { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public int PickupGameId { get; set; }

        [Required]
        public PickupGame PickupGame { get; set; }
        [Required]
        public bool IsStandby { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
    }
}
