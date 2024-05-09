using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMove : TutorialBase
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Vector3 targetPos;       // ui 가 이동하고싶은 최종 목적지
    [SerializeField] private float moveTime = 0.5f;
    private bool isCompleted = false;


    public override void Enter()
    {
        StartCoroutine(UILerpMove());
    }

    public override void Execute(TutorialController controller)
    {
        if (isCompleted) controller.SetNextTutorial();
    }

    public override void Exit()
    {
        
    }
    IEnumerator UILerpMove()
    {
        float currentTime = 0;
        float percent = 0;
        Vector3 startPos = rectTransform.anchoredPosition;      // 시작위치, target 위치, percent => lerp

        while(percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / moveTime;

            rectTransform.anchoredPosition = Vector3.Lerp(startPos, targetPos, percent);

            yield return null;
        }
        isCompleted = true;
    }
}
