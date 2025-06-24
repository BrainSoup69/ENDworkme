
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;



#if UNITY_EDITOR
using UnityEditor;
using System.Net;
#endif

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    #region Camera Movement Variables



    public float fov = 60f;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 50f;

    #endregion

    public bool lockCursor = true;

    #region Camera Zoom Variables

    public bool enableZoom = true;
    public bool holdToZoom = false;
    public KeyCode zoomKey = KeyCode.Mouse1;
    public float zoomFOV = 30f;
    public float zoomStepTime = 5f;

    // Internal Variables
    private bool isZoomed = false;

    #endregion

    #region Movement Variables
    [Header("Movement")]
    public bool playerCanMove = true;
    public float walkSpeed = 5f;
    public float sprintSpeed = 7f;

    [HideInInspector] public bool isMoving = false;

    #endregion

    #region StatsInfo

    public KeyCode infoKey = KeyCode.Tab;
    public bool useStats = false;
    public Image stats_Information;
    public Image stats_Info;
    public FreeCamera freeCamera;

    #endregion

    #region Sprint

    public bool enableSprint = true;
    public bool unlimitedSprint = false;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float sprintDuration = 5f;
    public float sprintCooldown = .5f;
    public float sprintFOV = 80f;
    public float sprintFOVStepTime = 10f;


    public bool useSprintBar = true;
    public bool hideBarWhenFull = true;
    public Image sprintBarBG;
    public Image sprintBar;

    private CanvasGroup sprintBarCG;
    [HideInInspector] public bool isSprinting = false;
    private bool wasSprinting;
    private float sprintRemaining;
    private bool isSprintCooldown = false;
    private float sprintCooldownReset;

    #endregion

    #region Jump

    public bool enableJump = true;
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpPower = 5f;

    private bool isGrounded = false;

    #endregion

    public float speedReduction = .5f;

    private Vector3 originalScale;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Camera.main.fieldOfView = fov;
        originalScale = transform.localScale;

        if (!unlimitedSprint)
        {
            sprintRemaining = sprintDuration;
            sprintCooldownReset = sprintCooldown;
        }
    }

    void Start()
    {
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            //    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        #region Sprint Bar

        if (useSprintBar)
        {
            sprintBarCG = GetComponentInChildren<CanvasGroup>();

            sprintBarBG.gameObject.SetActive(true);
            sprintBar.gameObject.SetActive(true);

            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            if (hideBarWhenFull)
            {
                sprintBarCG.alpha = 0;
            }
        }
        else
        {
            if (sprintBarBG == null || sprintBar == null) return;
            sprintBarBG.gameObject.SetActive(false);
            sprintBar.gameObject.SetActive(false);
        }

        #endregion
    }

    float camRotation;

    private void Update()
    {

        #region Camera Zoom

        if (enableZoom)
        {
            if (Input.GetKeyDown(zoomKey) && !holdToZoom && !isSprinting)
            {
                if (!isZoomed)
                {
                    isZoomed = true;
                }
                else
                {
                    isZoomed = false;
                }
            }

            if (holdToZoom && !isSprinting)
            {
                if (Input.GetKeyDown(zoomKey))
                {
                    isZoomed = true;
                }
                else if (Input.GetKeyUp(zoomKey))
                {
                    isZoomed = false;
                }
            }

            if (isZoomed)
            {
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoomFOV, zoomStepTime * Time.deltaTime);
            }
            else if (!isZoomed && !isSprinting)
            {
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fov, zoomStepTime * Time.deltaTime);
            }
        }

        #endregion

        #region Sprint

        if (enableSprint)
        {
            if (isSprinting)
            {
                isZoomed = false;
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, sprintFOV, sprintFOVStepTime * Time.deltaTime);

                if (!unlimitedSprint)
                {
                    sprintRemaining -= 1 * Time.deltaTime;
                    if (sprintRemaining <= 0)
                    {
                        isSprinting = false;
                        isSprintCooldown = true;
                    }
                }
            }
            else
            {
                sprintRemaining = Mathf.Clamp(sprintRemaining += 1 * Time.deltaTime, 0, sprintDuration);
            }

            if (isSprintCooldown)
            {
                sprintCooldown -= 1 * Time.deltaTime;
                if (sprintCooldown <= 0)
                {
                    isSprintCooldown = false;
                }
            }
            else
            {
                sprintCooldown = sprintCooldownReset;
            }

            if (useSprintBar && !unlimitedSprint)
            {
                float sprintRemainingPercent = sprintRemaining / sprintDuration;
                sprintBar.transform.localScale = new Vector3(sprintRemainingPercent, 1f, 1f);
            }
        }

        #endregion


        #region Stats

        #endregion

        #region Jump

        if (enableJump && Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        #endregion

        HandleStatsInfo();
        CheckGround();

    }

    #region StatsInfo

    [HideInInspector] public bool isStatsVisible = false;
    private Vector3 hiddenPosition = new Vector3(-800f, 0, 0);
    private Vector3 visiblePosition = new Vector3(0, 0, 0);
    private float slideSpeed = 10f;


    public Image stats_Image1;
    public Image stats_Image2;

    private void UpdateImages()
    {

        bool showFirstImage = isStatsVisible;
        stats_Image1.enabled = showFirstImage;
        stats_Image2.enabled = !showFirstImage;
    }

    private void HandleStatsInfo()
    {
        if (Input.GetKeyDown(infoKey))
        {
            isStatsVisible = !isStatsVisible;
            UpdateImages();

            Cursor.lockState = isStatsVisible ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isStatsVisible;
            freeCamera.enabled = !isStatsVisible;
        }

        if (stats_Information != null)
        {
            stats_Information.rectTransform.anchoredPosition = Vector3.Lerp(
                stats_Information.rectTransform.anchoredPosition,
                isStatsVisible ? visiblePosition : hiddenPosition,
                Time.deltaTime * slideSpeed
            );
        }

        if (stats_Info != null)
        {
            CanvasGroup cg = stats_Info.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.alpha = isStatsVisible ? Mathf.Lerp(cg.alpha, 1, Time.deltaTime * 10) : Mathf.Lerp(cg.alpha, 0, Time.deltaTime * 10);
                cg.blocksRaycasts = isStatsVisible;
            }
        }
    }

    #endregion




    void FixedUpdate()
    {
        #region Movement

        if (playerCanMove)
        {

            float maxSpeed = 0f;
            if (MoveDirection().x != 0 || MoveDirection().z != 0 && isGrounded)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            if (enableSprint && Input.GetKey(sprintKey) && sprintRemaining > 0f && !isSprintCooldown)
            {

                isSprinting = true;

                if (useSprintBar && hideBarWhenFull && !unlimitedSprint)
                {
                    sprintBarCG.alpha += 5 * Time.deltaTime;
                }
                maxSpeed = sprintSpeed;
            }

            else
            {
                isSprinting = false;

                if (useSprintBar && hideBarWhenFull && sprintRemaining == sprintDuration)
                {
                    sprintBarCG.alpha -= 3 * Time.deltaTime;
                }

                maxSpeed = walkSpeed;
            }
            float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
            if (isMoving) rb.AddForce(MoveDirection() * currentSpeed, ForceMode.VelocityChange);

            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
            AllignToDirection(MoveDirection());
        }

        #endregion
    }
    Vector3 MoveDirection()
    {
        Vector3 newDirection;
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.Normalize();
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.Normalize();
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        newDirection = cameraRight * x + cameraForward * z;
        newDirection = new Vector3(newDirection.x, 0f, newDirection.z);
        newDirection.Normalize();
        return newDirection;
    }

    void AllignToDirection(Vector3 dir)
    {
        if (dir.magnitude > 0)
            rb.rotation = Quaternion.LookRotation(dir);
    }
    private void CheckGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float distance = .75f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            Debug.DrawRay(origin, direction * distance, Color.red);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {

        if (isGrounded)
        {
            rb.AddForce(0f, jumpPower, 0f, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(Player)), InitializeOnLoad]
public class PlayerController : Editor
{
    Player fpc;
    SerializedObject SerFPC;

    private void OnEnable()
    {
        fpc = (Player)target;
        SerFPC = new SerializedObject(fpc);
    }
}
#endif