using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(FieldOfView))]
public class Enemy : Entity
{
    #region Compoment
    public EnemyStateMachine stateMachine {  get; private set; }
    public NavMeshAgent agent;
    public Rigidbody rigidbody;
    #endregion

    [Header("Enemy Stat")]
    public float idleTime;          // idle 상태 시간
    public float moveTime;          // 추적 시간
    public float chaseSpeed;        // 추적 속도
    public float AttackRange;
    public float viewRange;

    [Header("Search Target")]
    private FieldOfView fov;
    public LayerMask targetMask;    // 타겟 지정 레이어 마스크
    public Transform target;

    [Header("Patrol")]
    [SerializeField] private Transform[] wayPoints;
    [HideInInspector] public Transform targetWayPoint = null;
    private int wayPointIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    public override void OnLoadComponents()
    {
        base.OnLoadComponents();
        agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        fov = GetComponent<FieldOfView>();
    }
    public bool IsAvailableAttack
    {
        get
        {
            if (!target)
                return false;
            if ((target.position - transform.position).sqrMagnitude < AttackRange) 
                return true;
            else
                return false;
        }
    }
    public Transform SearchTarget()
    {
        this.target = fov.NearestTarget;

        return target;
    }

    /// <summary>
    /// true일때는 남은 거리가 있는 상태고, false면 다음 대상을 찾는다.
    /// </summary>
    public bool CheckRemainDistance()
    {
        if ((wayPoints[wayPointIndex].transform.position - transform.position).sqrMagnitude < 0.1f)
        {
            if (wayPointIndex < wayPoints.Length - 1)
                wayPointIndex++;
            else wayPointIndex = 0;

            return false;
        }
        return true;

    }

    public Transform FindNextWayPoints()
    {
        targetWayPoint = null;
        if(wayPoints.Length > 0)
        {
            targetWayPoint = wayPoints[wayPointIndex];
        }
        return targetWayPoint;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    public override void TakeDamage(int damage, Vector3 contactPos, GameObject hitEffectPrefabs = null)
    {
        base.TakeDamage(damage, contactPos, hitEffectPrefabs);
    }

}
