using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashToMenu : MonoBehaviour {

    void Start()
    {
        StartCoroutine(SplashFinish());
    }

    IEnumerator SplashFinish()
    {
        // waits for two and a half seconds in the process
        yield return new WaitForSeconds(2.5f);
        // loads to scene 3
        SceneManager.LoadScene(3);
    }
}
