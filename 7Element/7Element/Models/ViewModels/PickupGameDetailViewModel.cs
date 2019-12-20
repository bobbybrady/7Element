using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _7Element.Models.ViewModels
{
    public class PickupGameDetailViewModel
    {
        public List<UserPickupGame> userPickupGames { get; set; }
        public UserPickupGame UserPickupGame { get; set; }
        public PickupGame PickupGame { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public ApplicationUser User {get; set;}
        public int MaxSkaters => PickupGame.MaxSkaters;
        public int MaxGoalies => PickupGame.MaxGoalies;
        public int PickupGameId => PickupGame.PickupGameId;

    }
}
