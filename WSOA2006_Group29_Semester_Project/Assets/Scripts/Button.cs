using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public SlidingDoor doorScript; // Reference to the SlidingDoor script
    private bool isActivated = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isActivated)
        {
            ActivatePanel();
        }
    }

    void ActivatePanel()
    {
        isActivated = true;
        if (doorScript != null)
        {
            doorScript.OpenDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Optionally show UI prompt
        }
    }
}



