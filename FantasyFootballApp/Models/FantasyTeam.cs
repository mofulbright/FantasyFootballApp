using System.ComponentModel.DataAnnotations;

namespace FantasyFootballApp.Models
{
    public class FantasyTeam
    {
        [Key]
        public int TeamId { get; set; }
        public int LeagueId { get; set; }
        public string TeamName { get; set; }
    }
}
