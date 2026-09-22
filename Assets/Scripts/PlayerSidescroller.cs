using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerSidescroller : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 14f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Feel")]
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float jumpBufferTime = 0.1f;

    private Rigidbody2D rb;
    private float horizontalInput;
    private float coyoteCounter;
    private float jumpBufferCounter;
    private bool jumpRequested;
    private bool jumpCutRequested;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = InputHelper.Horizontal();

        if(IsGrounded()) 
            coyoteCounter = coyoteTime;
        else 
            coyoteCounter -= Time.deltaTime;

        if(InputHelper.JumpPressed())
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        if(jumpBufferCounter > 0f && coyoteCounter > 0f)
        {
          jumpRequested = true;
          jumpBufferCounter = 0f;
          coyoteCounter = 0f;  
        } 

        if(InputHelper.JumpReleased()) jumpCutRequested = true;
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = horizontalInput * speed;

        if (jumpRequested)
        {
            rb.linearVelocityY = jumpForce;
            jumpRequested = false;
        }

        if (jumpCutRequested)
        {
            if (rb.linearVelocityY > 0f) rb.linearVelocityY *= 0.5f;
            jumpCutRequested = false;
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        if(groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
