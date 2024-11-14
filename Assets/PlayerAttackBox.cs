using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    public int damageToDeal;

    void OnTriggerEnter(Collider col){
        if (col.transform.CompareTag("Enemy")){
            col.gameObject.GetComponent<Enemy>().TakeDamage(damageToDeal); // Change the damage value to your liking
        }
    }
}
