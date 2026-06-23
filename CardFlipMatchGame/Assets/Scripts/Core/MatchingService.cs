public class MatchingService
{
    public MatchResult Evaluate(CardController first, CardController second)
    {
        if (first == null || second == null)
            return MatchResult.Invalid;

        if (ReferenceEquals(first, second))
            return MatchResult.Invalid;

        if (first.IsMatched || second.IsMatched)
            return MatchResult.Invalid;

        return first.Data == second.Data
            ? MatchResult.Match
            : MatchResult.Mismatch;
    }
}