using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GlobalAmmo : MonoBehaviour
{   
    // Declarings variables
    public static int CurrentAmmo;
    public int InternalAmmo;
    public GameObject AmmoDisplay;
    public static int LoadedAmmo;
    public int Internaloaded;
    public GameObject LoadedDisplay;

    void Update()
    {   
        // for ammo display addition
        InternalAmmo = CurrentAmmo;
        Internaloaded = CurrentAmmo;
        AmmoDisplay.GetComponent< Text > ().text = "" + Internaloaded;
        LoadedDisplay.GetComponent<Text>().text = "" + LoadedAmmo;
    }
}
