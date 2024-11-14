using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFight : EnemyState
{
    public EnemyFight(EnemyStateController esm) : base(esm) { }
    public override void OnStateEnter()
    {
        esm.GetComponent<NavMeshAgent>().isStopped = false; //you can move
    }

    public override void CheckTransitions()
    {
        esm.FindNearestPlayer();
        if (esm.nearestPlayer != null) //if a player exists
        {
            float dist = Vector3.Distance(esm.enemy.transform.position, esm.nearestPlayer.position); //compare distance between you and player
            if (dist >= esm.maxPlayerDistance) //if nearest player is too far away
            {
                esm.SetState(new EnemyIdle(esm)); //idle
            }
        }
    }

    public override void Act()
    {
        esm.FindNearestPlayer(); //look for nearest player to your location
        if (esm.nearestPlayer != null) //if a player exists
        { 
            esm.enemy.destination = esm.nearestPlayer.position; //move towards them
            
            if (esm.reloading == false) //if you aren't reloading
            {
                esm.Shoot(); //shoot 
            }  
        }   
    }

    public override void OnStateExit()
    {

    }
}