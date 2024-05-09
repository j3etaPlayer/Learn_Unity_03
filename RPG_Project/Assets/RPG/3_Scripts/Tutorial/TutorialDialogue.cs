using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueSystem))]
public class TutorialDialogue : TutorialBase
{
    private DialogueSystem dialogueSystem;
    public override void Enter()
    {
        dialogueSystem = GetComponent<DialogueSystem>();
        dialogueSystem.Setup();                         // UI셋팅
    }

    public override void Execute(TutorialController controller)
    {
        bool isCompeted = dialogueSystem.UpdateDialog();      // false, true 반환 dialogue data가 최대 인덱스가 되면 true를 반환
        if (isCompeted)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        
    }

}
