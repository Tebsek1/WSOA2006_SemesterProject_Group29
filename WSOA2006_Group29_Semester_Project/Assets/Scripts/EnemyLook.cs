using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLook : MonoBehaviour
{    
    // Declaring variables
    public GameObject ThePlayer;

    // Positions the enemy AI in the direction of the player
    void Update()
    {
        transform.LookAt(ThePlayer.transform);
    }
}
