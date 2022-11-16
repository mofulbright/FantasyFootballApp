using FantasyFootballApp.Data;
using FantasyFootballApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Data;
using FantasyFootballApp.Repositories;

namespace FantasyFootballApp.Controllers
{
    public class PlayerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ITeamAndPlayerRepository _repo;
        public PlayerController(ApplicationDbContext db, ITeamAndPlayerRepository repo)
        {
            _db = db;
            _repo = repo;
        }
        public IActionResult Index()
        {
            var players = _repo.GetAllPlayers();
            return View(players);
        }
        public IActionResult Draft()
        {
            var players = _repo.GetAllPlayers().Where(x => x.AverageDraftPosition != null).OrderBy(x => x.AverageDraftPosition);
            return View(players);
        }
        public IActionResult UpdatePlayers()
        {
            _repo.UpdatePlayers();
            return RedirectToAction("Index");
        }
        
        public IActionResult SearchPlayer(string searchString)
        {
            var players = _repo.SearchPlayer(searchString);
            return View(players);
        }
        public IActionResult ViewPlayer(int Id)
        {
            return View(_db.Players.Where(x => x.Id == Id).ToArray()[0]);
        }
    }
}
