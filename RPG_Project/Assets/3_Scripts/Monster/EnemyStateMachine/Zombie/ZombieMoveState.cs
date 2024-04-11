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
        Debug.Log("Move 상태 진입");


        stateTimer = enemy.moveTime;    // Idle 상태를 지속하는 시간을 초기화
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Move 상태 퇴장");
    }

    public override void Update()
    {
        base.Update();

        // enemy.NavMeshAgent 플레이어 쫒는 기능

        if (stateTimer <= 0)
        {
            stateMachine.ChangeStat(enemy.idelState);
        }
    }
}
