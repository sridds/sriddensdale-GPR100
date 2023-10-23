using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float _movementSpeed;
    Vector2 input;

    private bool canMove = true;
    private GameManager gameManager;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGameOver += StopMovement;
    }

    private void OnDisable()
    {
        gameManager.OnGameOver -= StopMovement;
    }

    private void Update()
    {
        if (!canMove) return;
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

    private void StopMovement()
    {
        canMove = false;
        input = Vector2.zero;
    }
}
