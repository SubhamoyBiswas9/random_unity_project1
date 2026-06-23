using NUnit.Framework;

public class ScoreCalculatorTests
{
    ScoreCalculator calculator;

    [SetUp]
    public void Setup()
    {
        calculator = new ScoreCalculator();
    }

    [Test]
    public void StartsWithZeroScore()
    {
        Assert.AreEqual(0, calculator.Score);
        Assert.AreEqual(0, calculator.Combo);
    }

    [Test]
    public void FirstMatchGivesTenPoints()
    {
        calculator.RegisterMatch();

        Assert.AreEqual(10, calculator.Score);
        Assert.AreEqual(1, calculator.Combo);
    }

    [Test]
    public void ConsecutiveMatchesIncreaseCombo()
    {
        calculator.RegisterMatch();
        calculator.RegisterMatch();

        Assert.AreEqual(30, calculator.Score);
        Assert.AreEqual(2, calculator.Combo);
    }

    [Test]
    public void ThirdMatchAddsCorrectBonus()
    {
        calculator.RegisterMatch();
        calculator.RegisterMatch();
        calculator.RegisterMatch();

        Assert.AreEqual(55, calculator.Score);
        Assert.AreEqual(3, calculator.Combo);
    }

    [Test]
    public void MismatchResetsCombo()
    {
        calculator.RegisterMatch();
        calculator.RegisterMatch();

        calculator.RegisterMismatch();

        Assert.AreEqual(0, calculator.Combo);
    }

    [Test]
    public void SetScoreRestoresScore()
    {
        calculator.SetScore(150);

        Assert.AreEqual(150, calculator.Score);
    }
}