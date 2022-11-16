using System.ComponentModel.DataAnnotations;

namespace FantasyFootballApp.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public int PlayerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string PositionCategory { get; set; }
        public int? Age { get; set; }
        public string Height { get; set; }
        public int? Weight { get; set; }
        public string Team { get; set; }
        public string Status { get; set; }

        public int? TeamID { get; set; }
        public string PhotoUrl { get; set; }
        public double? AverageDraftPosition { get; set; }
    }
}
