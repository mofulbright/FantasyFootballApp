using FantasyFootballApp.Models;

namespace FantasyFootballApp.Repositories
{
    public interface IFantasyTeamRepository
    {
        public void AddToTeam(Player player);
        public IEnumerable<FantasyTeam> GetTeams(int id);
        public void DropPlayer(int id);
        public void AddLeagueTeams(FantasyLeague league);
        public FantasyLeague GetLeague(int id);
        public IEnumerable<FantasyLeague> GetAllLeagues();
        public FantasyLeague InitLeague(FantasyLeague league);
        public IEnumerable<FantasyTeamPlayer> PlayersAvailable();
        public void InitFantasyPlayers();
        public void DraftPlayer(int playerId, int fantasyTeamId);
        public DraftViewModel UpdateDraft(DraftViewModel draft);
        public void Save();
    }
}
