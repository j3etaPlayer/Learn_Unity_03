using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [System.Serializable]
    public struct Speaker
    {
        public Image portraitImage;
        public Image dialougeImage;
        public TextMeshProUGUI speakerName;
        public TextMeshProUGUI dialogue;
        public GameObject arrow;
    }

    [System.Serializable]
    public struct DialogData
    {
        public int speakerIndex;
        public string name;
        public string dialogue;
    }

    [SerializeField] private Speaker[] speakers;
    [SerializeField] private DialogData[] dialogs;

    [SerializeField] private bool isAuto = true; // true�̸� �������ڸ��� ù ��� ���, false�̸� �÷��̾��� �Է��� ���
    private bool isFirst = true;

    private int currentDialogIndex = -1;        // ù ��° ���� 0������ ��µȴ�. ���� ���� ���� �ѹ��� -1�� �ȴ�.
    private int currentSpeakerIndex = 0;        // 0���̸� ĳ���� �̹��� ,1���̸� �Ƿ翧 �̹��� 

    public float textSpeed = 0.1f;
    public bool isTypeing = false;

    public void Setup()
    {
        for (int i = 0; i < speakers.Length; i++)
        {
            // ������Ʈ ����
            SetActiveObjects(speakers[i], false);      // bool���� true�̸� �ش� UI ��� 
            speakers[i].portraitImage.gameObject.SetActive(false);
        }
    }

    public bool UpdateDialog()
    {
        if (isFirst == true)
        {
            Setup();

            if (isAuto)
                SetNextDialog();

            isFirst = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(isTypeing) // Ÿ������ ���� ���� ����
            {
               isTypeing = false;

               //Ÿ���� ��� ���� ��Ų��.
                StopAllCoroutines();
                SoundManager.Instance.StopSFX();
                speakers[currentSpeakerIndex].dialogue.text = dialogs[currentDialogIndex].dialogue; // ��� UI�� ��� ���
                speakers[currentSpeakerIndex].arrow.SetActive(true);   // ���� ��簡 �������� �˸���.
            }

            // ��簡 ���� ���� ��쿡�� ���� ��縦 ����
            if (dialogs.Length > currentDialogIndex + 1)
            {
                SetNextDialog();

            }
            // ��簡 ���� ��� ��� ������Ʈ�� ��Ȱ��ȭ �ϰ� true�� ��ȯ
            else
            {
                for (int i = 0; i < speakers.Length; i++)
                {
                    SetActiveObjects(speakers[i], false);
                    speakers[i].portraitImage.gameObject.SetActive(false);
                }

                return true;
            }
        }

        return false;

    }


    void SetNextDialog()
    {
        SetActiveObjects(speakers[currentSpeakerIndex], false); // ��� ������ �� ���� ��� UI ��Ȱ��ȭ

        currentDialogIndex++;  // ���� dialogueData �˻�

        currentSpeakerIndex = dialogs[currentSpeakerIndex].speakerIndex;

        SetActiveObjects(speakers[currentSpeakerIndex], true); // ���� ����� UI Ȱ��ȭ

        speakers[currentSpeakerIndex].speakerName.text = dialogs[currentDialogIndex].name; // �̸� ���� ��Ī

        //speakers[currentSpeakerIndex].dialogue.text = dialogs[currentDialogIndex].dialogue; // ��� UI�� ��� ���
        StartCoroutine(TypeSentence());
    }

    private void SetActiveObjects(Speaker speaker, bool visible)
    {
        speaker.dialougeImage.gameObject.SetActive(visible);
        speaker.speakerName.gameObject.SetActive(visible);
        speaker.dialogue.gameObject.SetActive(visible);
        speaker.portraitImage.gameObject.SetActive(visible);

        speaker.arrow.SetActive(false);
    }

    IEnumerator TypeSentence()
    {
        int index = 0;
        isTypeing = true;

        while(index <= dialogs[currentDialogIndex].dialogue.Length)
        {
            speakers[currentSpeakerIndex].dialogue.text = dialogs[currentDialogIndex].dialogue.Substring(0, index); // String�� ù ���� ���� ������ ���ڱ��� �ݺ�

            index++;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.TypeText);
            yield return new WaitForSeconds(textSpeed);
            SoundManager.Instance.StopSFX();
        }

        isTypeing = false;

        speakers[currentSpeakerIndex].arrow.SetActive(true);   // ���� ��簡 �������� �˸���.
    }
}
