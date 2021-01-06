using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed;
    public Vector3 pos;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + pos;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
