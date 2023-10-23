using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float startTime = 60; // in seconds

    private float timer;
    private int points;

    // accessor fields
    public float TimeRemaining { get { return timer; } }
    public int Points { get { return points; } }

    private void Start()
    {
        timer = startTime;

        Collectable collectable = FindObjectOfType<Collectable>();

        // check if the collectable exists
        if (collectable != null) {
            // subscribe to collectable event to reduce coupling
            Debug.Log("Subscribed to collectable event");
            collectable.OnTouchEvent += AddPoints;
        }
    }

    // iterate timer
    private void Update()
    {
        timer -= Time.deltaTime;
    }

    private void AddPoints(int pointValue) => points += pointValue;
}
