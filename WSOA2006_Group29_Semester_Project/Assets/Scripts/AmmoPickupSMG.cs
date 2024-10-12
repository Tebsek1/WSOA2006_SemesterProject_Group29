using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickupSMG : MonoBehaviour
{   
    // Declaring variable
    public AudioSource AmmoSound;

    void OnTriggerEnter(Collider col)
    {   
        // creates ammo audio sound when being picked up
        AmmoSound.Play();

        // loaded ammo allows a maximum of 30 to be picked
        if (GlobalAmmo.LoadedAmmo == 0)
        {
            GlobalAmmo.LoadedAmmo += 30;
            this.gameObject.SetActive(false);
        }
        else
        {
            // the loaded ammo gets added unto CurrentAmmo every time
            GlobalAmmo.CurrentAmmo += 30;
            this.gameObject.SetActive(false);
        }
    }
}

