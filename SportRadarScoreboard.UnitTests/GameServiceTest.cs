using FluentAssertions;
using Moq;
using SportRadarScoreboard.Core.Games;
using SportRadarScoreboard.Core.Games.Models;
using SportRadarScoreboard.Services;
using SportRadarScoreboard.Services.Exceptions;

namespace SportRadarScoreboard.UnitTests;

public class GameServiceTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void StartGame_ValidTeamNames_OutputValidGameId()
    {
        //arrange
        var homeTeam = Guid.NewGuid().ToString();
        var awayTeam = Guid.NewGuid().ToString();

        var gameRepository = new Mock<IGameRepository>(MockBehavior.Strict);
        gameRepository.Setup(x => x.IsGameInProgress(It.Is<string>(x => x == homeTeam), It.Is<string>(x => x == awayTeam))).Returns(false);
        gameRepository.Setup(x => x.AddGame(It.Is<string>(x => x == homeTeam), It.Is<string>(x => x == awayTeam))).Returns(Guid.NewGuid());

        var sut = new GameService(gameRepository.Object);

        //act
        var gameId = sut.StartGame(homeTeam, awayTeam);

        //assert
        gameId.Should().NotBeEmpty();
    }

    [Test]
    public void StartGame_InvalidHomeTeamName_ThrowError()
    {
        //arrange
        var gameRepository = Mock.Of<IGameRepository>(MockBehavior.Strict);

        var sut = new GameService(gameRepository);

        var homeTeam = " ";
        var awayTeam = Guid.NewGuid().ToString();

        //act
        var action = () => sut.StartGame(homeTeam, awayTeam);

        //assert
        action.Should().Throw<InvalidInputException>().WithMessage("invalid home team name");
    }

    [Test]
    public void StartGame_InvalidAwayTeamName_ThrowError()
    {
        //arrange
        var gameRepository = Mock.Of<IGameRepository>(MockBehavior.Strict);

        var sut = new GameService(gameRepository);

        var homeTeam = Guid.NewGuid().ToString();
        var awayTeam = " ";

        //act
        var action = () => sut.StartGame(homeTeam, awayTeam);

        //assert
        action.Should().Throw<InvalidInputException>().WithMessage("invalid away team name");
    }

    [Test]
    public void StartGame_GameStartedOneMoreTimeBeforeFinished_ThrowError()
    {
        //arrange
        var homeTeam = Guid.NewGuid().ToString();
        var awayTeam = Guid.NewGuid().ToString();

        var gameRepository = new Mock<IGameRepository>(MockBehavior.Strict);
        gameRepository.SetupSequence(x => x.IsGameInProgress(It.Is<string>(x => x == homeTeam), It.Is<string>(x => x == awayTeam)))
            .Returns(false)
            .Returns(true);
        gameRepository.Setup(x => x.AddGame(It.Is<string>(x => x == homeTeam), It.Is<string>(x => x == awayTeam))).Returns(Guid.NewGuid());

        var sut = new GameService(gameRepository.Object);

        sut.StartGame(homeTeam, awayTeam);

        //act
        var action = () => sut.StartGame(homeTeam, awayTeam);

        //assert
        action.Should().Throw<InvalidRequestException>().WithMessage("game is in progress");
    }

    [Test]
    public void StartGame_GameStartedAfterPreviousFinished_NewGameCreated()
    {
        //arrange
        var homeTeam = Guid.NewGuid().ToString();
        var awayTeam = Guid.NewGuid().ToString();

        var gameRepository = new Mock<IGameRepository>(MockBehavior.Strict);
        gameRepository.SetupSequence(x => x.IsGameInProgress(It.Is<string>(x => x == homeTeam), It.Is<string>(x => x == awayTeam)))
            .Returns(false)
            .Returns(false);
        gameRepository.Setup(x => x.AddGame(It.Is<string>(x => x == homeTeam), It.Is<string>(x => x == awayTeam))).Returns(Guid.NewGuid());

        var sut = new GameService(gameRepository.Object);

        sut.StartGame(homeTeam, awayTeam);

        //act
        var gameId = sut.StartGame(homeTeam, awayTeam);

        //assert
        gameId.Should().NotBeEmpty();
    }

    [Test]
    public void UpdateScore_ExistingGame_ChangeGameScoreRequested()
    {
        //arrange
        var gameId = Guid.NewGuid();
        var homeScore = 2;
        var awayScore = 5;

        var gameRepository = new Mock<IGameRepository>(MockBehavior.Strict);
        gameRepository.Setup(x => x.GetGameDetails(It.Is<Guid>(x => x == gameId))).Returns(new GameDetails
        {
            Id = gameId,
            IsFinished = false,
        });
        gameRepository.Setup(x => x.ChangeGameScore(It.Is<Guid>(x => x == gameId), It.Is<int>(x => x == homeScore), It.Is<int>(x => x == awayScore))).Verifiable();

        var sut = new GameService(gameRepository.Object);

        //act
        sut.UpdateScore(gameId, homeScore, awayScore);

        //assert
        gameRepository.Verify(x => x.ChangeGameScore(It.Is<Guid>(x => x == gameId), It.Is<int>(x => x == homeScore), It.Is<int>(x => x == awayScore)), Times.Once);
    }

    [Test]
    public void UpdateScore_NotExistingGame_ThrowError()
    {
        //arrange
        var gameId = Guid.NewGuid();
        var homeScore = 2;
        var awayScore = 5;

        var gameRepository = new Mock<IGameRepository>(MockBehavior.Strict);
        gameRepository.Setup(x => x.GetGameDetails(It.Is<Guid>(x => x == gameId))).Returns(value: null);
        gameRepository.Setup(x => x.ChangeGameScore(It.Is<Guid>(x => x == gameId), It.Is<int>(x => x == homeScore), It.Is<int>(x => x == awayScore))).Verifiable();

        var sut = new GameService(gameRepository.Object);

        //act
        var action = () => sut.UpdateScore(gameId, homeScore, awayScore);

        //assert
        action.Should().Throw<InvalidInputException>().WithMessage("invalid game id");
        gameRepository.Verify(x => x.ChangeGameScore(It.Is<Guid>(x => x == gameId), It.Is<int>(x => x == homeScore), It.Is<int>(x => x == awayScore)), times: Times.Never);
    }
}