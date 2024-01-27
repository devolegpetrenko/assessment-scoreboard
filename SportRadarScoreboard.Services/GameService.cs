using SportRadarScoreboard.Core.Games;
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
            throw new GameBetweeTeamsIsInProgressException();
        }

        return _gameRepository.AddGame(homeTeam, awayTeam);
    }

    public void UpdateScore(Guid id, int homeScore, int awayScore)
    {
        var gameState = _gameRepository.GetGameState(id);

        if (gameState is null)
        {
            throw new GameNotFoundException();
        }

        if (gameState.IsFinished)
        {
            throw new GameIsFinishedException();
        }

        _gameRepository.ChangeGameScore(id, homeScore, awayScore);
    }

    public void FinishGame(Guid id)
    {
        var gameState = _gameRepository.GetGameState(id);

        if (gameState is null)
        {
            throw new GameNotFoundException();
        }

        if (gameState.IsFinished)
        {
            throw new GameIsFinishedException();
        }

        _gameRepository.FinishGame(id);
    }
}