using SportRadarScoreboard.Core.Games.Models;

namespace SportRadarScoreboard.Services.Extensions;

public static class GameSummaryExtensions
{
    public static IOrderedEnumerable<GameSummary> OrderByTotalScore(this IEnumerable<GameSummary> games) => games
        .OrderByDescending(x => x.Score.Total)
        .ThenByDescending(x => x.Started);
}