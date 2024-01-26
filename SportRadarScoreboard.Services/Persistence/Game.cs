namespace SportRadarScoreboard.Services.Persistence;

public class Game
{
    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }

    public int HomeScore { get; set; }
    public int AwayScore { get; set; }

    public DateTime Started { get; set; }
    public bool Finished { get; set; }
}