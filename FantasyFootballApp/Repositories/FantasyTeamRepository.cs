using Dapper;
using FantasyFootballApp.Data;
using FantasyFootballApp.Models;
using System.Data;

namespace FantasyFootballApp.Repositories
{
    public class FantasyTeamRepository : IFantasyTeamRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IDbConnection _conn;
        public FantasyTeamRepository(ApplicationDbContext db, IDbConnection conn)
        {
            _db = db;
            _conn = conn;
        }
        public IEnumerable<FantasyTeam> GetTeams(int id)
        {
            var teams = _db.FantasyTeams.Where(x => x.LeagueId == id).ToArray();
            return teams;
        }
        public void AddLeagueTeams(FantasyLeague league)
        {
            //league.Teams.Select(team => { team.LeagueId = league.LeagueId; return team; });
            _db.FantasyTeams.AddRange(league.Teams);
            _db.SaveChanges();
        }
        public void AddToTeam(Player player)
        {
            var playerToAdd = _db.Players.Where(x => x.Id == player.Id).Select(x => new FantasyTeamPlayer()
            {
                Name = $"{x.FirstName} {x.LastName}",
                PlayerID = x.PlayerID,
                Position = x.Position,
                Team = x.Team,
                FantasyTeamID = 0,
                PhotoUrl = x.PhotoUrl,
            });
            _db.FantasyPlayers.AddRange(playerToAdd);
            _db.SaveChanges();
        }
        public void DropPlayer(int id)
        {
            _db.Remove(new FantasyTeamPlayer() { Id = id});
            _db.SaveChanges();
        }
        public IEnumerable<FantasyLeague> GetAllLeagues()
        {
            var leagues = _db.FantasyLeagues;
            return leagues;
        }

        public FantasyLeague GetLeague(int id)
        {
            var league = _db.FantasyLeagues.Where(x => x.LeagueId == id).ToArray()[0];
            return league;
        }

        public FantasyLeague InitLeague(FantasyLeague league)
        {           
            _db.FantasyLeagues.Add(league);
            _db.SaveChanges();
            //var id = _db.FantasyLeagues.Where(x => x.LeagueName == league.LeagueName).ToArray()[0].LeagueId;
            var leagueDb = _db.FantasyLeagues.OrderBy(x => x.LeagueId).Last();
            return leagueDb;
        }
        public void Save()
        {
            
        }

        public IEnumerable<FantasyTeamPlayer> PlayersAvailable()
        {
            var players = _db.FantasyPlayers.Where(x => x.FantasyTeamID == null).ToArray();
            return players;
        }

        public void InitFantasyPlayers()
        {
            _conn.Execute("TRUNCATE TABLE dbo.FantasyPlayers");
            var players = _db.Players.Where(x => x.Position == "QB"
            || x.Position == "WR"
            || x.Position == "QB"
            || x.Position == "TE"
            || x.Position == "K"
            || x.Position == "RB").Select(x => new FantasyTeamPlayer
            {
                PlayerID = x.PlayerID,
                Position = x.Position,
                ADP = x.AverageDraftPosition,
                Team = x.Team,
                Name = $"{x.FirstName} {x.LastName}",
                PhotoUrl = x.PhotoUrl,
            });
            _db.FantasyPlayers.AddRange(players);
            _db.SaveChanges();
        }

        public DraftViewModel DraftPlayer(int playerId, int fantasyTeamId, int leagueId, int orderIndex, bool reverse, bool flipping)
        {
            var player = _db.FantasyPlayers.Where(x => x.PlayerID == playerId).ToArray()[0];
            player.FantasyTeamID = fantasyTeamId;
            _db.FantasyPlayers.Update(player);
            _db.SaveChanges();
            var draft = new DraftViewModel()
            {
                LeagueId = leagueId,
                Beginning = false,
                Reversed = reverse,
                Flipping = flipping,
                OrderIndex = orderIndex,
            };
            return draft;
        }

        public DraftViewModel UpdateDraft(DraftViewModel draft)
        {
            draft.PlayersAvailable = PlayersAvailable().OrderBy(x => x.ADP == null ? 1 : 0).ThenBy(x => x.ADP).ToArray();
            draft.Teams = _db.FantasyTeams.Where(x => x.LeagueId == draft.LeagueId);
            if (draft.Beginning)
            {
                var team = draft.Teams.ToArray()[0];
                draft.CurrentTeam = team;
                //draft.OrderIndex++;
                draft.Beginning = false;
            }
            else
            {
                if (draft.OrderIndex == 0 && !draft.Flipping && !draft.Reversed)
                {
                    draft.OrderIndex++;
                }
                else if (draft.OrderIndex == 0 && !draft.Flipping && draft.Reversed)
                {
                    draft.Flipping = true;
                    draft.Reversed = false;
                }
                else if (draft.OrderIndex == 0 && draft.Flipping && draft.Reversed)
                {
                    draft.OrderIndex = 0;
                    draft.Flipping = false;
                    draft.Reversed = false;
                }
                else if (draft.OrderIndex == draft.Teams.Count() - 1 && !draft.Flipping && !draft.Reversed)
                {
                    draft.Flipping = true;
                    draft.Reversed = true;
                    draft.OrderIndex = draft.Teams.Count() - 1;
                }
                else if (draft.OrderIndex == draft.Teams.Count() - 1 && draft.Flipping && draft.Reversed)
                {
                    draft.Flipping = false;
                    draft.OrderIndex--;
                }
                else if (!draft.Reversed)
                {
                    draft.OrderIndex++;
                }
                else if (draft.Reversed)
                {
                    draft.OrderIndex--;
                }


                draft.CurrentTeam = draft.Teams.ToArray()[draft.OrderIndex];
            }


            return draft;
        }
    }
}
