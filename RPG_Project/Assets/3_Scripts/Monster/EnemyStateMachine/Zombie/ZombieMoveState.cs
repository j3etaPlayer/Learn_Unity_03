using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMoveState : EnemyState
{
    public Enemy_Zombie enemy;

    public ZombieMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animName, Enemy_Zombie _enemy) : base(_enemyBase, _stateMachine, _animName)
    {
        this.enemy = _enemy;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Move ���� ����");


        stateTimer = enemy.moveTime;    // Idle ���¸� �����ϴ� �ð��� �ʱ�ȭ
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Move ���� ����");
        enemy.agent.ResetPath();
    }

    public override void Update()
    {
        base.Update();

        // enemy.NavMeshAgent �÷��̾� �i�� ���
        Transform target = enemy.SearchTarget();

        if (target)
        {
            enemy.agent.SetDestination(target.position);
        }
        else
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (enemy.IsAvailableAttack)
        {
            stateMachine.ChangeState(enemy.attackState);
        }

    }
}
