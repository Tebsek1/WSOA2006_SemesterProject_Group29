using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoor001 : MonoBehaviour
{
    // declaring variables
    public float TheDistance = PlayerCasting.DistanceFromTarget;
    public GameObject TextDisplay;
    public GameObject TheDoor;
    private Animator anim;
    // Update is called once per frame
    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }
    // mouseover function allows you to click button near triggered button
    void OnMouseOver()
    {   // if distance is less 2 or less  then text will pop up
        if (TheDistance <= 2)
        {
            TextDisplay.GetComponent< Text > ().text = "Press E to Open The Door";
            if (Input.GetButtonDown("Action"))
            {
                Debug.Log("inside");

                if (TheDistance <= 2)
                {
                    StartCoroutine(OpenTheDoor(TheDoor));
                }
            }
        }
    }
    // text disappears when you leave the area
    void OnMouseExit()
    {
        TextDisplay.GetComponent< Text > ().text = "";
    }

    // Yield requires IEnumerator that's why we don't use void.
    IEnumerator OpenTheDoor(GameObject go)
    {
        // function that opens the door and stays open for 5 seconds then closes automatically
        go.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1);
        go.GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(5);
        go.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1);
        go.GetComponent<Animator>().enabled = false;       
    }
}
