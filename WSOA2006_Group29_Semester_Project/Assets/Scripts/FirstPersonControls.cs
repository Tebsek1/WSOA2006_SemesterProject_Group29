using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonControls : MonoBehaviour
{
    [SerializeField]

    [Header("MOVEMENT SETTINGS")]
    [Space(5)]
    // Public variables to set movement and look speed, and the player camera
    public float moveSpeed; // Speed at which the player moves
    public float lookSpeed; // Sensitivity of the camera movement
    public float gravity = -9.81f; // Gravity value
    public float jumpHeight = 1.0f; // Height of the jump
    public Transform playerCamera; // Reference to the player's camera
                                   // Private variables to store input values and the character controller
    private Vector2 moveInput; // Stores the movement input from the player
    private Vector2 lookInput; // Stores the look input from the player
    private float verticalLookRotation = 0f; // Keeps track of vertical camera rotation for clamping
    private Vector3 velocity; // Velocity of the player
    private CharacterController characterController; // Reference to the CharacterController component

    [Header("SHOOTING SETTINGS")]
    [Space(5)]
    public GameObject projectilePrefab; // Projectile prefab for shooting
    public Transform firePoint; // Point from which the projectile is fired
    public float projectileSpeed = 20f; // Speed at which the projectile is fired

    [Header("PICKING UP SETTINGS")]
    [Space(5)]
    public Transform holdPosition; // Position where the picked-up object will be held
    private GameObject heldObject; // Reference to the currently held object
    public float pickUpRange = 3f; // Range within which objects can be picked up
    private bool holdingGun = true;

    [Header("CROUCH SETTINGS")]
    [Space(5)]
    public float crouchHeight = 1f; //make short
    public float standingHeight = 2f; //make normal
    public float crouchSpeed = 1.5f; //short speed
    public bool isCrouching = false; //if short or normal

    [Header("SPRINT SETTINGS")]
    [Space(5)]
    public float sprintSpeed;
    public bool isSprinting = false;

    [Header("SLIDE SETTINGS")]
    [Space(5)]
    public float slideSpeed = 10f;
    public float slideDuration = 1f;
    public float slideTime;
    public bool isSliding = false;

    [Header("CLIMBING SETTINGS")]
    [Space(5)]
    public float climbSpeed = 3f; // Speed at which the player climbs
    public bool isClimbing = false; // Track whether the player is currently climbing
    public float climbDetectionRange = 3f; // Increased range to 3f for better detection
    public LayerMask climbableLayer; // Define which layer to detect for climbing
    public float climbTriggerDistance = 1.5f; // Distance to check for proximity to climbable object

    private bool canClimb = false; // Flag to check if the player can climb
    private float climbCooldown = 0.1f; // Cooldown period to avoid rapid state changes
    private float lastClimbChangeTime = 0f; // Last time the climbing state was changed
    Vector3 standstill = Vector3.zero;


    [Header("FOV SETTINGS")]
    [Space(5)]
    public Camera playerCameraComponent; // Reference to the Camera component
    public float normalFOV = 60f; // Default FOV
    public float sprintFOV = 80f; // FOV while sprinting
    public float fovTransitionSpeed = 5f; // Speed of FOV transition



    private void Awake()
    {
        // Get and store the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        // Create a new instance of the input actions
        var playerInput = new Controls();

        // Enable the input actions
        playerInput.Movement.Enable();

        // Subscribe to the movement input events
        playerInput.Movement.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed
        playerInput.Movement.Movement.canceled += ctx => moveInput = Vector2.zero; // Reset moveInput when movement input is canceled

        // Subscribe to the look input events
        playerInput.Movement.LookAround.performed += ctx => lookInput = ctx.ReadValue<Vector2>(); // Update lookInput when look input is performed
        playerInput.Movement.LookAround.canceled += ctx => lookInput = Vector2.zero; // Reset lookInput when look input is canceled

        // Subscribe to the jump input event
        playerInput.Movement.Jump.performed += ctx => Jump(); // Call the Jump method when jump input is performed

        // Subscribe to the shoot input event
        playerInput.Movement.Shoot.performed += ctx => Shoot(); // Call the Shoot method when shoot input is performed

        // Subscribe to the pick-up input event
        playerInput.Movement.PickUp.performed += ctx => PickUpObject(); // Call the PickUpObject method when pick-up input is performed

        // Subscribe to the crouch input event
        playerInput.Movement.Crouch.performed += ctx => ToggleCrouch(); // Call the Crouch method when crouch input is performed

      


    }

    private void Update()
    {
        // Call Move and LookAround methods every frame to handle player movement and camera rotation
        Movement();
        LookAround();
        ApplyGravity();
       
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            isSprinting = true;
            
            Debug.Log("Is now sprinting");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            
            Debug.Log("Has stopped");
        }

        if (Input.GetKeyDown(KeyCode.C) && characterController.velocity != standstill)
        {
            isSliding = true;

            Debug.Log("Is now sliding");
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            isSliding = false;

            Debug.Log("Has stopped sliding");
        }

        // Call climbing mechanics every frame
        CheckForClimb();

        if (isClimbing)
        {
            Climb();
        }

        // Adjust the FOV based on whether sprinting or not
        AdjustFOV();

    }

    /*
    public void Movement()
    {
        // Create a movement vector based on the input
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        // Transform direction from local to world space
        move = transform.TransformDirection(move);

        float currentSpeed;
        if (isCrouching )
        {
            currentSpeed = crouchSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        if (isSprinting ) 
        {
            currentSpeed = sprintSpeed;
        }
        else 
        {
            currentSpeed = moveSpeed;
        }

    
        characterController.Move(move * currentSpeed * Time.deltaTime);
    }*/

    public void Movement()
    {
        if (isClimbing) return; // Skip normal movement while climbing

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y); // Create movement vector
        move = transform.TransformDirection(move); // Transform direction to world space

        // Set speed depending on crouch, sprint, or normal state
        float currentSpeed = isCrouching ? crouchSpeed : (isSprinting ? sprintSpeed : moveSpeed);

        characterController.Move(move * currentSpeed * Time.deltaTime);
    }

    private void CheckForClimb()
    {
        // Perform a raycast forward to check for climbable objects
        RaycastHit hit;
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);

        // Debugging the ray to see if it hits anything
        Debug.DrawRay(playerCamera.position, playerCamera.forward * climbDetectionRange, Color.green, 1f);

        // Check if we're looking at a climbable surface within range
        if (Physics.Raycast(ray, out hit, climbDetectionRange, climbableLayer))
        {
            if (hit.collider.CompareTag("Climbable"))
            {
                // Check if we're close enough to climb using proximity check
                float distanceToClimbable = Vector3.Distance(transform.position, hit.point);
                if (distanceToClimbable <= climbTriggerDistance)
                {
                    canClimb = true; // Allow climbing if close enough

                    // If the player presses the "Climb" button and is not already climbing
                    if (Time.time - lastClimbChangeTime >= climbCooldown)
                    {
                        if (Input.GetKeyDown(KeyCode.E) && !isClimbing)
                        {
                            isClimbing = true;
                            characterController.enabled = false; // Disable CharacterController while climbing
                            velocity = Vector3.zero; // Reset velocity
                            lastClimbChangeTime = Time.time; // Update last climb change time
                            Debug.Log("Started climbing!");
                        }
                    }
                }
                else
                {
                    canClimb = false; // Cannot climb if not close enough
                }
            }
        }
        else
        {
            canClimb = false; // Reset canClimb if no climbable object is detected
        }
    }



    private void Climb()
    {
        if (isClimbing)
        {
            // Climb up if pressing "W" and down if pressing "S"
            float climbInput = Input.GetAxis("Vertical");

            // Move the player up/down along the Y-axis
            transform.Translate(Vector3.up * climbSpeed * climbInput * Time.deltaTime);

            // If the player presses "E" again, stop climbing
            if (Time.time - lastClimbChangeTime >= climbCooldown)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isClimbing = false;
                    characterController.enabled = true; // Re-enable CharacterController
                    lastClimbChangeTime = Time.time; // Update last climb change time
                    Debug.Log("Stopped climbing!");
                }
            }
        }
    }

    /* public void HoldtoSprint()
     {
         // Create a movement vector based on the input
         Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

         // Transform direction from local to world space
         move = transform.TransformDirection(move);

         float currentSpeed;


         currentSpeed = sprintSpeed;          

         // Move the character controller based on the movement vector and speed
         characterController.Move(move * currentSpeed * Time.deltaTime);
     }*/

    public void LookAround()
    {
        // Get horizontal and vertical look inputs and adjust based on sensitivity
        float LookX = lookInput.x * lookSpeed;
        float LookY = lookInput.y * lookSpeed;

        // Horizontal rotation: Rotate the player object around the y-axis
        transform.Rotate(0, LookX, 0);

        // Vertical rotation: Adjust the vertical look rotation and clamp it to prevent flipping
        verticalLookRotation -= LookY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        // Apply the clamped vertical rotation to the player camera
        playerCamera.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
    }



    public void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f; // Small value to keep the player grounded
        }

        velocity.y += gravity * Time.deltaTime; // Apply gravity to the velocity
        characterController.Move(velocity * Time.deltaTime); // Apply the velocity to the character
    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            // Calculate the jump velocity
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
  
    public void Shoot()
    {
        if (holdingGun && projectilePrefab != null && firePoint != null)
        {
            // Instantiate the projectile at the fire point
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Align the bullet's forward direction with the firePoint's forward direction
            projectile.transform.forward = firePoint.forward;

            // Get the Rigidbody component of the projectile
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Apply velocity to the projectile in the forward direction of the firePoint
                rb.velocity = firePoint.forward * projectileSpeed;
            }
            else
            {
                Debug.LogError("Projectile is missing Rigidbody component!");
            }

            // Destroy the projectile after 3 seconds to prevent memory overflow
            Destroy(projectile, 3f);
        }
        else
        {
            Debug.LogWarning("No gun is being held or firePoint/projectilePrefab is missing.");
        }
    }
       

    public void PickUpObject()
    {
        // Check if we are already holding an object
        if (heldObject != null)
        {
            // Release the held object
            heldObject.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
            heldObject.transform.parent = null; // Detach from holdPosition
            heldObject = null; // Clear the reference
            holdingGun = false; // Update the holdingGun flag
        }

        // Perform a raycast from the camera's position forward
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        // Debugging: Draw the ray in the Scene view
        Debug.DrawRay(playerCamera.position, playerCamera.forward * pickUpRange, Color.red, 2f);

        // Check if the ray hits an object within the specified range
        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            // Check if the hit object has the tag "PickUp" or "Gun"
            if (hit.collider.CompareTag("PickUp") || hit.collider.CompareTag("Gun"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;

                // Update the holdingGun flag if necessary
                if (hit.collider.CompareTag("Gun"))
                {
                    holdingGun = true;
                }
            }
        }
    }
    public void ToggleCrouch()
    {
        if (isCrouching)
        {
            //stand up
            characterController.height = standingHeight;
            isCrouching = false;
        }
        else
        {
            characterController.height = crouchHeight;
            isCrouching = true;
        }
    }

    private void AdjustFOV()
    {
        float targetFOV = isSprinting ? sprintFOV : normalFOV;
        playerCameraComponent.fieldOfView = Mathf.Lerp(playerCameraComponent.fieldOfView, targetFOV, fovTransitionSpeed * Time.deltaTime);
    }




}

