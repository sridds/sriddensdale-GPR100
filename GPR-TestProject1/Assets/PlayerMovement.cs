using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _baseSpeed;

    [Header("Ground Check")]
    [SerializeField] private Vector3 _groundCheckOffset = new Vector3(0.0f, -0.5f, 0.0f);
    [SerializeField] private LayerMask _groundLayer;

    [Header("Jumping")]
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _jumpBufferTime = 0.2f;
    [SerializeField] private float _coyoteTime = 0.2f;

    private Rigidbody2D rb;
    private float xInput;
    private bool isGrounded;
    private bool isJumpQueued;

    private float jumpBufferTimer;
    private float coyoteTimer;
    private float jumpPressedTimestamp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check for ground
        isGrounded = Physics2D.OverlapCircle(transform.position + _groundCheckOffset, 0.2f, _groundLayer);

        // Get input
        xInput = Input.GetAxisRaw("Horizontal");

        TryQueueJump();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(xInput * _baseSpeed, rb.velocity.y);

        if (isJumpQueued)
        {
            isJumpQueued = false;
            jumpBufferTimer = 0.0f;

            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            rb.AddForce(new Vector2(0.0f, _jumpForce), ForceMode2D.Impulse);
        }
    }

    private void TryQueueJump()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            jumpBufferTimer = _jumpBufferTime;
            jumpPressedTimestamp = Time.time;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }

        if (isGrounded)
        {
            coyoteTimer = _coyoteTime;
        }
        else
        {
            //
            coyoteTimer -= Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Z) && rb.velocity.y > 0.0f)
        {
            coyoteTimer = 0.0f;
        }

        if (CanJump()) isJumpQueued = true;
    }

    private bool CanJump()
    {
        if (jumpBufferTimer <= 0.0f) return false;
        if (coyoteTimer <= 0.0f) return false;
        return true;
    }
}