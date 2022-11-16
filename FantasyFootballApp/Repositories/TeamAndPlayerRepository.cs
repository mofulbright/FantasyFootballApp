using Dapper;
using FantasyFootballApp.Data;
using FantasyFootballApp.Models;
using System.Data;

namespace FantasyFootballApp.Repositories
{
    public class TeamAndPlayerRepository : ITeamAndPlayerRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly APIClient _api;
        private readonly IDbConnection _conn;
        public TeamAndPlayerRepository(ApplicationDbContext db, APIClient api, IDbConnection conn)
        {
            _db = db;
            _api = api;
            _conn = conn;
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _db.Players.OrderBy(x => x.TeamID);
        }

        public IEnumerable<NFLTeam> GetAllTeams()
        {
            return _db.NFLTeams;
        }

        public Player GetPlayer(int id)
        {
            return _db.Players.Where(x => x.Id == id).ToArray()[0];
        }

        public NFLTeam GetTeam(int id)
        {
            return _db.NFLTeams.Where(x => x.Id == id).ToArray()[0];
        }

        public IEnumerable<Player> SearchPlayer(string searchString)
        {
            var players = _db.Players.ToList().Where(x => x.FirstName.Contains(searchString) || x.LastName.Contains(searchString) || $"{x.FirstName} {x.LastName}" == searchString).ToList();
            return players;
        }

        public void UpdatePlayers()
        {
            var players = _api.GetPlayers().Where(x => x.Status == "Active");
            _conn.Execute("TRUNCATE TABLE dbo.Players");
            _db.Players.AddRange(players);
            //_conn.Execute("SET IDENTITY_INSERT Players ON");

            _db.SaveChanges();
        }
        public void UpdateTeams()
        {
            var teams = _api.GetStandings();
            _conn.Execute("TRUNCATE TABLE dbo.NFLTeams");
            _db.NFLTeams.AddRange(teams);
            _db.SaveChanges();
        }

    }
}
