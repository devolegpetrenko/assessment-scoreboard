using FluentAssertions;
using Moq;
using SportRadarScoreboard.Core.Games;
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
    public void StartGame_ValidTeamNames_OutputValidId()
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
}