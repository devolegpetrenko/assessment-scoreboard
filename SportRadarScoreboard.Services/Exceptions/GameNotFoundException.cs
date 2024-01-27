namespace SportRadarScoreboard.Services.Exceptions;

public class GameNotFoundException : InvalidInputException
{
    public const string ErrorMessage = "game not found";

    public GameNotFoundException() : base(ErrorMessage) { }
}