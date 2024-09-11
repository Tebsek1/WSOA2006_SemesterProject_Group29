using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunPickup : MonoBehaviour
{
    public Rigidbody gunRigidbody;

    private void Start()
    {
        // Make sure the gun is kinematic (not affected by physics) when on the floor
        gunRigidbody.isKinematic = true;
    }

    public void PickUp()
    {
        // When the gun is picked up, disable kinematic and parent it to the player's hand
        gunRigidbody.isKinematic = false;
        // Add logic to parent gun to player's hand
    }
}

