using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _7Element.Models.ViewModels
{
    public class PredsGameIndexViewModel
    {
        public List<PredsGame> PredsGames {get; set;}
        public ApplicationUser User { get; set; }
    }
}
