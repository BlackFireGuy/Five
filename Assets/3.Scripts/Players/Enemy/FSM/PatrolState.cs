using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyBaseState
{
    float attackDistance = 1f;

    public override void EnterState(Enemy enemy)
    {
        enemy.aniState = 0;
        enemy.SwitchPoint();
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (!enemy.ani.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            enemy.aniState = 1;
            enemy.MoveToTarget();
        }

        if (Mathf.Abs((enemy.transform.position - enemy.targetPoint.position).magnitude) < attackDistance)
        {
            enemy.TransitionToState(enemy.patrolState);
        }

        if (enemy.attackList.Count > 0)
            enemy.TransitionToState(enemy.attackState);
    }
}
