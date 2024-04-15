using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ���� Ÿ���� �����ϴ� �þ߸� �����ϴ� Ŭ����
/// </summary>
public class FieldOfView : MonoBehaviour
{
    Enemy_Zombie enemy;

    public float viewRange = 5f;
    [Range(0, 180)]
    public float viewAngle = 90f;

    public LayerMask targetMask;
    public LayerMask obstalceMask;


    [Header("�þ� �� ���� �ִ� Ÿ�� �� ���� ������ �ִ� ����� ã�� ����")]
    private List<Transform> visibleTargets = new List<Transform>();
    private Transform nearestTarget;
    private float distanceToTarget = 0.0f;

    public float findDelay = 0.2f;      // �ð����� target�� ã�� �Լ��� �����Ѵ�.
    
    #region ������Ƽ
    public Transform NearestTarget => nearestTarget;
    public List<Transform> VisibleTargets => visibleTargets;
    #endregion

    private void Awake()
    {
        enemy = GetComponent<Enemy_Zombie>();   
    }

    private void Start()
    {
        viewRange = enemy.viewRange;

        StartCoroutine(FindeTargetWithDelay(findDelay));
    }
    IEnumerator FindeTargetWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    /// <summary>
    /// �þ� ���� ���� �ִ� ��ü�� �Ÿ��� ���Ͽ� �ּ� �Ÿ��� ��ü�� ��ȯ�Ѵ�.
    /// </summary>
    void FindVisibleTargets()
    {
        // �Լ� ���ึ�� �����͸� �ʱ�ȭ
        distanceToTarget = 0.0f;
        nearestTarget = null;
        visibleTargets.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRange, targetMask);
        for (int i = 0;i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized; // Ÿ�ٰ��� ������ ���Ѵ�
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstalceMask)) // ��ֹ��� ���� ���
                {
                    visibleTargets.Add(target);
                    if (nearestTarget == null || distanceToTarget > dstToTarget)
                    {
                        nearestTarget = target;
                        distanceToTarget = dstToTarget;
                    }
                }
            }
        }
    }
}
