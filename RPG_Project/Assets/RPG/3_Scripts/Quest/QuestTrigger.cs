using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour, IInteractable
{
    public Quest myQuest = new Quest();
    public int questId;

    [SerializeField] private DialogueSystem inGameDialogueSystem;
    bool nowSpeaking = false;

    void Start()
    {
        myQuest = QuestManager.Instance.questDatabase[questId];

        QuestManager.Instance.onCompleteQuest += OnCompleteQuest;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            QuestManager.Instance.ProcessQuest(myQuest.type, questId);
        }
    }

    public void Interact(GameObject gameObject)
    {
        if (nowSpeaking) return;

        if (QuestManager.Instance.questDatabase[myQuest.id].type == QuestType.Ending && myQuest.status == QuestStatus.Rewarded)
        {
            Ending.Instance.PlayEndingScene();
        }

        if (QuestManager.Instance.questDatabase[myQuest.id].status == QuestStatus.None)
        {
            QuestManager.Instance.LoadQuestUI(myQuest, false);

            inGameDialogueSystem.indexNumber = myQuest.startIndexNumber;
            inGameDialogueSystem.Setup();
            StartCoroutine(InGameDialogue(inGameDialogueSystem, QuestStatus.Accepted));
        }
        else if (QuestManager.Instance.questDatabase[myQuest.id].status == QuestStatus.Completed)
        {
            inGameDialogueSystem.indexNumber = myQuest.completeIndexNumber;
            inGameDialogueSystem.Setup();
            StartCoroutine(InGameDialogue(inGameDialogueSystem, QuestStatus.Rewarded));
        }
        else if (QuestManager.Instance.questDatabase[myQuest.id].status == QuestStatus.Rewarded)
        {
            inGameDialogueSystem.indexNumber = myQuest.endIndexNumber;
            inGameDialogueSystem.Setup();
            StartCoroutine(InGameDialogue(inGameDialogueSystem, QuestStatus.Rewarded));
        }
    }
    IEnumerator InGameDialogue(DialogueSystem text, QuestStatus status)
    {
        nowSpeaking = true;
        yield return new WaitUntil(() => text.UpdateDialog() == true);
        if (text.UpdateDialog())
        {
            QuestManager.Instance.questDatabase[myQuest.id].status = status;
            nowSpeaking = false;
        }
    }
    private void OnCompleteQuest(Quest quest)
    {
        if (quest.id == myQuest.id && quest.status == QuestStatus.Completed)
        {
            QuestManager.Instance.LoadQuestUI(quest, true);
        }

    }
}
