using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공격할 대상의 LayerMask를 manualCollier 컴포넌트에서 지정해주세요
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
