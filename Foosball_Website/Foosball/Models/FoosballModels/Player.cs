using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Foosball.Models.FoosballModels
{
//    Created by Ferenc Hammerl

    public class Player
    {
        public const int K = 32;
        public int Id { get; set; }
        public int Elo { get; set; }

        [Required]
        public string Username { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<PlayerGame> PlayerGames { get; set; }

        public Player()
        {
            Elo = 1400;
        }

        public void CalculateEloWin(int averageElo)
        {
            Elo += EloChange(averageElo, true);
        }

        public void CalculateEloLose(int averageElo)
        {
            Elo -= EloChange(averageElo, false);
        }

        public int EloChange(int averageElo, bool isVictory)
        {
            int res = (int)Math.Round(ChanceToWin(averageElo) * K);
            return isVictory ? K - res : res;
        }

        public double ChanceToWin(int averageElo)
        {
            return 1 / (1 + Math.Pow(10.0, (averageElo - (double)Elo) / 400));
        }
    }
}

