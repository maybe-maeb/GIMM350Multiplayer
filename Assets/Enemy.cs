using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health & Damage")]
    [Tooltip("The amount of health the enemy spawns with.")]
    [SerializeField] private float baseHealth;

    //How much health they currently have
    private float currentHealth;
    
    [Tooltip("The effect to create when this enemy takes damage.")]
    [SerializeField] private GameObject hitEffect;

    [Tooltip("The effect to create when this enemy dies.")]
    [SerializeField] private GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        //Reset the enemy's health to its base amount 
        currentHealth = baseHealth;
    }

    void TakeDamage(float damage){
        //Decrease its current health by the damage taken
        currentHealth -= damage;

        //If its health is less than 0, die
        if (currentHealth <= 0) Die();

        //Create a hit effect and destroy it after a second
        GameObject hitFX = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(hitFX, 1f);
    }

    void Die(){
        //Create a death effect and destroy it after a few seconds
        GameObject deathFX = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathFX, 3f);

        //Destroy the enemy
        Destroy(this.gameObject);
    }
}
