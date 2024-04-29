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
        dialogueSystem.Setup();                         // UI����
    }

    public override void Execute(TutorialController controller)
    {
        bool isCompeted = dialogueSystem.UpdateDialog();      // false, true ��ȯ dialogue data�� �ִ� �ε����� �Ǹ� true�� ��ȯ
        if (isCompeted)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        
    }

}
