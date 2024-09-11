using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false;
    private bool isPlayerNear = false;
    public float openAngle = 90f; // Angle the door should rotate
    public float doorSpeed = 2f;  // How fast the door opens/closes
    private Quaternion closedRotation;
    private Quaternion openRotation;
    public Transform doorPivot;  // Reference to the DoorPivot
    private bool isMoving = false;  // To check if the door is currently moving
    private Vector3 playerDirection; // To determine the player's position relative to the door

    void Start()
    {
        // Ensure the doorPivot is assigned
        if (doorPivot == null)
        {
            Debug.LogError("DoorPivot is not assigned! Please assign it in the Inspector.");
            return;
        }

        // Set the closed rotation based on the door's starting rotation
        closedRotation = doorPivot.rotation;
    }

    void Update()
    {
        // Check for player interaction
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isMoving)
        {
            // Check which side of the door the player is on
            Vector3 doorToPlayer = transform.InverseTransformPoint(Camera.main.transform.position);

            // If player is on the left of the door, rotate to the right; if on the right, rotate to the left
            if (doorToPlayer.x > 0)
            {
                openRotation = Quaternion.Euler(doorPivot.eulerAngles + new Vector3(0, openAngle, 0));
            }
            else
            {
                openRotation = Quaternion.Euler(doorPivot.eulerAngles + new Vector3(0, -openAngle, 0));
            }

            isOpen = !isOpen;
            isMoving = true;
        }

        // Smoothly open or close the door
        if (isMoving)
        {
            if (isOpen)
            {
                doorPivot.rotation = Quaternion.Slerp(doorPivot.rotation, openRotation, Time.deltaTime * doorSpeed);
                if (Quaternion.Angle(doorPivot.rotation, openRotation) < 0.1f)
                {
                    doorPivot.rotation = openRotation;
                    isMoving = false;
                    Debug.Log("Door fully opened.");
                }
            }
            else
            {
                doorPivot.rotation = Quaternion.Slerp(doorPivot.rotation, closedRotation, Time.deltaTime * doorSpeed);
                if (Quaternion.Angle(doorPivot.rotation, closedRotation) < 0.1f)
                {
                    doorPivot.rotation = closedRotation;
                    isMoving = false;
                    Debug.Log("Door fully closed.");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}