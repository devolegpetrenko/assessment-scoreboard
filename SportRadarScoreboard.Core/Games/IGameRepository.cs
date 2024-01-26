namespace SportRadarScoreboard.Core.Games;

public interface IGameRepository
{
    public Guid AddGame(string homeTeam, string awayTeam);
    public bool IsGameInProgress(string homeTeam, string awayTeam);
    public void ChangeGameScore(Guid id, int homeScore, string awayScore);
    public void FinishGame(Guid id);
    public void GetNotFinishedGames();
}