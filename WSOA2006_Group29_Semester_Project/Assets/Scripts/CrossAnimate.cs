using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossAnimate : MonoBehaviour
{   
    // Declaring variables
    public GameObject UpCurs;
    public GameObject DownCurs;
    public GameObject LeftCurs;
    public GameObject RightCurs;
    /*public Animator upAnim;
    public Animator downAnim;
    public Animator leftAnim;
    public Animator rightAnim;

    /*void Awake()
    {
        StartCoroutine(WaitingAnim());
    }

    void SetAnim(bool aState)
    {
        upAnim.enabled = aState;
        downAnim.enabled = aState;
        leftAnim.enabled = aState;
        rightAnim.enabled = aState;
    }

    IEnumerator WaitingAnim()
    {   //while loop
        while (true)
        {
            while (!Input.GetButtonDown("Fire1"))
                yield return null;

            SetAnim(true);
            yield return new WaitForSeconds(0.1f);
            SetAnim(false);
        }
    }*/
    void Update()
    {
        if (GlobalAmmo.LoadedAmmo >= 1)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                /*UpCurs.GetComponent("Animator");
                DownCurs.GetComponent("Animator");
                LeftCurs.GetComponent("Animator");
                RightCurs.GetComponent("Animator");*/
                UpCurs.GetComponent<Animator>().enabled = true;
                DownCurs.GetComponent<Animator>().enabled = true;
                LeftCurs.GetComponent<Animator>().enabled = true;
                RightCurs.GetComponent<Animator>().enabled = true;
                StartCoroutine(WaitingAnim());
                //WaitingAnim();                
            }
        }
    }
    // Yield requires IEnumerator that's why we don't use void.
    IEnumerator WaitingAnim()
    {
        /*yield return new WaitForSeconds(0.1f);
        UpCurs.GetComponent ( "Animator" );
        DownCurs.GetComponent ( "Animator" );
        LeftCurs.GetComponent ( "Animator" );
        RightCurs.GetComponent ( "Animator" );*/

        yield return new WaitForSeconds(0.1f);
        UpCurs.GetComponent<Animator>().enabled = true;
        DownCurs.GetComponent<Animator>().enabled = true;
        LeftCurs.GetComponent<Animator>().enabled = true;
        RightCurs.GetComponent<Animator>().enabled = true;
    }
}