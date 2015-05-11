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
        public bool IsConfirmed() { return PlayerGames.All(x => x.IsConfirmed); }

        public virtual Location Location { get; set; }
        public virtual ICollection<PlayerGame> PlayerGames { get; set; }

        public Game()
        {
            PlayerGames = PlayerGames ?? new List<PlayerGame>() { };
        }

        public bool HasThisPlayer(Player player)
        {
            return PlayerGames.Any(x => x.PlayerId == player.Id);
        }
        public void PlayerConfirm(Player player)
        {
             PlayerGames.First(x => x.PlayerId == player.Id).IsConfirmed = true;
        }
    }
}