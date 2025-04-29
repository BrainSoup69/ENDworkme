using UnityEngine;

public class HeadBob : MonoBehaviour
{

    public Transform target; // The object the camera follows
    public float bobSpeed = 5f; // How fast the bobbing happens
    public float bobAmount = 0.05f; // How much it moves up and down
    public float sprintMultiplier = 1.5f; // Multiplier when sprinting

    private float defaultY; // Original local Y position
    private float bobTimer;

    // You need to set these booleans from your movement script
    public Player getPlayerScript;

    void Start()
    {
        if (target == null)
            target = transform;

        defaultY = target.localPosition.y;
    }

    void Update()
    {
        if (getPlayerScript.isMoving)
        {
            float speed = getPlayerScript.isSprinting ? bobSpeed * sprintMultiplier : bobSpeed;

            bobTimer += Time.deltaTime * speed;
            float newY = defaultY + Mathf.Sin(bobTimer) * bobAmount;
            Vector3 pos = target.localPosition;
            pos.y = newY;
            target.localPosition = pos;
        }
        else
        {
            // Reset position smoothly
            Vector3 pos = target.localPosition;
            pos.y = Mathf.Lerp(pos.y, defaultY, Time.deltaTime * 5f);
            target.localPosition = pos;
            bobTimer = 0f; // Reset timer so bobbing starts fresh
        }
    }
}


