namespace SportRadarScoreboard.Services.Persistence;

public class GameScore
{
    public Guid GameId { get; set; }

    public int Home { get; set; }
    public int Away { get; set; }
}