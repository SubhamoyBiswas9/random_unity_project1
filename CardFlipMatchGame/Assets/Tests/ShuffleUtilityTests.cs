using NUnit.Framework;
using System.Collections.Generic;

public class ShuffleUtilityTests
{
    [Test]
    public void SameSeedProducesSameShuffle()
    {
        List<int> first = new() { 1, 2, 3, 4, 5, 6, 7, 8 };
        List<int> second = new() { 1, 2, 3, 4, 5, 6, 7, 8 };

        ShuffleUtility.Shuffle(first, 1234);
        ShuffleUtility.Shuffle(second, 1234);

        CollectionAssert.AreEqual(first, second);
    }

    [Test]
    public void DifferentSeedsProduceDifferentShuffle()
    {
        List<int> first = new() { 1, 2, 3, 4, 5, 6, 7, 8 };
        List<int> second = new() { 1, 2, 3, 4, 5, 6, 7, 8 };

        ShuffleUtility.Shuffle(first, 10);
        ShuffleUtility.Shuffle(second, 999);

        CollectionAssert.AreNotEqual(first, second);
    }
}