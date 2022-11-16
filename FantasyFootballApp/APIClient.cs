using FantasyFootballApp.Models;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace FantasyFootballApp
{
    public class APIClient
    {
        HttpClient client = new HttpClient();
        private readonly string _apiKey;
        public APIClient(string apiKey)
        {
            _apiKey = apiKey;
        }
        public IEnumerable<Player> GetPlayers()
        {
            var endpoint = $"https://api.sportsdata.io/v3/nfl/scores/json/Players?key={_apiKey}";
            var response = client.GetStringAsync(endpoint).Result;
            var json = JArray.Parse(response);
            var players = new List<Player>();
            foreach (var player in json)
            {
                players.Add(JsonSerializer.Deserialize<Player>(player.ToString()));
            }
            return players;
        }
        public IEnumerable<NFLTeam> GetStandings()
        {
            var endpoint = $"https://api.sportsdata.io/v3/nfl/scores/json/Standings/2022reg?key={_apiKey}";           
            var response = client.GetStringAsync(endpoint).Result;
            var json = JArray.Parse(response);
            var teams = new List<NFLTeam>();
            var teamsInfo = GetTeamInfo();
            foreach (var team in json)
            {
                var teamToAdd = JsonSerializer.Deserialize<NFLTeam>(team.ToString());
                teams.Add(teamToAdd);
            }      
            foreach (var team in teams)
            {
                var teamInfo = teamsInfo.Where(x => x.TeamID == team.TeamID).ToArray()[0];
                team.ByeWeek = teamInfo.ByeWeek;
                team.StadiumID = teamInfo.StadiumID;
                team.HeadCoach = teamInfo.HeadCoach;
                team.WikipediaLogoUrl = teamInfo.WikipediaLogoUrl;
                team.OffensiveCoordinator = teamInfo.OffensiveCoordinator;
                team.DefensiveCoordinator = teamInfo.DefensiveCoordinator;
                team.OffensiveScheme = teamInfo.OffensiveScheme;
                team.DefensiveScheme = teamInfo.DefensiveScheme;
            }
            return teams;
        }
        public IEnumerable<NFLTeamInfo> GetTeamInfo()
        {
            var endpoint = $"https://api.sportsdata.io/v3/nfl/scores/json/Teams?key={_apiKey}";
            var response = client.GetStringAsync(endpoint).Result;
            var json = JArray.Parse(response);
            var teamsInfo = new List<NFLTeamInfo>();
            foreach (var team in json)
            {
                teamsInfo.Add(JsonSerializer.Deserialize<NFLTeamInfo>(team.ToString()));               
            }
            return teamsInfo;
        }
    }
}
