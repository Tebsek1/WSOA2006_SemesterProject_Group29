using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOptions : MonoBehaviour
{
    public GameObject cameraObject;
   
    public Animator anim;
    void Start()
    {
        // Get the Animator component attached to the camera
      //  anim = GetComponent<Animator>();
        anim.GetComponent<Animator>().enabled = false;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void RotateCamera()
    {
        anim.GetComponent<Animator>().enabled=true;
    }
}
