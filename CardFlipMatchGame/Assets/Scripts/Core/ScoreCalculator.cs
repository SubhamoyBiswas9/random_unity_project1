public class ScoreCalculator
{
    public int Score { get; private set; }

    public int Combo { get; private set; }

    public void RegisterMatch()
    {
        Combo++;

        int basePoints = 10;

        int comboBonus = Combo > 1
            ? Combo * 5
            : 0;

        Score += basePoints + comboBonus;
    }

    public void RegisterMismatch()
    {
        Combo = 0;
    }

    public void SetScore(int score)
    {
        Score = score;
    }
}