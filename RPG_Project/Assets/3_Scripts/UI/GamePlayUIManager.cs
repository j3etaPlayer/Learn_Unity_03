using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayUIManager : MonoBehaviour
{
    // �̱��� �Ŵ���

    [SerializeField] private GameObject UIGameObject;

    private bool isOpen = false; // True�̸� UIManager�� ȣ���ϰ�, false�̸� �ݴ´�.


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC ��ư�� ������ �� Setting ȭ���� ȣ���ϰ� �ݴ� �ڵ�
        {
            isOpen = !isOpen;
            CallSettingUI(isOpen);
        }
    }

    private void CallSettingUI(bool isOpen)
    {
        UIGameObject.SetActive(isOpen);
    }

    public void CloseUIMenu()
    {
        UIGameObject.SetActive(false);
    }

    public void ReturnToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
