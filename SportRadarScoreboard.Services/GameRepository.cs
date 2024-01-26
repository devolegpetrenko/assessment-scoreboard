using SportRadarScoreboard.Core.Games;
using SportRadarScoreboard.Core.Games.Models;
using SportRadarScoreboard.Services.Persistence;

namespace SportRadarScoreboard.Services;

public class GameRepository : IGameRepository
{
    List<Game> _games;
    List<GameScore> _gameScores;

    public GameRepository()
    {
        _games = new List<Game>();
        _gameScores = new List<GameScore>();
    }

    public Guid AddGame(string homeTeam, string awayTeam)
    {
        var id = new Guid();

        _games.Add(new Game
        {
            Id = id,

            HomeTeam = homeTeam,
            AwayTeam = awayTeam,

            Started = DateTime.UtcNow,
        });

        _gameScores.Add(new GameScore
        {
            GameId = id,
            Home = 0,
            Away = 0,
        });

        return id;
    }

    public void ChangeGameScore(Guid id, int homeScore, int awayScore)
    {
        var gameScore = _gameScores.FirstOrDefault(x => x.GameId == id);
        gameScore.Home = homeScore;
        gameScore.Away = awayScore;
    }

    public void FinishGame(Guid id)
    {
        throw new NotImplementedException();
    }

    public GameDetails? GetGameDetails(Guid id)
    {
        return _games
            .Select(x => new GameDetails
            {
                IsFinished = x.Finished,
                Id = x.Id
            })
            .FirstOrDefault(x => x.Id == id);
    }

    public void GetNotFinishedGames()
    {
        throw new NotImplementedException();
    }

    public bool IsGameInProgress(string homeTeam, string awayTeam)
    {
        return _games.Any(x =>
            !x.Finished
            && string.Equals(x.HomeTeam, homeTeam, StringComparison.InvariantCultureIgnoreCase)
            && string.Equals(x.AwayTeam, awayTeam, StringComparison.InvariantCultureIgnoreCase));
    }

    public bool IsGameInProgress(Guid id)
    {
        return _games.Any(x => x.Id == id && x.Finished);
    }
}