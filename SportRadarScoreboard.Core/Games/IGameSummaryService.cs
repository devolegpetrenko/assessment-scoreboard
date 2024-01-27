using SportRadarScoreboard.Core.Games.Models;

namespace SportRadarScoreboard.Core.Games;

public interface IGameSummaryService
{
    /// <summary>
    /// Get a summary of games in progress ordered by their total score. The games with the same total score will be returned ordered by the most recently started match in the scoreboard.
    /// </summary>
    public List<GameSummary> GetInProgress();
}