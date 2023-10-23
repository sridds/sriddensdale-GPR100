using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private Vector2 moveRange;
    [SerializeField] private int pointValue = 50;

    private Vector2 startPos;

    public delegate void OnTouch(int points);
    public OnTouch OnTouchEvent;

    private void Start()
    {
        startPos = transform.position;
    }

    void Touch()
    {
        // Randomize position
        transform.position = new Vector2(
            startPos.x + Random.Range(-moveRange.x, moveRange.x),
            startPos.y + Random.Range(-moveRange.y, moveRange.y));

        OnTouchEvent?.Invoke(pointValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player touches the collectable, perform the Touch() function
        if (collision.GetComponent<TopDownMovement>())
        {
            Touch();
        }
    }
}
