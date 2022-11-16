using FantasyFootballApp.Models;

namespace FantasyFootballApp.Repositories
{
    public interface ITeamAndPlayerRepository
    {
        public Player GetPlayer(int id);
        public IEnumerable<Player> GetAllPlayers();
        public NFLTeam GetTeam(int id);
        public IEnumerable<NFLTeam> GetAllTeams();
        public void UpdateTeams();
        public void UpdatePlayers();
        public IEnumerable<Player> SearchPlayer(string searchString);
    }
}
