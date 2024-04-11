using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleZombieController : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent agent;

    [Header("좀비가 도착할 대상")]
    public Transform target;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        TraceTarget(target.position);
    }

    /// <summary>
    /// target의 위치를 통해 Nav Mesh Agent를 이용하여 추적하는 함수
    /// </summary>
    private void TraceTarget(Vector3 des)
    {
        agent.SetDestination(des);
    }
    
}
