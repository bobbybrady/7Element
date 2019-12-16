using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _7Element.Models.ViewModels
{
    public class PredsGameDetailViewModel
    {
        public PredsGame PredsGame { get; set; }
        public ApplicationUser User { get; set; }
        public UserPredsGame UserPredsGame { get; set; }
        public int predsGameId { get; set; }
        public string userId { get; set; }
    }
}
