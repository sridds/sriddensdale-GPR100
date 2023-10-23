using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float startTime = 60; // in seconds

    private float timer;
    private int points;
    private bool isGameOver;

    // accessor fields
    public float TimeRemaining { get { return timer; } }
    public int Points { get { return points; } }

    public delegate void GameOver();
    public GameOver OnGameOver;

    private Collectable collectable;

    private void Start()
    {
        timer = startTime;

        collectable = FindObjectOfType<Collectable>();

        // check if the collectable exists
        if (collectable != null) {
            // subscribe to collectable event to reduce coupling
            Debug.Log("Subscribed to collectable event");
            collectable.OnTouchEvent += AddPoints;
        }
    }

    private void OnDisable()
    {
        collectable.OnTouchEvent -= AddPoints;
    }

    private void Update()
    {
        // iterate timer 
        if(timer > 0) timer -= Time.deltaTime;

        // Invoke game over
        if(timer <= 0 && !isGameOver) {
            isGameOver = true;
            OnGameOver?.Invoke();
        }

        // Restart scene when game over
        if (isGameOver && Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void AddPoints(int pointValue) => points += pointValue;
}
