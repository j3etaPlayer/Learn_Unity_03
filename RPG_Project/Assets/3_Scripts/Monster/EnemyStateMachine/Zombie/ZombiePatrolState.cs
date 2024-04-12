using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 정찰중에 적을 발견하면 적을 공격하고 적이 없어지면 patrol하는 클래스
/// </summary>
public class ZombiePatrolState : EnemyState
{
    public Enemy_Zombie enemy;

    public ZombiePatrolState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animName, Enemy_Zombie enemy) : base(_enemyBase, _stateMachine, _animName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        if (enemy.targetWayPoint == null)
        {
            enemy.FindNextWayPoints();
        }
        if(enemy.targetWayPoint)
        {
            enemy.agent.SetDestination(enemy.targetWayPoint.position);
        }
    }

    public override void Exit()
    {
        base.Exit();
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
            if (!enemy.CheckRemainDistance())
            {
                Transform nextDestination = enemy.FindNextWayPoints();
                enemy.agent.SetDestination(nextDestination.position);
                stateMachine.ChangeState(enemy.idleState);
            }
        }

    }
}
