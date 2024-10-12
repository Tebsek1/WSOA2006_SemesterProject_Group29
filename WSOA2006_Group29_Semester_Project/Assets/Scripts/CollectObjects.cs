using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectObjects : MonoBehaviour
{
    public Transform handPosition;   // Position where the object will be held
    public GameObject TextDisplay;   // UI Text object to display messages
    public Text collectstext;        // Text object to display collected count
        // Counter for collected objects
    private GameObject currentGrabbable;
    public static CollectObjects instance;
    public void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // Check if there is a current grabbable object and the player presses 'E'
        if (currentGrabbable != null && Input.GetKeyDown(KeyCode.E))
        {
            GrabObject();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        // Check if the collided object is grabbable
        if (col.gameObject.tag == "Grabbable")
        {
            currentGrabbable = this.gameObject; // Store the reference to the grabbable object
            TextDisplay.GetComponent<Text>().text = "Press 'E' to collect object"; // Show message
            TextDisplay.SetActive(true); // Show the text
        }
    }

    void OnTriggerExit(Collider col)
    {
        // Reset when exiting the trigger zone
        if (col.gameObject.tag== "Grabbable")
        {
            currentGrabbable = null; // Clear the reference
            TextDisplay.SetActive(false); // Hide the text
           
        }
    }

    private void GrabObject()
    {
        if (currentGrabbable != null)
        {
            currentGrabbable.GetComponent<Rigidbody>().isKinematic = true; // Disable physics
            currentGrabbable.transform.position = handPosition.position;   // Move object to hand
            currentGrabbable.transform.parent = handPosition; // Attach to hand
            PlayerCasting.countcollects++; // Increment the counter
            collectstext.text = PlayerCasting.countcollects.ToString(); // Update text display
            Debug.Log("Object grabbed!");
            currentGrabbable = null; // Clear the reference after grabbing
            TextDisplay.SetActive(false); // Hide the text
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            TextDisplay.GetComponent<Text>().text = "";
        }
    }
}
