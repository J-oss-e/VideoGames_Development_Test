using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerTopDown : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float acceleration = 40f;
    [SerializeField] private float deceleration = 50f;

    [Header("Facing")]
    [SerializeField] private bool faceMouse = false; //True for twin-stick aiming

    private Rigidbody2D rb;
    private Vector2 moveInput;
    public Vector2 FacingDirection { get; private set;} = Vector2.down;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.gravityScale = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        moveInput = new Vector2(InputHelper.Horizontal(), InputHelper.Vertical()).normalized;

        if (faceMouse)
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(InputHelper.MouseScreenPosition());
            Vector2 toMouse = (mouseWorld - transform.position);
            if(toMouse.sqrMagnitude > 0.01f)
                FacingDirection = toMouse.normalized;
        }
        else if (moveInput != Vector2.zero)
        {
            FacingDirection = moveInput;
        }

        Debug.DrawRay(transform.position, FacingDirection, Color.red);
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = moveInput * speed;
        float rate = moveInput != Vector2.zero ? acceleration : deceleration;
        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, targetVelocity, rate * Time.fixedDeltaTime);
    }
}
