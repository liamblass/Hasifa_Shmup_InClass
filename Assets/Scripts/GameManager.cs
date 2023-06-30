using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnScoreUpdated;

    private int score;
    private int highScore;

    [SerializeField] private int pointsPerEnemy = 10;

    private void Awake()
    {
        Health.OnEnemyDead += UpdateScore;
    }

    private void OnDestroy()
    {
        Health.OnEnemyDead -= UpdateScore;
    }

    private void UpdateScore()
    {
        score += pointsPerEnemy;
        OnScoreUpdated?.Invoke(score);
    }
}
