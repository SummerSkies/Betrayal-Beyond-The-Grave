using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    //[SerializeField] private string look = "Look";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string crouch = "Crouch";
    [SerializeField] private string sprint = "Sprint";

    private InputAction moveAction;
    //private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction crouchAction;
    private InputAction sprintAction;

    public Vector2 MoveInput { get; private set; }
    //public Vector2 LookInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public bool CrouchTriggered { get; private set; }
    public bool SprintTriggered { get; private set; }

    public static PlayerInputManager Instance { get; private set; }

    private void Awake()
    {
        //Only ever have one version of this game object at a time
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //Get and set all actions
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        //lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        crouchAction = playerControls.FindActionMap(actionMapName).FindAction(crouch);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);

        //Sets input action references in order to check input values at any time
        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        //Look at weather an action is performed at any time, use context method to look for input, then read the exact value of said input.
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        //lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        jumpAction.performed += context => JumpTriggered = true;
        crouchAction.performed += context => CrouchTriggered = true;
        sprintAction.performed += context => SprintTriggered = true;

        //If an action is cancelled, nothing should happen
        moveAction.canceled += context => MoveInput = Vector2.zero;
        //lookAction.canceled += context => LookInput = Vector2.zero;
        jumpAction.canceled += context => JumpTriggered = false;
        crouchAction.canceled += context => CrouchTriggered = false;
        sprintAction.canceled += context => SprintTriggered = false;
    }

    private void OnEnable()
    {
        //Enable all actions on enable
        moveAction.Enable();
        //lookAction.Enable();
        jumpAction.Enable();
        crouchAction.Enable();
        sprintAction.Enable();
    }

    private void OnDisable()
    {
        //Disable all actions on disable
        moveAction.Disable();
        //lookAction.Disable(); 
        jumpAction.Disable();
        crouchAction.Disable();
        sprintAction.Disable();
    }
}