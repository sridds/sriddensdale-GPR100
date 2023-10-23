using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private Vector2 moveRange;
    [SerializeField] private int pointValue = 50;
    [SerializeField] private float smoothTime = 0.2f;

    [SerializeField] private AudioClip collectClip;
    [SerializeField] private AudioSource source;

    [SerializeField] private Animator animator;

    private Vector2 startPos;
    private Vector2 targetPos;
    private Vector2 velocity;

    private bool canCollect = true;

    public delegate void OnTouch(int points);
    public OnTouch OnTouchEvent;

    private void Start()
    {
        startPos = Vector2.zero;
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
        SetNewPosition();

        animator.SetTrigger("Collect");

        OnTouchEvent?.Invoke(pointValue);

        // Randomize pitch for fun
        source.pitch = Random.Range(0.94f, 1.08f);
        source.PlayOneShot(collectClip);
    }

    void SetNewPosition()
    {
        bool isValid = false;
        do
        {
            Vector2 newPos = new Vector2(
            startPos.x + Random.Range(-moveRange.x, moveRange.x),
            startPos.y + Random.Range(-moveRange.y, moveRange.y));

            if(Vector2.Distance(newPos, transform.position) > 4.0f) {
                isValid = true;

                targetPos = newPos;
            }
        } while (!isValid);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!canCollect) return;

        // If the player touches the collectable, perform the Touch() function
        if (collision.GetComponent<TopDownMovement>())
        {
            Touch();
            canCollect = false;
        }
    }
}
