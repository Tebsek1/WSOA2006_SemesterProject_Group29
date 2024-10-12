using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Declaring variable
    public int EnemyHealth = 10;
    public GameObject TheZombie;

    void DeductPoints(int DamageAmount)
    {   
        //enemy health taken damage
        EnemyHealth -= DamageAmount;        
    }

    void Update()
    {   
        //if statement for terminating gameObject's health when less than zero 
        if (EnemyHealth <= 0)
        {
            this.GetComponent<ZombieFollow>().enabled = false;
            TheZombie.GetComponent<Animation>().Play("Dying");
            StartCoroutine(EndZombie());
        }
    }

    IEnumerator EndZombie()
    {   
        // waits for 3 seconds before the enemy AI is destroyed
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
