namespace Foosball.Models.FoosballModels
{
    public class PlayerGame
    {
        public int PlayerId { get; set; }
        public int GameId { get; set; }

        public bool IsWin { get; set; }
        public bool? IsConfirmed { get; set; }

        public virtual Player Player { get; set; }
        public virtual Game Game { get; set; }
    }
}