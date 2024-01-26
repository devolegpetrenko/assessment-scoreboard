using SportRadarScoreboard.Core.Games;
using SportRadarScoreboard.Core.Games.Models;

namespace SportRadarScoreboard.Services
{
    public class GameService : IGameService
    {
        private GameRepository _gameRepository;

        public GameService(GameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Guid StartGame(string homeTeam, string awayTeam)
        {
            throw new NotImplementedException();
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
}