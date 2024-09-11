using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage = 1; // Damage per bullet

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object hit has the EnemyHealth component
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            // Deal damage to the enemy
            enemy.TakeDamage(bulletDamage);

            // Destroy the bullet after hitting the enemy
            Destroy(gameObject);
        }
    }
}

