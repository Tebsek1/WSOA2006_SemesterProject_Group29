using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBobbing : MonoBehaviour
{
    public float bobAmount = 0.1f;  // Vertical bobbing amount
    public float bobSpeed = 1f;     // Speed of vertical bobbing
    public float bobAmountHorizontal = 0.05f; // Horizontal bobbing amount
    public float bobSpeedHorizontal = 0.5f;   // Speed of horizontal bobbing
    public float movementSpeedFactor = 1f;    // Factor to adjust bobbing based on movement speed
    public float headTiltAmount = 1f;         // Amount of head tilt when looking around

    private float timer = 0f;
    private Vector3 initialPosition;
    private bool isWalking = false;
    private float movementSpeed = 0f;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Check if the player is moving
        isWalking = Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d");
        movementSpeed = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        if (isWalking)
        {
            // Vertical and horizontal bobbing
            float verticalBobbing = Mathf.Sin(timer * bobSpeed * movementSpeedFactor) * bobAmount;
            float horizontalBobbing = Mathf.Cos(timer * bobSpeedHorizontal * movementSpeedFactor) * bobAmountHorizontal;

            transform.localPosition = initialPosition + new Vector3(horizontalBobbing, verticalBobbing, 0);

            // Update timer for bobbing effect
            timer += Time.deltaTime;
        }
        else
        {
            // Reset position if idle
            transform.localPosition = initialPosition;
        }

        // Optional: Add head tilt effect for looking around
        // Adjust head tilt based on the camera's forward direction
        float headTilt = Mathf.Clamp(Vector3.Dot(transform.forward, Vector3.up), -1, 1) * headTiltAmount;
        transform.localRotation = Quaternion.Euler(headTilt, 0, 0);
    }
}


