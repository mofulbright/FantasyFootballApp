namespace FantasyFootballApp.Models
{
    public class DraftViewModel
    {
        public FantasyTeam CurrentTeam { get; set; }
        public IEnumerable<FantasyTeamPlayer> CurrentTeamsPlayers { get; set; }
        public string LeagueName { get; set; }
        public int LeagueId { get; set; }
        public IEnumerable<FantasyTeam> Teams { get; set; }
        public FantasyTeamPlayer[] PlayersAvailable { get; set; }
        public bool Beginning { get; set; }
        public int Reversed { get; set; }
        public int OrderIndex { get; set; }
        public int TeamToDraft { get; set; }
        public int PlayerToDraft { get; set; }
        public int Flipping { get; set; }
    }
}
