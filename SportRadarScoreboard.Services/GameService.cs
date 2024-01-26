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

    public void UpdateScore(Guid id, int homeScore, int awayScore)
    {
        var game = _gameRepository.GetGameDetails(id);

        if (game is null)
        {
            throw new InvalidInputException("invalid game id");
        }

        if (game.IsFinished)
        {
            throw new InvalidRequestException("can not change score of finished game");
        }

        _gameRepository.ChangeGameScore(id, homeScore, awayScore);
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