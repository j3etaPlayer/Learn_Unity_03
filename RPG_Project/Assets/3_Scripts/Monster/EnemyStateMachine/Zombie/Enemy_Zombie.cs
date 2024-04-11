using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Zombie : Enemy
{
    #region State
    public ZombieIdleState idelState {  get; private set; }
    public ZombieMoveState moveState { get; private set; }
    #endregion



    public ZombieData zombieData;

    protected override void Awake()
    {
        base.Awake();
        OnLoadComponents();

        idelState = new ZombieIdleState(this, stateMachine, "Idle", this);
        moveState = new ZombieMoveState(this, stateMachine, "Move", this);
        // attackStat...
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initilize(idelState);
    }
    protected override void Update()
    {
        base.Update();
    }

    public override void OnLoadComponents()
    {
        base.OnLoadComponents();
        HP = zombieData.HP;
        AttackPower = zombieData.attack;
        AttackRange = zombieData.attackRange;
    }
}
