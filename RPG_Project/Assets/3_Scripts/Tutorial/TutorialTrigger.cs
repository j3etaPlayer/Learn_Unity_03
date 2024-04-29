using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialTrigger : TutorialBase
{
    [SerializeField]PlayerManager player;
    [SerializeField] private Transform missionUI;

    public bool isTrigger = false;    // trigger�� ����Ǹ� Setnexttutorial����
    [SerializeField] Transform targetTrigger;       // Ʃ�丮��Ʈ������ istrigger�� true�� ��ȯ������ target����

    public override void Enter()
    {
        player.isPerformingAction = false;
        player.canMove = true;

        missionUI.gameObject.SetActive(true);
    }

    public override void Execute(TutorialController controller)
    {

        if (isTrigger)
        {
            controller.SetNextTutorial();
            missionUI.gameObject.SetActive(false);
            // missionUI�� �̼� �Ϸ���·� ��ȯ�����ִ� �ڵ��ۼ�
        }
    }

    public override void Exit()
    {
        
    }

}
