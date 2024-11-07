using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
     [Header("Health & Damage")]
    [Tooltip("The amount of health the player spawns with.")]
    [SerializeField] private float baseHealth;

    //How much health they currently have
    private float currentHealth;
    
    [Tooltip("The effect to create when this player takes damage.")]
    [SerializeField] private GameObject hitEffect;

    [Tooltip("The effect to create when this player dies.")]
    [SerializeField] private GameObject deathEffect;

    public bool isPlayerOne;
    public PlayerHealth otherPlayer;

    void Start()
    {
        //Reset the health to its base amount 
        currentHealth = baseHealth;

        if (isPlayerOne) otherPlayer = GameObject.FindGameObjectWithTag("Players/Player2").GetComponent<PlayerHealth>();
        else otherPlayer = GameObject.FindGameObjectWithTag("Players/Player1").GetComponent<PlayerHealth>();
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

        if (otherPlayer == null) {
            Debug.Log("Both players dead! Game lose logic here.");
        }

        //Destroy the enemy
        Destroy(this.gameObject);
    }
}