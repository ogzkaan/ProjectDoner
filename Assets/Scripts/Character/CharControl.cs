using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using static Models;
using static UnityEngine.Timeline.AnimationPlayableAsset;

public class CharMovement : MonoBehaviour
{
    public UnityEngine.CharacterController controller;
    private PlayerInput playerinput;

    private Vector3 NewCameraRotation;
    private Vector3 NewCharRotation;


    [Header("Ref")]
    public Transform cameraHolder;
    public Transform playerCamera;

    [Header("Settings")]
    public PlayerSettingsModel playerSettings;

    [Header("Gravity")]
    public float playerGravity;
    public float minGravity;
    public float gravityAmount;
    
    private Vector2 raw_Movement;
    private Vector2 raw_Look;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 1000;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Awake()
    {
        playerinput = new PlayerInput();

        playerinput.Player.Movement.performed += ctx => raw_Movement = ctx.ReadValue<Vector2>();
        playerinput.Player.Look.performed += ctx => raw_Look = ctx.ReadValue<Vector2>();

        playerinput.Enable();

        NewCameraRotation = new Vector3(0,0,0);
        NewCharRotation = new Vector3(0, 0, 0);

        controller = GetComponent<UnityEngine.CharacterController>();

    }
    private void Update()
    {
        CalculateMovement();
        CalculateLook();
    }
    private void CalculateMovement()
    {
        var verticaldSpeed = playerSettings.walkSpeed * raw_Movement.x*Time.deltaTime;
        var horizontalSpeed = playerSettings.walkSpeed * raw_Movement.y * Time.deltaTime;

        var newMovementSpeed = new Vector3(verticaldSpeed, 0, horizontalSpeed);

        newMovementSpeed=transform.TransformDirection(newMovementSpeed);


        if (playerGravity > minGravity)
        {
            playerGravity -= gravityAmount * Time.deltaTime;
        }

        if (playerGravity < -1 && controller.isGrounded)
        {
            playerGravity = -1;
        }
        newMovementSpeed.y -= playerGravity;
        controller.Move(newMovementSpeed);
        

    }
    private void CalculateLook()
    {
        NewCharRotation.y += playerSettings.xLookSens * raw_Look.x * Time.deltaTime;
        transform.localRotation= Quaternion.Euler(NewCharRotation);

        NewCameraRotation.x -= playerSettings.yLookSens * raw_Look.y * Time.deltaTime;
        NewCameraRotation.x = Mathf.Clamp(NewCameraRotation.x, -85f, 85f);

        cameraHolder.localRotation = Quaternion.Euler(NewCameraRotation);
    }


}


