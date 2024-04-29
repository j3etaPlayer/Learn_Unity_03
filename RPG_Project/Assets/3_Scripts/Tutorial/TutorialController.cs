using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public PlayerManager player;
    [SerializeField] private List<TutorialBase> tutorials; // �ν����� â���� Ʃ�丮�� Ŭ������ �޾ƿͼ� ������ ����Ʈ

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
        currentTutorial?.Execute(this); // Ʃ�丮�� ������ ������ update�� �����϶�
    }
    public void SetNextTutorial()  // Ŀ��Ʈ Ʃ�丮���� ����� ������Ѷ�
    {
        if (currentTutorial != null) currentTutorial.Exit();

        if (currentIndex > tutorials.Count -1)
        {
            CompleteTutorial();
            return;
        }
        // ���� Ʃ�丮�� ����

        currentIndex++;
        currentTutorial = tutorials[currentIndex];


        currentTutorial.Enter();
    }
    void CompleteTutorial()
    {
        Debug.Log("Ʃ�丮�� ��");
        currentTutorial = null;

        LoadingUI.LoadScene("BattleScene"); // �ε������� �̵� �� �ε��� ������ nextScene���̵�
    }
}
