using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{   
    // Declaring variable
    public GameObject Flash;

    void Update()
    {   
        // if the ammo is greater than 1, gun is enabled to shoot
        if (GlobalAmmo.LoadedAmmo >= 1)
        {
            if (Input.GetButtonDown("Fire1"))
            {   
                // AudioSource enables the audio sound of the gun
                AudioSource gunsound = GetComponent<AudioSource>();
                gunsound.Play();
                Flash.SetActive(true);
                StartCoroutine(MuzzleOff());
                GetComponent<Animation>().Play("GunShot");
                GlobalAmmo.LoadedAmmo -= 1;
            }
        }
    }

    // Yield requires IEnumerator that's why we don't use void.
    IEnumerator MuzzleOff()
    {   
        // waits for 0.1 seconds
        yield return new WaitForSeconds(0.1f);
        Flash.SetActive(false);
    }
}
