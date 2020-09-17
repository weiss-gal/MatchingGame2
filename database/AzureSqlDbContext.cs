using MatchingGame2.models.game;
using MatchingGame2.models.gameUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace MatchingGame2.database
{
    public class AzureSqlDbContext : IdentityDbContext<ApplicationUser>
    {
        public AzureSqlDbContext([NotNullAttribute]DbContextOptions options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
        //public DbSet<GameParticipant> GamesToParticipants { get; set; }
       // public DbSet<GameAdmin> GamesToAdmins { get; set; }
        // We use this view for accessing only the active (not-deleted) games. 
        public DbSet<GameView> View_ActiveGames { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //// Wire Game<=>Participants relationship
            //builder.Entity<GameParticipant>()
            //    .HasKey(gameUser => new { gameUser.GameId, gameUser.UserId });

            //// Wire Game<=>Admins relationship
            //builder.Entity<GameAdmin>()
            //    .HasKey(gameUser => new { gameUser.GameId, gameUser.UserId });
        }
    }
}
