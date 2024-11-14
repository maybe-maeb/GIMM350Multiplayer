using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Tooltip("Changes how much damage the enemy fireballs do.")]
    public float bulletDamage = 5f;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Players")
        {
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(-bulletDamage);
            Destroy(gameObject);
        }

        else if (col.gameObject.CompareTag("Fireball") && gameObject.CompareTag("Fireball"))
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), col);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}