using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerSidescroller : MonoBehaviour
{
    //horizontal speed in units per second
    [SerializeField] private float speed = 8f;

    private Rigidbody2D rb;
    private float horizontalInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = InputHelper.Horizontal();
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = horizontalInput * speed;
    }
}
