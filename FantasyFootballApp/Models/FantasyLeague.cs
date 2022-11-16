using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyFootballApp.Models
{
    public class FantasyLeague
    {
        [Key]
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        [NotMapped]
        public FantasyTeam[] Teams { get; set; }
    }
}
