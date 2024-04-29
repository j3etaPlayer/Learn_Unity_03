using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ʃ�丮��� ����� ��� Ŭ������ �߻� Ŭ������ ���
/// </summary>
public abstract class TutorialBase : MonoBehaviour
{
    public abstract void Enter();

    public abstract void Execute(TutorialController controller);

    public abstract void Exit();
}
