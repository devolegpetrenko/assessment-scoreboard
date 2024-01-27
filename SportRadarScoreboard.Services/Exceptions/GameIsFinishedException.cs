namespace SportRadarScoreboard.Services.Exceptions;

public class GameIsFinishedException : InvalidRequestException
{
    public const string ErrorMessage = "game is finished";

    public GameIsFinishedException() : base(ErrorMessage) { }
}