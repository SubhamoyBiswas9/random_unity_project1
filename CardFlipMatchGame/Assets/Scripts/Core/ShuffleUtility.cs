using System;
using System.Collections.Generic;

public static class ShuffleUtility
{
    public static void Shuffle<T>(List<T> list, int seed)
    {
        System.Random random = new(seed);

        for (int i = 0; i < list.Count; i++)
        {
            int rand = random.Next(i, list.Count);

            (list[i], list[rand]) =
            (list[rand], list[i]);
        }
    }
}