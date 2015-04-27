using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Razor.Text;

namespace Foosball.Models.FoosballModels
{
    public class Game
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public DateTime Date { get; set; }
        public bool IsConfirmed { get { return PlayerGames.All(x => x.IsConfirmed); } }

        public virtual Location Location { get; set; }
        public virtual ICollection<PlayerGame> PlayerGames { get; set; }


    }
}