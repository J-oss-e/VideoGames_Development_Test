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

    [Header("Look Ahead")]
    [SerializeField] private float lookAheadDistance = 0f;
    [SerializeField] private float lookAheadSmooth = 0.5f;

    [Header("Bounds")]
    [SerializeField] private BoxCollider2D levelBounds;

    private Vector3 velocity;
    private Vector3 lastTargetPos;
    private Vector2 currentLookAhead;
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (target != null) lastTargetPos = target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = transform.position;

        Vector3 targetDelta = target.position - lastTargetPos;
        lastTargetPos = target.position;
        Vector2 lookGoal = Vector2.zero;
        if(lookAheadDistance > 0f && targetDelta.sqrMagnitude > 0.0001f)
            lookGoal = ((Vector2)targetDelta).normalized * lookAheadDistance;
        currentLookAhead = Vector2.Lerp(currentLookAhead, lookGoal, Time.deltaTime / Mathf.Max(lookAheadSmooth, 0.01f));

        Vector2 focus = (Vector2)target.position + offset + currentLookAhead;

        if (followX)
        {
            float dx = focus.x - transform.position.x;
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

        if (levelBounds != null)
            desired = ClampToBounds(desired);

        desired.z = -10f;

        transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
    }

    private Vector3 ClampToBounds(Vector3 pos)
    {
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        Bounds b = levelBounds.bounds;
        float minX = b.min.x + halfWidth;
        float maxX = b.max.x - halfWidth;
        float minY = b.min.y + halfHeight;
        float maxY = b.max.y - halfHeight;

        pos.x = minX > maxX ? b.center.x : Mathf.Clamp(pos.x, minX, maxX);
        pos.y = minY > maxY ? b.center.y : Mathf.Clamp(pos.y, minY, maxY);
        return pos;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(deadZone.x * 2f, deadZone.y * 2f, 0f));
    }
}
