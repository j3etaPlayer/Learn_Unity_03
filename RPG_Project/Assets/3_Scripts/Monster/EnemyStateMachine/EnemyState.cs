using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehaviour를 상속하는 enemy클래스에 부착하여 사용
/// enemy의 상태에 따른 기능 enter, exit, update 기능 구현
/// 구현할 State 들이 Base로 상속할 클래스
/// </summary>
public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;

    private string animName;

    protected float stateTimer;

    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animName)
    {
        enemyBase = _enemyBase;
        stateMachine = _stateMachine;
        animName = _animName;
    }

    public virtual void Enter()
    {
        enemyBase.animator.CrossFade(animName, 0.2f);
    }
    public virtual void Exit()
    {

    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
}
