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
        private bool _archived;

        public virtual Location Location { get; set; }
        public virtual ICollection<PlayerGame> PlayerGames { get; set; }

        public Game()
        {
        }

        public bool HasThisPlayer(Player player)
        {
            return PlayerGames.Any(x => x.PlayerId == player.Id);
        }
        public void PlayerConfirm(Player player)
        {
            PlayerGames.First(x => x.PlayerId == player.Id).IsConfirmed = true;
            EloAlarm();
        }


        void EloAlarm()
        {
            if (!IsConfirmed()|| _archived) return;
            _archived = true;
            List<Player> winners = PlayerGames.Where(x => x.IsWin).Select(x => x.Player).ToList();
            List<Player> losers = PlayerGames.Where(x => !x.IsWin).Select(x => x.Player).ToList();

            int winnerElo = PlayerGames.Where(x => x.IsWin).Sum(x => x.Player.Elo) / losers.Count;

            int loserElo = PlayerGames.Where(x => !x.IsWin).Sum(x => x.Player.Elo) / losers.Count;

            winners.ForEach(x => x.CalculateEloWin(loserElo));
            losers.ForEach(x => x.CalculateEloLose(winnerElo));
        }
    }
}
