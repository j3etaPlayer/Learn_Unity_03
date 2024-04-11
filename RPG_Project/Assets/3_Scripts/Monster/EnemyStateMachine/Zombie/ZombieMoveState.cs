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
    }

    public override void Update()
    {
        base.Update();

        // enemy.NavMeshAgent �÷��̾� �i�� ���

        if (stateTimer <= 0)
        {
            stateMachine.ChangeStat(enemy.idelState);
        }
    }
}
