using System;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices;
using System.Linq;
using Microsoft.Ajax.Utilities;

namespace Foosball.Models.FoosballClasses
{
    public class Match
    {
        public int MatchId { get; set; }

        [Required]
        public string Location;

        [Required]
        public DateTime Date;

        [Required]
        public Player[] Players;

        [Required]
        public bool[] Confirmations;

        public bool IsConfirmed
        {
            get
            {
                if (Confirmations != null) return !Confirmations.Where(x => !x).Any();
                return false;
            }
        } //is true if there is no x that is false

        public Match() { }

        public Match(string location, DateTime date, Player[] players, bool[] confirmations)
        {
            Location = location;
            Date = date;
            Players = players;
            Confirmations = confirmations;
        }

        public Match(int matchId, string location, DateTime date, Player[] players, bool[] confirmations)
        {
            MatchId = matchId;
            Location = location;
            Date = date;
            Players = players;
            Confirmations = confirmations;
        }

        public bool Confirm(Player player)
        {
            int index = Players.ToList().IndexOf(Players.ToList().First(x => (x.UserId == player.UserId)));
            if (index < 0) return false;
            Confirmations[index] = true;
            if (IsConfirmed)
                EloAlarm();

            return true;
        }

        void EloAlarm()
        {
            if (!IsConfirmed) return;

            int team1Elo = 0;

            int team2Elo = 0;

            for (int i = 0; i < Players.Length / 2; i++)
            {
                team1Elo += Players[i].EloPoints / 2;
                team2Elo += Players[Players.Length - i - 1].EloPoints / 2;
            }
            for (int i = 0; i < Players.Length / 2; i++)
            {
                Players[i].CalculateEloWin(team2Elo);
                Players[Players.Length - i - 1].CalculateEloLose(team1Elo);
            }
        }

    }

}