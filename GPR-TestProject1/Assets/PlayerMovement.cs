using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _baseSpeed = 6.0f;

    [Header("Mach Movement")]
    [SerializeField] private float _machSpeed = 12.0f;
    [SerializeField] private int _maxMachTier = 3;
    [SerializeField] private float _machTierMultiplier = 1.5f;
    [SerializeField] private float _machChangeTierTime = 1.0f;
    [SerializeField] private AnimationCurve _directionChangeSpeedOverTime;
    [SerializeField] private float _stunTime = 0.4f;

    [Header("Ground Check")]
    [SerializeField] private Vector3 _groundCheckOffset = new Vector3(0.0f, -0.5f, 0.0f);
    [SerializeField] private LayerMask _groundLayer;

    [Header("Jumping")]
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _jumpBufferTime = 0.2f;
    [SerializeField] private float _jumpHoldTime = 0.2f;
    [SerializeField] private float _coyoteTime = 0.2f;

    private Rigidbody2D rb;
    private float xInput;

    private bool stunned = false;

    // states 
    private bool isGrounded;
    private bool isJumping;
    private bool isMoving;
    private bool isMachRunning;

    // jumping
    private bool isJumpQueued;

    // jump timers
    private float jumpBufferTimer;
    private float coyoteTimer;
    private float jumpPressedTimestamp;
    private float jumpHoldTimer;

    // speed
    private float currentSpeed;
    private float targetSpeed;

    private float directionChangeTimer = 0.0f;
    private bool canSkid = false;

    private int lastDirection;
    private int direction; // 1 if right, -1 if left.

    private bool readyToResetStun = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check for ground
        isGrounded = Physics2D.OverlapCircle(transform.position + _groundCheckOffset, 0.2f, _groundLayer);

        // Get input

        if (!stunned) {
            xInput = Input.GetAxisRaw("Horizontal");
            isMoving = xInput != 0;
        }

        // set direction based on x input
        if (xInput > 0) direction = 1;
        else if (xInput < 0) direction = -1;

        TryQueueJump();
        HandleJump();
        MachRun();

        // smoothly lerp to target speed if mach running to "skid
        if(lastDirection != direction) {
            directionChangeTimer = 0.0f;
        }

        // basically whats up morning me. skid can cancel a mach run if you arent holding shift anymore. once skid to 0, mach run or normal run. skid should only happen beyond a certain threshold.
        if (isMachRunning) {
            targetSpeed = _machSpeed * direction;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * _directionChangeSpeedOverTime.Evaluate(directionChangeTimer));
        }
        else {
            currentSpeed = xInput * _baseSpeed;
        }


        if(readyToResetStun && stunned && isGrounded) {
            stunned = false;
            readyToResetStun = false;
        }

        CheckToEndMachRun();
        directionChangeTimer += Time.deltaTime;
        lastDirection = direction;
    }

    private void MachRun()
    {
        // player must be grounded and holding shift to mach run
        if (!isMachRunning && isGrounded && Input.GetKey(KeyCode.LeftShift)) {
            isMachRunning = true;
        } else if(isMachRunning && !Input.GetKey(KeyCode.LeftShift)) {
            isMachRunning = false;
        }
    }

    private void CheckToEndMachRun()
    {
        // bounce off the wall and stun
        if (Physics2D.Raycast(transform.position, Vector2.right * direction, 1f, _groundLayer) && isMachRunning)
        {
            if (isGrounded) {
                isMachRunning = false;
                stunned = true;
                Invoke(nameof(ResetStun), _stunTime);

                rb.velocity = new Vector2(3 * -direction, 6);
            }

            else {
                WallRun();
            }
        }
    }

    private void WallRun()
    {

    }

    private void ResetStun()
    {
        readyToResetStun = true;
    }

    private void FixedUpdate()
    {
        if (stunned) return;

        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
    }

    private void TryQueueJump()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            jumpBufferTimer = _jumpBufferTime;
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
        if (stunned) return false;
        if (jumpBufferTimer <= 0.0f) return false;
        if (coyoteTimer <= 0.0f) return false;
        return true;
    }

    private void HandleJump()
    {
        // Check if jump is queued. If so, jump
        if (isJumpQueued)
        {
            isJumpQueued = false;
            jumpBufferTimer = 0.0f;

            jumpPressedTimestamp = Time.time;
            jumpHoldTimer = _jumpHoldTime;
            isJumping = true;

            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
        }

        // Allows player to hold jump for a greater boost
        if (Input.GetKey(KeyCode.Z) && isJumping)
        {
            if (jumpHoldTimer > 0.0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
                jumpHoldTimer -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            isJumping = false;
        }
    }
}