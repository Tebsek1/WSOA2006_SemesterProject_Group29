using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpSMG : MonoBehaviour
{
    // Declaring variables
    public float TheDistance = PlayerCasting.DistanceFromTarget;
    public GameObject TextDisplay;
    public GameObject FakeGun;
    public GameObject RealGun;
    public GameObject AmmoDisplay;
    public AudioSource PickUpAudio;
    public GameObject Mechanics;

    // Update is called once per frame
	void Update () {
        TheDistance = PlayerCasting.DistanceFromTarget;        
    }
    // mouseover function allows you to click button near triggered button
    void OnMouseOver()
    {   // if the distance is less than 2 then text will display
        if (TheDistance <= 2)
        {
            TextDisplay.GetComponent< Text > ().text = "Press ‘E’ To interact";
        }

        if (Input.GetButtonDown("Action"))
        {
            Debug.Log("inside");

            if (TheDistance <= 2)
            {
                TakeNineMil();
                Mechanics.SetActive(true);
            }
        }       
    }

    // text disappears when you leave the area
    void OnMouseExit()
    {
        TextDisplay.GetComponent< Text > ().text = "";
    }

    // function to set up gun on display to pick up in game
    void TakeNineMil()
    {
        PickUpAudio.Play();
        transform.position += Vector3.up * Time.deltaTime;
        FakeGun.SetActive(false);
        RealGun.SetActive(true);
        AmmoDisplay.SetActive(true);
    }
}
