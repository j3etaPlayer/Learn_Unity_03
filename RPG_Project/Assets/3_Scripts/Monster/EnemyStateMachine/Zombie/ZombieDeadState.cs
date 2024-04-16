using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeadState : EnemyState
{
    Enemy_Zombie enemy;
    public ZombieDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animName, Enemy_Zombie _enemy) : base(_enemyBase, _stateMachine, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 5;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
            GameObject.Destroy(enemy.gameObject);   // hp�� 0�� �Ǿ����� ������Ʈ�� �ı��Ѵ�.
    }
}
