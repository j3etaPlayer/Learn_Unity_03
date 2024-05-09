using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFadeEffect : TutorialBase
{
    [SerializeField] FadeEffect fadeEffect;     // Ư�� �̹����� alpha�� 0~1�� ��ȯ���ִ� �ڵ�
    [SerializeField] bool isFadeIn = true;      // true�� fadein, false�� out
    private bool isCompleted = false;


    public override void Enter()
    {
        // �ڷ�ƾ�� �������� ���� Ʃ�丮���� �����ؾ��Ѵ�.
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
