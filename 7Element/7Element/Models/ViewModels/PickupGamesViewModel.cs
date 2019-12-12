using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _7Element.Models.ViewModels
{
    public class PickupGamesViewModel
    {
        public ApplicationUser User { get; set; }
        public List<PickupGame> PickupGames { get; set; } = new List<PickupGame>();
        public List<PickupGame> Upcoming = new List<PickupGame>();
        public List<PickupGame> Past = new List<PickupGame>();

        public void PickupGameManager()
        {
            var today = DateTime.Now;
            foreach(var pg in PickupGames)
            {
                if(pg.DateTime > today)
                {
                    Upcoming.Add(pg);
                }
                else
                {
                    Past.Add(pg);
                }
            }
        }
    }
}
