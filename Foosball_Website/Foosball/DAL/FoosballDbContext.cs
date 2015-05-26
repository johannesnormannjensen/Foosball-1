using System.Data.Entity;
using Foosball.Models;
using Foosball.Models.FoosballModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Foosball.DAL
{
    public class FoosballDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerGame> PlayerGames { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Location> Locations { get; set; }

        public FoosballDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public static FoosballDbContext Create()
        {
            return new FoosballDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<PlayerGame>()
                .HasKey(c => new { c.PlayerId, c.GameId });

            modelBuilder.Entity<Player>()
                .HasMany(c => c.PlayerGames)
                .WithRequired()
                .HasForeignKey(c => c.PlayerId);

            modelBuilder.Entity<Game>()
                .HasMany(c => c.PlayerGames)
                .WithRequired()
                .HasForeignKey(c => c.GameId);
        }

    }
}