namespace SportRadarScoreboard.Services.Persistence;

public class Game
{
    public Guid Id { get; set; }

    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }

    public DateTime Started { get; set; }
    public bool Finished { get; set; }
}