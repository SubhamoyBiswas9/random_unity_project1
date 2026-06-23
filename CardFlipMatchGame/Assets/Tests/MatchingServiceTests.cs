using NUnit.Framework;
using UnityEngine;

public class MatchingServiceTests
{
    private MatchingService matcher;

    private CardDataSO apple;
    private CardDataSO banana;

    [SetUp]
    public void Setup()
    {
        matcher = new MatchingService();

        apple = ScriptableObject.CreateInstance<CardDataSO>();
        banana = ScriptableObject.CreateInstance<CardDataSO>();
    }

    [Test]
    public void SameCardDataReference_ReturnsMatch()
    {
        MatchResult result = matcher.Evaluate(apple, apple);

        Assert.AreEqual(MatchResult.Match, result);
    }

    [Test]
    public void DifferentCardDataReferences_ReturnMismatch()
    {
        MatchResult result = matcher.Evaluate(apple, banana);

        Assert.AreEqual(MatchResult.Mismatch, result);
    }

    [Test]
    public void NullFirstCard_ReturnsInvalid()
    {
        MatchResult result = matcher.Evaluate(null, apple);

        Assert.AreEqual(MatchResult.Invalid, result);
    }

    [Test]
    public void NullSecondCard_ReturnsInvalid()
    {
        MatchResult result = matcher.Evaluate(apple, null);

        Assert.AreEqual(MatchResult.Invalid, result);
    }

    [Test]
    public void BothCardsNull_ReturnsInvalid()
    {
        MatchResult result = matcher.Evaluate(null, null);

        Assert.AreEqual(MatchResult.Invalid, result);
    }
}