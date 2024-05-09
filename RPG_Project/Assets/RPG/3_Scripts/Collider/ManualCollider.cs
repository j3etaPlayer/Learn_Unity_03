using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ����� LayerMask�� manualCollider ������Ʈ���� �������ּ���.
/// </summary>
public class ManualCollider : MonoBehaviour
{
    public Vector3 boxSize = new Vector3(2, 2, 2);
    public LayerMask layermask;

    /// <summary>
    /// ��ü�� ȸ�� �������� �浹�� �˻��ϴ� LayerBox�� �����ϰ�, �浹�� ��ü�� ��ȯ�Ѵ�.
    /// </summary>
    public Collider[] GetColliderObject() 
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, boxSize / 2, transform.rotation, layermask);
        return colliders;
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}
