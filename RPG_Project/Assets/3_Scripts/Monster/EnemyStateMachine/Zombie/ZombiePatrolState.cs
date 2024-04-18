using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �߿� Target�� �߰��ϸ� Target�� �����ϰ�, Target�� ��ġ�� �ٽ� �����ϴ� ���� ���� �ڵ�
/// </summary>
public class ZombiePatrolState : EnemyState
{
    Enemy_Zombie enemy;

    public ZombiePatrolState(Enemy _enemybase, EnemyStateMachine _stateMachine, string _animName, Enemy_Zombie _enemy) : base(_enemybase, _stateMachine, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        if(enemy.targetWayPoint == null)
        {
            enemy.FindnextWayPoint();
        }

        if (enemy.targetWayPoint)
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
                Transform nextDestination = enemy.FindnextWayPoint();
                enemy.agent.SetDestination(nextDestination.position);
                stateMachine.ChangeState(enemy.IdleState);                         // ������ �������� Idle ��� ���� ����
            }
        }
    }
}
