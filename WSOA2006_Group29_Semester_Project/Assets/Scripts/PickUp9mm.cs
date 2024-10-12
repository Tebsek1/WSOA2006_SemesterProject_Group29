using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp9mm : MonoBehaviour {
    // declaring variables
    public float TheDistance = PlayerCasting.DistanceFromTarget;
    public GameObject TextDisplay;
    public GameObject FakeGun;
    public GameObject RealGun;
    public GameObject AmmoDisplay;
    public AudioSource PickUpAudio;

    // Update is called once per frame
	void Update () {
        TheDistance = PlayerCasting.DistanceFromTarget;        
    }
    // mouseover function allows you to click button near triggered button
    void OnMouseOver()
    {   // if the distance is less than 2 then text will display
        if (TheDistance <= 2)
        {
            TextDisplay.GetComponent< Text > ().text = "Take 9mm Pistol";
        }

        if (Input.GetButtonDown("Action"))
        {
            Debug.Log("inside");

            if (TheDistance <= 2)
            {
                TakeNineMil();
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
        TaskManager.taskcount = +1;
        TaskManager.instance.collectable.SetActive(true);
        PickUpAudio.Play();
        //transform.position = Vector3.up;
        transform.position += Vector3.up * Time.deltaTime;
        //transform.position = Vector3(0, -1000, 0);
        FakeGun.SetActive(false);
        RealGun.SetActive(true);
        AmmoDisplay.SetActive(true);
    }
}
