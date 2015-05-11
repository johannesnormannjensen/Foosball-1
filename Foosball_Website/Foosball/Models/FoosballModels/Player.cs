using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Foosball.Models.FoosballModels
{
    public class Player
    {
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
    }
}