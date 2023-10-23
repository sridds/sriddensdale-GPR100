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

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        // Guard clause just in case the manager failed
        if (manager == null) return;

        timerText.text = $"Time Remaining: {(int)manager.TimeRemaining}";
        pointsText.text = $"Points: {manager.Points}";
    }
}
