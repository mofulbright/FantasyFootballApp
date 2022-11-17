using FantasyFootballApp.Models;
using FantasyFootballApp.Repositories;
using Medallion;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballApp.Controllers
{
    public class FantasyTeamController : Controller
    {
        private readonly IFantasyTeamRepository _repo;
        public FantasyTeamController(IFantasyTeamRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var leagues = _repo.GetAllLeagues();
            return View(leagues);
        }
        public IActionResult ViewLeague(int id)
        {
            var league = _repo.GetLeague(id);
            league.Teams = _repo.GetTeams(id).ToArray();
            return View(league);
        }
        public IActionResult NewLeague(int leagueSize, string leagueName)
        {
            var league = new FantasyLeague();
            league.LeagueName = leagueName;
            league.Teams = new FantasyTeam[leagueSize];
            
            return View(league);
        }
        public IActionResult StartDraft(FantasyLeague league)
        {            
            var d = new DraftViewModel();
            d.Teams = _repo.GetTeams(league.LeagueId).ToArray();
            InitFantasyPlayers();
            d.LeagueName = league.LeagueName;
            d.LeagueId = league.LeagueId;
            //d.PlayersAvailable = _repo.PlayersAvailable();
            d.OrderIndex = 0;
            d.Beginning = true;
            d.Reversed = false;
            d.Flipping = false;
            //_repo.UpdateDraft(d);
            return RedirectToAction("Draft", d);
        }
        public IActionResult Draft(DraftViewModel draft)
        {
            _repo.UpdateDraft(draft);
            return View(draft);
        }
        public IActionResult AddLeague(FantasyLeague league)
        {
            var leagueDb = _repo.InitLeague(league);
            foreach (var team in league.Teams)
            {
                team.LeagueId = league.LeagueId;
            }
            _repo.AddLeagueTeams(league);
            return RedirectToAction("ViewLeague", new { id = league.LeagueId});
        }
        public IActionResult DraftPlayer(int player, int team, int league, int index, bool reversed, bool flipping)
        {
            var draft = _repo.DraftPlayer(player, team, league, index, reversed, flipping);
            //_repo.UpdateDraft(draft);
            return RedirectToAction("Draft", draft);
        }
        public IActionResult AddToTeam(Player player)
        {
            _repo.AddToTeam(player);
            return RedirectToAction("Index");
        }
        public IActionResult DropPlayer(int id)
        {
            _repo.DropPlayer(id);
            return RedirectToAction("Index");
        }
        public IActionResult InitFantasyPlayers()
        {
            _repo.InitFantasyPlayers();
            return RedirectToAction("Index");
        }
    }
}
