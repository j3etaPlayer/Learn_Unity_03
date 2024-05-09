using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public PlayerManager player;
    [SerializeField] private List<TutorialBase> tutorials; // 인스펙터 창에서 튜토리얼 클래스를 받아와서 저장할 리스트

    private TutorialBase currentTutorial = null;
    private int currentIndex = -1;

    private void Start()
    {
        SetNextTutorial();
        player.canMove = false;
        player.isPerformingAction = true;
    }
    private void Update()
    {
        currentTutorial?.Execute(this); // 튜토리얼 고유의 로직으 update로 실행하라
    }
    public void SetNextTutorial()  // 커런트 튜토리얼의 기능을 실행시켜라
    {
        if (currentTutorial != null) currentTutorial.Exit();

        if (currentIndex > tutorials.Count -1)
        {
            CompleteTutorial();
            return;
        }
        // 다음 튜토리얼 설정

        currentIndex++;
        currentTutorial = tutorials[currentIndex];


        currentTutorial.Enter();
    }
    void CompleteTutorial()
    {
        Debug.Log("튜토리얼 끝");
        currentTutorial = null;

        LoadingUI.LoadScene("BattleScene"); // 로딩씬으로 이동 후 로딩이 끝나면 nextScene로이동
    }
}
