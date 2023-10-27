
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    private bool groundedPlayer;

    public float playerSpeed;

    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float sprintSpeed = 4.0f;

    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private InputManager inputManager;
    private Transform cameraTransform;

    Vector3 move;
    Vector2 movement;

    private void OnEnable()
    {
        Events.OnGravityReverse += ChangerGravityValue;
    }

    private void OnDisable()
    {
        Events.OnGravityReverse -= ChangerGravityValue;

    }


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
        playerSpeed = walkSpeed;
        gravityValue = Physics.gravity.y;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);


        if (inputManager.PlayerJumped() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        if (inputManager.PlayerSprint() && groundedPlayer)
        {
            playerSpeed = sprintSpeed;
        }
        else
        {
            playerSpeed = walkSpeed;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        transform.forward = cameraTransform.forward;

    }

    public void OnMove(InputAction.CallbackContext value)
    {
         movement = value.ReadValue<Vector2>();
         move = new Vector3(movement.x, 0f, movement.y);
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
        playerSpeed = sprintSpeed;

        }
        else
        {
            playerSpeed = walkSpeed;
        }
    }

    void ChangerGravityValue(Vector3 newValue)
    {
        gravityValue = newValue.y;
    }
}
