namespace SportRadarScoreboard.Core.Games.Models;

public class GameSummary
{
    public Guid Id { get; set; }
    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }
    public Score Score { get; set; }
    public DateTime Started { get; set; }
    public bool IsFinished { get; set; }
}