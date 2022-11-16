using System.ComponentModel.DataAnnotations;

namespace FantasyFootballApp.Models
{
    public class NoFantasyTeamPlayer
    {
        [Key]
        public int Id { get; set; }
        public int PlayerID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Team { get; set; }
        public int? FantasyTeamID { get; set; } = null;
        public string PhotoUrl { get; set; }
    }
}
