using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    private GameManager manager;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI pointsText;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI finalPoints;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();

        manager.OnGameOver += ShowGameOver;
    }

    private void OnDisable()
    {
        manager.OnGameOver -= ShowGameOver;
    }

    private void Update()
    {
        // Guard clause just in case the manager failed
        if (manager == null) return;

        timerText.text = $"Time Remaining: {(int)manager.TimeRemaining}";

        // change the text to red if the time is low;
        if ((int)manager.TimeRemaining <= 10)
            timerText.color = Color.red;

        pointsText.text = $"Points: {manager.Points}";
    }

    private void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
        finalPoints.text = $"Final Points: {manager.Points}";
    }
}
