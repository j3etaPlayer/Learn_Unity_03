using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour, IAttackable
{
    public ManualCollider attackColider;
    private void Awake()
    {
        attackColider = GetComponentInChildren<ManualCollider>();
    }
    public void AttackTrigger()
    {
        Collider[] colliders = attackColider.GetColliderObject();
        foreach (var hit in colliders)
        {
            if (hit.GetComponentInParent<Enemy>() != null)
            {
                PlayerManager player = hit.GetComponentInParent<PlayerManager>();
                player.TakeDamage(player.AttackPower, player.transform.position);

            }
        }
    }
}