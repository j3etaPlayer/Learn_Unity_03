using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMoveState : EnemyState
{
    public Enemy_Zombie enemy;
    public ZombieMoveState(Enemy _enemybase, EnemyStateMachine _stateMachine, string _animName, Enemy_Zombie _enemy) : base(_enemybase, _stateMachine, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.moveTime;   // Idle ���¸� �����ϴ� �ð��� �ʱ�ȭ
    }

    public override void Exit()
    {
        base.Exit();
        // Exit�� �� agent�� ���� �Ÿ��� ������ �ش� ��ġ�� �̵��Ѵ�.
        enemy.agent.ResetPath();
    }

    public override void Update()
    {
        base.Update();

        //enemy. NaveMeshAgnet �÷��̾ �Ѵ� ���
        Transform target = enemy.SearchTarget();

        if (target)
        {
            enemy.agent.SetDestination(target.position);
        }
        else
        {
            stateMachine.ChangeState(enemy.IdleState);
        }

        if (enemy.IsAvailableAttack)
            stateMachine.ChangeState(enemy.attackState);

    }
}
