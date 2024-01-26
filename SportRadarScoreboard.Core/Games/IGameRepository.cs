using SportRadarScoreboard.Core.Games.Models;

namespace SportRadarScoreboard.Core.Games;

public interface IGameRepository
{
    Guid AddGame(string homeTeam, string awayTeam);
    GameDetails? GetGameDetails(Guid id);
    bool IsGameInProgress(string homeTeam, string awayTeam);
    bool IsGameInProgress(Guid id);
    void ChangeGameScore(Guid id, int homeScore, int awayScore);
    void FinishGame(Guid id);
    void GetNotFinishedGames();
}