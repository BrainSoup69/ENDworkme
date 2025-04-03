using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float rotationSpeed = 3f;
    public float height = 2f;
    private float currentYaw = 0f;
    private float currentPitch = 10f;
    public Vector2 pitchLimits = new Vector2(-20, 60);

    private float yaw = 0f;
    private float pitch = 0f;

    void Start()
    {
        if (target != null)
        {
            transform.position = target.position - transform.forward * distance;
        }
    }

    void Update()
    {
        if (target == null) return;

        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        currentYaw += mouseX;
        currentPitch -= mouseY;
        currentPitch = Mathf.Clamp(currentPitch, -10f, 80f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 direction = rotation * Vector3.back * distance;
        transform.position = target.position + direction;
        transform.LookAt(target);

        transform.LookAt(target.position + Vector3.up * height);
    }
    void LateUpdate()
    {
        if (target == null) return;

        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeed;
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
            pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);
        }
    }
}
