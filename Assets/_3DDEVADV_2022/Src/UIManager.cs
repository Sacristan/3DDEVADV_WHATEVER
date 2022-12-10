using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Image gameWinPanel;

    private void Start()
    {
        GameManager.instance.OnScoreUpdate += UpdateScore;
        GameManager.instance.OnGameWon += GameWon;
        UpdateScore(0);
    }
    private void OnDestroy()
    {
        GameManager.instance.OnScoreUpdate -= UpdateScore;
        GameManager.instance.OnGameWon -= GameWon;
    }

    private void GameWon()
    {
        gameWinPanel.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
    }

    void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }
}
