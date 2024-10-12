using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFollow : MonoBehaviour {

    // Declaring variables
    public GameObject ThePlayer;
    public float TargetDistance;
    public float AllowedRange = 10;
    public GameObject TheEnemy;
    public float EnemySpeed;
    public int AttackTrigger;
    public RaycastHit Shot;
    public int IsAttacking;
    public GameObject ScreenFlash;
    public AudioSource Hurt01;
    public AudioSource Hurt02;
    public AudioSource Hurt03;
    public int PainSound;

    void Update()
    {
        // where ever the player is in position, the enemy is always facing the player
        transform.LookAt(ThePlayer.transform);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot))
        {
            TargetDistance = Shot.distance;
            if (TargetDistance < AllowedRange)
            {
                // enemy speed is set for attack
                EnemySpeed = 0.01f;
                if (AttackTrigger == 0)
                {
                    TheEnemy.GetComponent<Animation>().Play("Walking");
                    transform.position = Vector3.MoveTowards(transform.position, ThePlayer.transform.position, Time.deltaTime);
                }
            }
            else
            {
                EnemySpeed = 0;
                TheEnemy.GetComponent<Animation>().Play("Idle");
            }
        }

        if (AttackTrigger == 1)
        {
            if (IsAttacking == 0)
            {
                StartCoroutine(EnemyDamage());
            }
            EnemySpeed = 0;
            TheEnemy.GetComponent<Animation>().Play("Attacking");
        }
    }

    void OnTriggerEnter()
    {
        AttackTrigger = 1;
    }

    void OnTriggerExit()
    {
        AttackTrigger = 0;
    }

    IEnumerator EnemyDamage()
    {
        // the amount of damage inflicted unto the player by the enmy AI
        IsAttacking = 1;
        PainSound = Random.Range(1, 4);
        yield return new WaitForSeconds(0.9f);
        ScreenFlash.SetActive(true);
        GlobalHealth.PlayerHealth -= 2;

        // the different sound effect the player makes when being attacked by the enemy AI
        if (PainSound == 1)
        {
            Hurt01.Play();
        }
        if (PainSound == 2)
        {
            Hurt02.Play();
        }
        if (PainSound == 3)
        {
            Hurt03.Play();
        }
        // waits for 0.5 seconds for a screen flash after every enemy attack
        yield return new WaitForSeconds(0.05f);
        ScreenFlash.SetActive(false);
        yield return new WaitForSeconds(1);
        IsAttacking = 0;
    }
}
