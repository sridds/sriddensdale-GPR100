using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float _movementSpeed;
    Vector2 input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }
    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * _movementSpeed * Time.fixedDeltaTime);
    }

    private void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input.Normalize();
    }
}
