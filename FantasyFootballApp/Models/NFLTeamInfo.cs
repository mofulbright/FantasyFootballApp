namespace FantasyFootballApp.Models
{
    public class NFLTeamInfo
    {
        public int TeamID { get; set; }
        public int StadiumID { get; set; }
        public int ByeWeek { get; set; }
        public string HeadCoach { get; set; }
        public string OffensiveCoordinator { get; set; }
        public string DefensiveCoordinator { get; set; }
        public string OffensiveScheme { get; set; }
        public string DefensiveScheme { get; set; }
        public string WikipediaLogoUrl { get; set; }
    }
}
