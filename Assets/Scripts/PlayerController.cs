using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("The three numbers")]

    public float moveSpeed = 8f; // ground speed, units/second
    public float jumpSpeed = 14f; // initial upward speed
    public float airControl = 0.5f; // steering kept mid-air, 0..1

    [Header("Ground check")]

    public Transform groundCheck;
    public float groundRadius = 0.15f;
    public LayerMask groundLayer;
    Rigidbody2D rb;
    Vector2 moveInput;
    bool grounded;

    void Awake() { rb = GetComponent<Rigidbody2D>(); }
    
    void OnMove(InputValue value) { moveInput = value.Get<Vector2>(); }
    
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position,
        groundRadius, groundLayer);
        float targetX = moveInput.x * moveSpeed;
        if (grounded)
            rb.linearVelocity = new Vector2(targetX, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(
            Mathf.Lerp(rb.linearVelocity.x, targetX, airControl),
            rb.linearVelocity.y);
    }

    void OnJump()
    {
        if (grounded)
            rb.linearVelocity =
            new Vector2(rb.linearVelocity.x, jumpSpeed);
    }
}