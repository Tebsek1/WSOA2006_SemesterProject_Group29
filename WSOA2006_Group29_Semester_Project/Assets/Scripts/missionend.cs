using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class missionend : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerCasting.countcollects >= 7)
        {
            TaskManager.taskcount++;
            TaskManager.instance.collectable.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        // Check if the collided object is grabbable
        if (col.gameObject.tag == "Grabbable")
        {
            if (PlayerCasting.countcollects >= 7)
            {
                TaskManager.taskcount = +1;
                SceneManager.LoadScene("Mission End");
            }
               
           
        }
    }
}
