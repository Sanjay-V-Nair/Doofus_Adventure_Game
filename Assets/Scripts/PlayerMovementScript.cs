using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    private InputAction action;
    private PlayerInput playerInput;
    int isRunningHash;

    [SerializeField]float playerSpeed = 1f;

    Animator animator;


    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        action = playerInput.actions.FindAction("Move");
        animator = GetComponentInChildren<Animator>();
        isRunningHash = Animator.StringToHash("isRunning");
    }

    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool forwardPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");
        if (action != null) {
            Move();
        }
        if (!isRunning && forwardPressed) {
            animator.SetBool(isRunningHash, true);
        }
        if (isRunning && !forwardPressed) {
            animator.SetBool(isRunningHash, false);
        }
    }


    private void Move() {
        Vector2 movdir = action.ReadValue<Vector2>();
        transform.position += new Vector3(movdir.x,0, movdir.y) * playerSpeed * Time.deltaTime;
        HandleRotation(movdir);
    }

    private void HandleRotation(Vector2 mov) {
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = new Vector3(mov.x, 0, mov.y);
        Vector3 positionToLookAt = currentPosition + newPosition;
        transform.LookAt(positionToLookAt);
    }

}
