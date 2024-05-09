using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleZombieController : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent agent;

    [Header("���� �Ѿư� ���")]
    public Transform target;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        TraceTarget(target.position);
    }

    /// <summary>
    /// Ÿ���� ��ġ�� ���� navMeshAgent�� Ÿ���� �����ϴ� �Լ�
    /// </summary>
    private void TraceTarget(Vector3 des)
    {
        agent.SetDestination(des);
    }
}
