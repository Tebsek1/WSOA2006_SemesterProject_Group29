using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public string[] Task;
    public Text taskdisplay;
    public static int taskcount;
    public GameObject collectable;
    public static TaskManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        taskdisplay.text = Task[taskcount];
    }
}
