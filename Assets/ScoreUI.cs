using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TMP_Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();

        GameManager.OnScoreUpdated += UpdateScoreText;
    }

    private void OnDestroy()
    {
        GameManager.OnScoreUpdated -= UpdateScoreText;

    }

    private void UpdateScoreText(int num)
    {
        scoreText.text = num.ToString();
    }
}
