using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    PlayerManager player;

    [Header("�ִϸ��̼� ���� ����")]
    private Animator animator;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        animator = GetComponentInChildren<Animator>();
    }

    // �ִϸ��̼� Ŭ���� �̸��� ȣ���Ͽ� �� playerManager���� �ִϸ��̼��� ���� ȣ���� �� �ְ� ĸ��ȭ�� �Լ�
    public void PlayerTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true, bool canRotate = false, bool canMove = false) 
    {
        animator.CrossFade(targetAnimation, 0.2f);
        player.isPerformingAction = isPerformingAction;
        player.applyRootMotion = applyRootMotion;
        player.canRotate = canRotate;
        player.canMove = canMove;
    }

    private void AttackTrigger()
    {
        Collider[] colliders = player.manualCollider.GetColliderObject();
        foreach(var hit in colliders)
        {
            if (hit.GetComponentInParent<Enemy>() != null)
            {
                Enemy enemy = hit.GetComponentInParent<Enemy>();
                (enemy as Enemy_Zombie).TakeDamage(player.AttackPower, player.transform.position);

            }
        }
    }
}
