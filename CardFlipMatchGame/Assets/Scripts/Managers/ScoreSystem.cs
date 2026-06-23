using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    ScoreCalculator calculator = new();

    public int score => calculator.Score;

    public int comboCount => calculator.Combo;

    MatchHandler matchHandler;

    public event System.Action<int,int> OnScoreChanged;

    public void Init(MatchHandler matchHandler)
    {
        this.matchHandler = matchHandler;

        calculator = new ScoreCalculator();

        matchHandler.OnPairEvaluated += OnPairEvaluated;
    }

    void OnPairEvaluated(bool isMatch)
    {
        if (isMatch)
        {
            calculator.RegisterMatch();

            Debug.Log($"Match! Combo: {comboCount}, Score: {score}");
        }
        else
        {
            calculator.RegisterMismatch();
        }

        OnScoreChanged?.Invoke(score, comboCount);
    }

    public void SetScore(int score)
    {
        calculator.SetScore(score);
    }

    void OnDisable()
    {
        if (matchHandler != null)
            matchHandler.OnPairEvaluated -= OnPairEvaluated;
    }
}
