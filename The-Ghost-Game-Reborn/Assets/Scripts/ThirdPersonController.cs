using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float moveSpeed = 5.0f; //was 3.0f
    [SerializeField] private float sprintMultiplier = 1.75f; //was 2.0f
    [SerializeField] private float crouchMultiplier = 0.5f; //was 0.5f

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5.0f; //was 5.0f
    [SerializeField] private float gravity = 12.81f; //was 9.81f

    
    [Header("Crouch Parameters")]
    [SerializeField] private float controllerCrouchedHeight; //Character controller height while crouched
    [SerializeField] private float controllerCrouchedCenter; //Character controller center while crouched

    //Adjusted character controller values
    private float controllerSkinWidth;
    private float asjustedHeight;
    private float adjustedCenter;
    private float adjustedCrouchedHeight;

    //Object references
    private CharacterController characterController;
    private PlayerInputManager playerInputManager;
    private Camera mainCamera;

    private Vector3 currentMovement;
    private float rotationLock = 0;

    private void Awake()
    {
        //Access the character controller, camera, and input manager instance
        characterController = GetComponent<CharacterController>();
        playerInputManager = PlayerInputManager.Instance;
        mainCamera = Camera.main;
    }

    private void Start()
    {
        ResizeCharacterController();
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleCrouching();
    }


    //Adjusts character controller values to make the character visually appear to be touching the ground without changing skin width values.
    void ResizeCharacterController()
    {
        /// <summary>
        /// To set up your Character Controller:
        /// 1. Set the height and radius, and center to be half of height. Make sure your character's y position is 0.
        /// 2. Subtract skinwidth from the height and then move the center to be half of the skin width up.
        /// Your character will appear exactly on the ground in playmode without having to reduce skin width.
        /// For crouching, set the height to height - (skinwidth * 2) and leave the center as is.
        /// </summary>\
        
        controllerSkinWidth = characterController.skinWidth; //should equal 0.08

        asjustedHeight = characterController.height - controllerSkinWidth; //should equal 1.72
        adjustedCenter = characterController.center.y + (controllerSkinWidth / 2); //should equal 0.25
        adjustedCrouchedHeight = controllerCrouchedHeight - (controllerSkinWidth * 2);
    }

    void HandleMovement()
    {
        float speed;
        
        //Sets player speed
        if (playerInputManager.CrouchTriggered)
        {
            speed = moveSpeed * crouchMultiplier;
        }
        else if (playerInputManager.SprintTriggered)
        {
            speed = moveSpeed * sprintMultiplier;
        }
        else
        {
            speed = moveSpeed;
        }

        //Transform player direction to world space, normalize to prevent speed from getting too high
        Vector3 inputDirection = new Vector3(playerInputManager.MoveInput.x, 0.0f, playerInputManager.MoveInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        worldDirection.Normalize();

        //Apply speed to direction
        currentMovement.x = worldDirection.x * speed;
        currentMovement.z = worldDirection.z * speed;

        HandleJumping();

        //Move character
        characterController.Move(currentMovement * Time.deltaTime);
    }

    void HandleJumping()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = -0.5f;

            if (playerInputManager.JumpTriggered && !playerInputManager.CrouchTriggered)
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }
    }

    void HandleRotation()
    {
        //Rotate player with camera
        Quaternion cameraDirection = mainCamera.transform.localRotation;
        transform.rotation = Quaternion.Euler(rotationLock, cameraDirection.eulerAngles.y, rotationLock);
    }

    void HandleCrouching()
    {
        if (playerInputManager.CrouchTriggered)
        {
            characterController.height = adjustedCrouchedHeight;
            characterController.center = new Vector3(characterController.center.x, controllerCrouchedCenter, characterController.center.z);
        }
        else
        {
            characterController.height = asjustedHeight;
            characterController.center = new Vector3(characterController.center.x, adjustedCenter, characterController.center.z);
        }
    }
}
