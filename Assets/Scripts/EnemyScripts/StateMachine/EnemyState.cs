using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState

{
    protected EnemyStateController esm;

    public abstract void CheckTransitions();

    public abstract void Act();

    public abstract void OnStateEnter();

    public abstract void OnStateExit();

    public EnemyState(EnemyStateController esm)
    {
        this.esm = esm;
    }
}