using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow2D : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 offset = Vector2.zero;

    [Header("Axes")]
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;
    [SerializeField] private float fixedY = 0f;

    [Header("Smoothing")]
    [SerializeField] private float smoothTime = 0.2f;

    [Header("Dead Zone")]
    [SerializeField] private Vector2 deadZone = Vector2.zero;

    private Vector3 velocity;

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = transform.position;
        Vector2 focus = (Vector2)target.position + offset;

        if (followX)
        {
            float dx = focus.y - transform.position.y;
            if(Mathf.Abs(dx) > deadZone.x)
                desired.x = focus.x - Mathf.Sign(dx) * deadZone.x;
        }

        if (followY)
        {
            float dy = focus.y - transform.position.y;
            if(Mathf.Abs(dy) > deadZone.y)
                desired.y = focus.y - Mathf.Sign(dy) * deadZone.y;
        }
        else desired.y = fixedY;

        desired.z = -10f;

        transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(deadZone.x * 2f, deadZone.y * 2f, 0f));
    }
}
