using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMonitor : MonoBehaviour {

    // Declaring variable
    public int CurrentHealth;
    public Slider healthbar;

    void Update()
    {
        // Sets the amount of health to each red bar
        CurrentHealth = GlobalHealth.PlayerHealth;
        healthbar.value = CurrentHealth;


    }
}
