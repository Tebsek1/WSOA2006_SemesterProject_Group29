using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemyScript : MonoBehaviour
{   //declaring variable
    public int EnemyHealth = 15;
    public GameObject TheSpider;

    void DeductPoints(int DamageAmount)
    {   //enemy health taken damage
        EnemyHealth -= DamageAmount;        
    }

    void Update()
    {   //if statement for terminating gameObject's health when less than zero 
        if (EnemyHealth <= 0)
        {
            this.GetComponent<SpiderFollow>().enabled = false;
            TheSpider.GetComponent<Animation>().Play("die");
            //StartCoroutine(EndZombie());
            EnemyHealth = 1;
        }
    }

    IEnumerator EndZombie()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
