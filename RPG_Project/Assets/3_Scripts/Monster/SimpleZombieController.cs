using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleZombieController : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent agent;

    [Header("���� ������ ���")]
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
    /// target�� ��ġ�� ���� Nav Mesh Agent�� �̿��Ͽ� �����ϴ� �Լ�
    /// </summary>
    private void TraceTarget(Vector3 des)
    {
        agent.SetDestination(des);
    }
    
}
