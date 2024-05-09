using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFadeEffect : TutorialBase
{
    [SerializeField] FadeEffect fadeEffect;     // 특정 이미지의 alpha를 0~1로 변환해주는 코드
    [SerializeField] bool isFadeIn = true;      // true면 fadein, false면 out
    private bool isCompleted = false;


    public override void Enter()
    {
        // 코루틴이 끝나고나서 다음 튜토리얼을 실행해야한다.
        if (isFadeIn)
        {
            fadeEffect.OnFade(FadeState.FadeIn, OnAfterFadeEffect);
        }
        else
        {
            fadeEffect.OnFade(FadeState.FadeOut, OnAfterFadeEffect);
        }
    }

    private void OnAfterFadeEffect()
    {
        isCompleted = true;
    }

    public override void Execute(TutorialController controller)
    {
        if (isCompleted)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {

    }

}
