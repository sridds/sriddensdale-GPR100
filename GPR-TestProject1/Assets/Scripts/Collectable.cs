using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private Vector2 moveRange;
    [SerializeField] private int pointValue = 50;
    [SerializeField] private float smoothTime = 0.2f;

    private Vector2 startPos;
    private Vector2 targetPos;
    private Vector2 velocity;

    private bool canCollect = true;

    public delegate void OnTouch(int points);
    public OnTouch OnTouchEvent;

    private void Start()
    {
        startPos = transform.position;
        targetPos = transform.position;
    }

    private void Update()
    {
        // smooth position to target
        transform.position = Vector2.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        if (Vector2.Distance(transform.position, targetPos) < 0.5f) canCollect = true;
        else canCollect = false;
    }

    void Touch()
    {
        // Randomize position
        targetPos = new Vector2(
            startPos.x + Random.Range(-moveRange.x, moveRange.x),
            startPos.y + Random.Range(-moveRange.y, moveRange.y));

        OnTouchEvent?.Invoke(pointValue);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!canCollect) return;

        // If the player touches the collectable, perform the Touch() function
        if (collision.GetComponent<TopDownMovement>())
        {
            Touch();
        }
    }
}
