using System.ComponentModel.DataAnnotations;

namespace FantasyFootballApp.Models
{
    public class NFLTeam
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
        public int DivisionWins { get; set; }
        public int DivisionLosses { get; set; }
        public int DivisionTies { get; set; }
        public int ConferenceWins { get; set; }
        public int ConferenceLosses { get; set; }
        public int ConferenceTies { get; set; }
        public int? TeamID { get; set; }
        public string Conference { get; set; }
        public string Division { get; set; }
        public int DivisionRank { get; set; }
        public int ConferenceRank { get; set; }
        public int StadiumID { get; set; }
        public int ByeWeek { get; set; }
        public string HeadCoach { get; set; }
        public string? OffensiveCoordinator { get; set; }
        public string? DefensiveCoordinator { get; set; }
        public string OffensiveScheme { get; set; }
        public string DefensiveScheme { get; set; }
        public string WikipediaLogoUrl { get; set; }

    }
}
