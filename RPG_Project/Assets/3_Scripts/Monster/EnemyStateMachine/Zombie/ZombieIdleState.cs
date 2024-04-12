using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : EnemyState
{
    Enemy_Zombie enemy;

    public ZombieIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animName, Enemy_Zombie _enemy) : base(_enemyBase, _stateMachine, _animName)
    {
        enemy = _enemy;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("idle 상태 진입");


        stateTimer = enemy.idleTime;    // Idle 상태를 지속하는 시간을 초기화
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("idle 상태 퇴장");
    }

    public override void Update()
    {
        base.Update();

        Transform target = enemy.SearchTarget();
        if (target)
        {
            if (enemy.IsAvailableAttack)
            {
                stateMachine.ChangeState(enemy.attackState);
            }
            else
            {
                stateMachine.ChangeState(enemy.moveState);
            }
        }
        else
        { 
            if (stateTimer <= 0 )
            {
                stateMachine.ChangeState(enemy.patrolState);
            }
        }

    }
}
