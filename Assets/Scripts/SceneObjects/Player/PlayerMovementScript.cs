using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float baseMoveSpeed = 5.0f;
    public float sneakMoveSpeed = 2.5f;
    public float sprintingSpeed = 7.5f;
    public float jumpSpeed = 4.0f;
    public float gravity = 9.8f;
    public float terminalVelocity = 100f;

    private CharacterController _charCont;
    private Vector3 _moveDirection = Vector3.zero;

    void Start()
    {
        _charCont = GetComponent<CharacterController>();
    }

    void Update()
    {
        // STUB: Potentially handle movement if player is active/inactive
        if (true)
        {
            HandlePlayerMove();
        }
        else if (false)
        {
            HandlePlayerInactiveMove();
        }
    }

    private void HandlePlayerMove()
    {
        // Move direction directly from axes
        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");

        // Check if the CTRL key is held down for sneaking
        bool isSneaking = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        // Check if the SHIFT key is held down for sprinting
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Adjust movement speed based on sneaking or sprinting
        float currentMoveSpeed = baseMoveSpeed;

        if (isSneaking)
        {
            currentMoveSpeed = sneakMoveSpeed;
        }
        else if (isSprinting)
        {
            currentMoveSpeed = sprintingSpeed;
        }

        _moveDirection = new Vector3(deltaX * currentMoveSpeed, _moveDirection.y, deltaZ * currentMoveSpeed);

        // Accept jump input if grounded
        if (_charCont.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                _moveDirection.y = jumpSpeed;
            }
            else
            {
                _moveDirection.y = 0f;
            }

            // STUB: Handle movement processes, such as footsteps SFX
            if (deltaX != 0 || deltaZ != 0)
            {
                // Do handling here...
            }
        }
        else
        {
            // STUB: Handle movement stop processes, such as footsteps SFX
            // Do handling here...
        }

        ApplyMovement();
    }

    private void HandlePlayerInactiveMove()
    {
        _moveDirection = Vector3.zero;
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        _moveDirection = transform.TransformDirection(_moveDirection);
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, 
        // and once below when the moveDirection is multiplied by deltaTime). 
        // This is because gravity should be applied as an acceleration (ms^-2)
        _moveDirection.y -= this.gravity * Time.deltaTime;
        // Move the controller
        _charCont.Move(_moveDirection * Time.deltaTime);
    }
}
