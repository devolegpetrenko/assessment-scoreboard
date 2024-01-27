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
        var id = Guid.NewGuid();

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
        var game = _games.FirstOrDefault(x => x.Id == id);
        game.IsFinished = true;
    }

    public GameDetails? GetGameDetails(Guid id)
    {
        return _games
            .Select(x => new GameDetails
            {
                IsFinished = x.IsFinished,
                Id = x.Id
            })
            .FirstOrDefault(x => x.Id == id);
    }

    public List<GameSummary> GetGameSummaries() => _games.Join(
        _gameScores,
        g => g.Id,
        gs => gs.GameId,
        (g, gs) => new GameSummary
        {
            Id = g.Id,
            HomeTeam = g.HomeTeam,
            AwayTeam = g.AwayTeam,
            Score = new Score(gs.Home, gs.Away),
            Started = g.Started,
            IsFinished = g.IsFinished,
        }).ToList();

    public bool IsGameInProgress(string homeTeam, string awayTeam)
    {
        return _games.Any(x =>
            !x.IsFinished
            && string.Equals(x.HomeTeam, homeTeam, StringComparison.InvariantCultureIgnoreCase)
            && string.Equals(x.AwayTeam, awayTeam, StringComparison.InvariantCultureIgnoreCase));
    }

    public bool IsGameInProgress(Guid id)
    {
        return _games.Any(x => x.Id == id && x.IsFinished);
    }
}