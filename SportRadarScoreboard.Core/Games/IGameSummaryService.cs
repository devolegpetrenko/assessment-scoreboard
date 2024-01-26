using SportRadarScoreboard.Core.Games.Models;

namespace SportRadarScoreboard.Core.Games;

public interface IGameSummaryService
{
    public List<GameModel> GetInProgressSummary();
}