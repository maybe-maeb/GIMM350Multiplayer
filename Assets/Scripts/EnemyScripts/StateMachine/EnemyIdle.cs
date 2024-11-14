using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdle : EnemyState
{
    public EnemyIdle(EnemyStateController esm) : base(esm) { }
    public override void OnStateEnter()
    {
        esm.GetComponent<NavMeshAgent>().isStopped = true; //you can't move
    }

    public override void CheckTransitions()
    {
        esm.FindNearestPlayer();
        if (esm.nearestPlayer != null) //if a player exists
        {
            float dist = Vector3.Distance(esm.enemy.transform.position, esm.nearestPlayer.position); //compare enemy and player distance
            if (dist < esm.maxPlayerDistance) //if the nearest player is within range
            {
                esm.SetState(new EnemyFight(esm)); //chase and fight
            }
        }
    }

    public override void Act()
    {

    }

    public override void OnStateExit()
    {

    }
}
