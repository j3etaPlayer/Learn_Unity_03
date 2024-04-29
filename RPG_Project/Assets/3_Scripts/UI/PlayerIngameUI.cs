using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// player�� HP, Mp, Stamina ���� UI���� ���� �� ��������ִ� Ŭ����
/// </summary>
public class PlayerInGameUI : MonoBehaviour
{
    public PlayerManager player;

    public Image hpSlider;
    public Image mpSlider;

    public GameObject gameOverUI;

    // UI scripts Components
    public PlayerStatsUI playerStatsUI;
    [Header("�κ��丮")]
    public DynamicInventory dynamicInventory;
    public StaticInventory staticInventory;

    public void InitializeSlider()
    {
        hpSlider.fillAmount = (float)player.HP / (float)player.MAXHP;
        mpSlider.fillAmount = (float)player.MP / (float)player.MAXMP;
    }

    private void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            playerStatsUI.gameObject.SetActive(!playerStatsUI.gameObject.activeSelf);
        }
        if(Input.GetKeyDown("o"))
        {
            staticInventory.gameObject.SetActive(!staticInventory.gameObject.activeSelf);
        }
        if (Input.GetKeyDown("p"))
        {
            dynamicInventory.gameObject.SetActive(!dynamicInventory.gameObject.activeSelf);
        }
    }


    private void OnEnable()
    {
        player.OnChangedStats += OnChangedValue;
    }

    private void OnDisable()
    {
        player.OnChangedStats -= OnChangedValue;
    }

    private void OnChangedValue()
    {
        hpSlider.fillAmount = (float)player.HP / (float)player.MAXHP;
        mpSlider.fillAmount = (float)player.MP / (float)player.MAXMP;
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }
}
