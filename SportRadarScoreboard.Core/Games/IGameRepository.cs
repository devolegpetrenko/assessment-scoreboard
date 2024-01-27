using SportRadarScoreboard.Core.Games.Models;

namespace SportRadarScoreboard.Core.Games;

public interface IGameRepository
{
    Guid AddGame(string homeTeam, string awayTeam);
    GameState? GetGameState(Guid id);
    bool IsGameInProgress(string homeTeam, string awayTeam);
    void ChangeGameScore(Guid id, int homeScore, int awayScore);
    void FinishGame(Guid id);
    List<GameSummary> GetGameSummaries();
}