using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // Max health is 3 (for 3 hits)
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Initialize health
    }

    // This method will be called when the enemy is hit by a bullet
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if health has dropped to 0 or below
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to handle the enemy's death
    void Die()
    {
        // You can play a death animation or effect here if needed
        Destroy(gameObject); // Destroy the enemy object
    }
}

