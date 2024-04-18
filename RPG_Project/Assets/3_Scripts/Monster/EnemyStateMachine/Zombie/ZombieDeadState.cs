using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieDeadState : EnemyState
{
    Enemy_Zombie enemy;

    public ZombieDeadState(Enemy _enemybase, EnemyStateMachine _stateMachine, string _animName, Enemy_Zombie _enemy) : base(_enemybase, _stateMachine, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.agent.isStopped = true;
        stateTimer = 5;         // ���� 2�� deadTime ������ �ٲ� ��
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
            GameObject.Destroy(enemy.gameObject);   // HP�� 0�� ���� �� ������Ʈ�� �ı��Ѵ�.
    }
}
