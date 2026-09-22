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

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool jumpRequested;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = InputHelper.Horizontal();

        if(InputHelper.JumpPressed() && IsGrounded()) jumpRequested = true;
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = horizontalInput * speed;

        if (jumpRequested)
        {
            rb.linearVelocityY = jumpForce;
            jumpRequested = false;
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
