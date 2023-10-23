using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float startTime = 60; // in seconds

    private float timer;

    // accessor field
    public float TimeRemaining { get; private set; }


    private void Start()
    {
        timer = startTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }
}
