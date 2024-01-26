namespace SportRadarScoreboard.Core.Games.Models;

public class GameModel
{
    public Guid Id { get; set; }

    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }

    public GameScore Score { get; set; }
}