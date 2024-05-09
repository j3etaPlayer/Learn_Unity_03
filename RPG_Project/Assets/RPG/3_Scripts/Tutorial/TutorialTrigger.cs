using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialTrigger : TutorialBase
{
    [SerializeField]PlayerManager player;
    [SerializeField] private Transform missionUI;

    public bool isTrigger = false;    // trigger가 실행되면 Setnexttutorial실행
    [SerializeField] Transform targetTrigger;       // 튜토리얼트리거의 istrigger를 true로 변환시켜줄 target지정

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
            // missionUI를 미션 완료상태로 변환시켜주는 코드작성
        }
    }

    public override void Exit()
    {
        
    }

}
