using SportRadarScoreboard.Core.Games;
using SportRadarScoreboard.Core.Games.Models;
using SportRadarScoreboard.Services.Exceptions;

namespace SportRadarScoreboard.Services;

public class GameService : IGameService
{
    private IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public Guid StartGame(string homeTeam, string awayTeam)
    {
        if (string.IsNullOrWhiteSpace(homeTeam))
        {
            throw new InvalidInputException("invalid home team name");
        }

        if (string.IsNullOrWhiteSpace(awayTeam))
        {
            throw new InvalidInputException("invalid away team name");
        }

        if (_gameRepository.IsGameInProgress(homeTeam, awayTeam))
        {
            throw new InvalidRequestException("game is in progress");
        }

        return _gameRepository.AddGame(homeTeam, awayTeam);
    }

    public void UpdateScore(Guid id, int homeScore, string awayScore)
    {
        throw new NotImplementedException();
    }

    public void FinishGame(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<GameModel> GetInProgressSummary()
    {
        throw new NotImplementedException();
    }
}