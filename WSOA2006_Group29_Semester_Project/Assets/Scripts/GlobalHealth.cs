using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalHealth : MonoBehaviour
{
    // Declaring variables
    public static int PlayerHealth = 10;
    public int InternalHealth;
    public GameObject HealthDisplay;

    // for health bar display on screen
    void Update()
    {
        InternalHealth = PlayerHealth;
        HealthDisplay.GetComponent<Text>().text = "Health: " + PlayerHealth;

        if (PlayerHealth == 0)
        {
            // loads to scene 2
            SceneManager.LoadScene(2);
        }
    }
}
