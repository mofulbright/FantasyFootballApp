using FantasyFootballApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballApp.Controllers
{
    public class NFLTeamController : Controller
    {
        private readonly ITeamAndPlayerRepository _repo;
        public NFLTeamController(ITeamAndPlayerRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View(_repo.GetAllTeams());
        }
        public IActionResult ViewTeam(int id)
        {
            return View(_repo.GetTeam(id));
        }
        public IActionResult UpdateTeams()
        {
            _repo.UpdateTeams();
            return RedirectToAction("Index");
        }
    }
}
