using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public float slideDistance = 5f; // Distance to slide the door
    public float slideSpeed = 2f; // Speed at which the door slides
    private Vector3 originalPosition;
    private Vector3 openPosition;
    private bool isOpening = false;
    private bool isClosing = false;

    void Start()
    {
        originalPosition = transform.position;
        openPosition = originalPosition + new Vector3(-slideDistance, 0, 0); // Slide to the left
    }

    void Update()
    {
        if (isOpening)
        {
            // Slide the door to the left
            transform.position = Vector3.MoveTowards(transform.position, openPosition, slideSpeed * Time.deltaTime);
            if (transform.position == openPosition)
            {
                isOpening = false; // Stop sliding when the target position is reached
            }
        }
        else if (isClosing)
        {
            // Slide the door back to the original position
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, slideSpeed * Time.deltaTime);
            if (transform.position == originalPosition)
            {
                isClosing = false; // Stop sliding when the target position is reached
            }
        }
    }

    public void OpenDoor()
    {
        isOpening = true;
    }

    public void CloseDoor()
    {
        isClosing = true;
    }
}

