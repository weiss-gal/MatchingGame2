using MatchingGame2.models.game;
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
        public DbSet<GameView>View_ActiveGames { get; set; }
    }
}
