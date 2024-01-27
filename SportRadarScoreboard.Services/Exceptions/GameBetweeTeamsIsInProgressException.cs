namespace SportRadarScoreboard.Services.Exceptions;

public class GameBetweeTeamsIsInProgressException : InvalidRequestException
{
    public const string ErrorMessage = "game between teams is in progress";

    public GameBetweeTeamsIsInProgressException() : base(ErrorMessage) { }
}