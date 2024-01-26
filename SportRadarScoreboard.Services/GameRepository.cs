using SportRadarScoreboard.Core.Games;
using SportRadarScoreboard.Services.Persistence;

namespace SportRadarScoreboard.Services;

public class GameRepository : IGameRepository
{
    List<Game> _games;

    public GameRepository()
    {
        _games = new List<Game>();
    }

    public Guid AddGame(string homeTeam, string awayTeam)
    {
        var id = new Guid();

        _games.Add(new Game
        {
            Id = id,

            HomeTeam = homeTeam,
            AwayTeam = awayTeam,

            HomeScore = 0,
            AwayScore = 0,

            Started = DateTime.UtcNow,
        });

        return id;
    }

    public void ChangeGameScore(Guid id, int homeScore, string awayScore)
    {
        throw new NotImplementedException();
    }

    public void FinishGame(Guid id)
    {
        throw new NotImplementedException();
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
}