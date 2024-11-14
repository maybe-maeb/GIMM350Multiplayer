using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //the rest of these are all for the Health Bar 

    private float maxHealthBarLength = 1.0f;
    private float healthPercentage;
    private float currentHealthBarLength;
    [Tooltip("HealthBar image goes here")]
    [SerializeField] private Image healthBarImage;
    private RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        //Reset the enemy's health to its base amount 
        currentHealth = baseHealth;

        rt = healthBarImage.GetComponent<RectTransform>();
        healthBarImage.GetComponent<Image>().color = Color.green;
    }

    public void TakeDamage(float damage){
        //Decrease its current health by the damage taken
        currentHealth -= damage;

        //If its health is less than 0, die
        if (currentHealth <= 0) Die();

        //Create a hit effect and destroy it after a second
        GameObject hitFX = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(hitFX, 1f);

        UpdateBar();
    }

    void UpdateBar()
    {
        if (currentHealth > baseHealth)
        {
            currentHealth = baseHealth;
        }

        healthPercentage = currentHealth / baseHealth;
        currentHealthBarLength = maxHealthBarLength * healthPercentage;
        rt.sizeDelta = new Vector2(currentHealthBarLength, rt.sizeDelta.y);
        BarColor();
    }

    void BarColor()
    {
        if (currentHealth >= baseHealth * 0.5)
        {
            healthBarImage.GetComponent<Image>().color = Color.green;
        }
        else if (currentHealth < baseHealth * 0.5 && currentHealth >= baseHealth * 0.25)
        {
            healthBarImage.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            healthBarImage.GetComponent<Image>().color = Color.red;
        }
    }

    void Die(){
        //Create a death effect and destroy it after a few seconds
        GameObject deathFX = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathFX, 3f);

        //Destroy the enemy
        Destroy(this.gameObject);
    }
}
