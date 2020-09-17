using MatchingGame2.models.game;
using MatchingGame2.models.gameUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MatchingGame2.database
{
    public class AzureSqlDbContext : IdentityDbContext<ApplicationUser> 
    {
        public AzureSqlDbContext([NotNullAttribute]DbContextOptions options) : base(options)
        {

        }

        public DbSet<Game>Games { get; set; }
        public DbSet<GameAdmin>GamesAdmins { get; set; }
        public DbSet<GameParticipant>GamesParticipants { get; set; }
        public DbSet<GameView>View_ActiveGames { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<GameAdmin>()
                .HasKey(user => new { user.GameId, user.UserId });
            builder.Entity<GameParticipant>()
                .HasKey(user => new { user.GameId, user.UserId });

        }

    }
}
