public class MatchingService
{
    public MatchResult Evaluate(CardDataSO first, CardDataSO second)
    {
        if (first == null || second == null)
            return MatchResult.Invalid;

        return first == second
            ? MatchResult.Match
            : MatchResult.Mismatch;
    }
}