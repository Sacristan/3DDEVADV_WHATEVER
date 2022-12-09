using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private void Start()
    {
        GameManager.instance.OnScoreUpdate += UpdateScore;
        UpdateScore(0);
    }

    private void OnDestroy()
    {
        GameManager.instance.OnScoreUpdate -= UpdateScore;
    }

    void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }
}
