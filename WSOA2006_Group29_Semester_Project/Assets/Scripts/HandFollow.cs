using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandFollow : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera's transform
    public Transform handTransform; // Reference to the hand's transform
    public Vector3 handOffset; // Offset from the camera's position

    void Update()
    {
        if (cameraTransform != null && handTransform != null)
        {
            // Update the hand's position relative to the camera
            handTransform.position = cameraTransform.position + cameraTransform.forward * handOffset.z
                                      + cameraTransform.right * handOffset.x
                                      + cameraTransform.up * handOffset.y;

            // Update the hand's rotation to match the camera's rotation
            handTransform.rotation = cameraTransform.rotation;
        }
    }
}

