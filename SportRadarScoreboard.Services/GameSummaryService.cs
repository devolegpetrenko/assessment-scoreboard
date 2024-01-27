using SportRadarScoreboard.Core.Games;
using SportRadarScoreboard.Core.Games.Models;
using SportRadarScoreboard.Services.Extensions;

namespace SportRadarScoreboard.Services;

public class GameSummaryService : IGameSummaryService
{
    IGameRepository _gameRepository;

    public GameSummaryService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public List<GameSummary> GetInProgress()
    {
        var games = _gameRepository.GetGameSummaries();

        return games
            .Where(x => !x.IsFinished)
            .OrderByTotalScore()
            .ToList();
    }
}