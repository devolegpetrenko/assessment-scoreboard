
namespace SportRadarScoreboard.Core.Games.Models;

public struct Score
{
    public Score(int home, int away)
    {
        Home = home;
        Away = away;
    }

    public int Home { get; set; }
    public int Away { get; set; }

    public int Total => Home + Away;

    public static implicit operator Score((int home, int away) input) => new Score(input.home, input.away);
}