namespace SportRadarScoreboard.Core.Games;

public interface IGameService
{
    public Guid StartGame(string homeTeam, string awayTeam);
    public void UpdateScore(Guid id, int homeScore, string awayScore);
    public void FinishGame(Guid id);
}