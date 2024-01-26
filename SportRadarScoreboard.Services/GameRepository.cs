using SportRadarScoreboard.Services.Persistence;

namespace SportRadarScoreboard.Services;

public class GameRepository
{
    List<Game> _games;

    public GameRepository()
    {
        _games = new List<Game>();
    }
}