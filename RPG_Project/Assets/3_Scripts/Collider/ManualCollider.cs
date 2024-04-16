using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ����� LayerMask�� manualCollier ������Ʈ���� �������ּ���
/// </summary>
public class ManualCollider : MonoBehaviour
{
    public Vector3 collider = new Vector3(2, 2, 2);
    public LayerMask layerMask;

    public Collider[] GetColliderObject()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, collider, Quaternion.identity, layerMask);
        return colliders;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, collider);
    }


}
