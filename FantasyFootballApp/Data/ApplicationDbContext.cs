using FantasyFootballApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FantasyFootballApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Player> Players { get; set; }
        public DbSet<FantasyTeamPlayer> FantasyPlayers { get; set; }
        public DbSet<NFLTeam> NFLTeams { get; set; }
        public DbSet<FantasyTeam> FantasyTeams { get; set; }
        public DbSet<FantasyLeague> FantasyLeagues { get; set; }

    }
}
