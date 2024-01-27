using FluentAssertions;
using Moq;
using SportRadarScoreboard.Core.Games;
using SportRadarScoreboard.Core.Games.Models;
using SportRadarScoreboard.Services;

namespace SportRadarScoreboard.UnitTests;

public class GameSummaryServiceTest
{
    [Test]
    public void GetInProgress_NoGames_OutputEmpty()
    {
        //arrange
        var gameRepository = new Mock<IGameRepository>(MockBehavior.Strict);
        gameRepository.Setup(x => x.GetGameSummaries()).Returns(new List<GameSummary>(0));

        var sut = new GameSummaryService(gameRepository.Object);

        //act
        var games = sut.GetInProgress();

        //assert
        games.Should().BeEmpty();
    }

    [Test]
    public void GetInProgress_RandomOrderForGames_OutputInExpectedOrder()
    {
        //arrange
        var getGameSummariesOutput = new List<GameSummary>();
        getGameSummariesOutput.Add(BuildGameWithScore(2, 5, DateTime.Parse("9:00"), isFinished: false));
        getGameSummariesOutput.Add(BuildGameWithScore(1, 3, DateTime.Parse("9:10"), isFinished: false));
        getGameSummariesOutput.Add(BuildGameWithScore(0, 0, DateTime.Parse("9:50"), isFinished: false));
        getGameSummariesOutput.Add(BuildGameWithScore(7, 2, DateTime.Parse("9:20"), isFinished: false));
        getGameSummariesOutput.Add(BuildGameWithScore(0, 0, DateTime.Parse("9:40"), isFinished: false));
        getGameSummariesOutput.Add(BuildGameWithScore(4, 3, DateTime.Parse("9:30"), isFinished: false));

        var gameRepository = new Mock<IGameRepository>(MockBehavior.Strict);
        gameRepository.Setup(x => x.GetGameSummaries()).Returns(getGameSummariesOutput);

        var sut = new GameSummaryService(gameRepository.Object);

        var expectedGamesOutput = new List<GameSummary>
        {
            getGameSummariesOutput[3],
            getGameSummariesOutput[5],
            getGameSummariesOutput[0],
            getGameSummariesOutput[1],
            getGameSummariesOutput[2],
            getGameSummariesOutput[4],
        };

        //act
        var games = sut.GetInProgress();

        //assert
        games.Should().Equal(expectedGamesOutput);
    }

    [Test]
    public void GetInProgress_BothFinishedAndInProgressGames_OutputOnlyInProgressGames()
    {
        //arrange
        var getGameSummariesOutput = new List<GameSummary>();
        getGameSummariesOutput.Add(BuildGameWithScore(2, 5, DateTime.Parse("9:00"), isFinished: true));
        getGameSummariesOutput.Add(BuildGameWithScore(4, 3, DateTime.Parse("9:10"), isFinished: false));
        getGameSummariesOutput.Add(BuildGameWithScore(0, 0, DateTime.Parse("9:20"), isFinished: true));
        getGameSummariesOutput.Add(BuildGameWithScore(2, 2, DateTime.Parse("9:10"), isFinished: false));

        var gameRepository = new Mock<IGameRepository>(MockBehavior.Strict);
        gameRepository.Setup(x => x.GetGameSummaries()).Returns(getGameSummariesOutput);

        var sut = new GameSummaryService(gameRepository.Object);

        var expectedGamesOutput = new List<GameSummary>
        {
            getGameSummariesOutput[1],
            getGameSummariesOutput[3],
        };

        //act
        var games = sut.GetInProgress();

        //assert
        games.Should().Equal(expectedGamesOutput);
    }

    static GameSummary BuildGameWithScore(int home, int away, DateTime started, bool isFinished)
    {
        return new GameSummary
        {
            Id = Guid.NewGuid(),
            HomeTeam = Guid.NewGuid().ToString(),
            AwayTeam = Guid.NewGuid().ToString(),
            Score = (home, away),
            Started = started,
            IsFinished = isFinished,
        };
    }
}