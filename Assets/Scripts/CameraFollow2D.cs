using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow2D : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Axes")]
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;
    [SerializeField] private float fixedY = 0f;

    [Header("Smoothing")]
    [SerializeField] private float smoothTime = 0.2f;

    private Vector3 velocity;

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = transform.position;

        if(followX) desired.x = target.position.x;

        if(followY) desired.y = target.position.y;
        else desired.y = fixedY;

        desired.z = -10f;

        transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
    }
}
