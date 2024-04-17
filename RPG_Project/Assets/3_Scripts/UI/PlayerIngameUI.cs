using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// player�� hp, mp, stamina ���� ���� ������� �� ����Ǵ� �ڵ�
/// </summary>
public class PlayerIngameUI : MonoBehaviour
{
    public PlayerManager playerManager;
    public Image hpSlider;
    public Image mpSlider;

    public GameObject gameOverUI;
public void InitializeSlider()
    {
        hpSlider.fillAmount = (float)playerManager.HP / (float)playerManager.maxHP;
        mpSlider.fillAmount = (float)playerManager.MP / (float)playerManager.maxMP;
    }

    private void OnEnable()
    {
        playerManager.OnChangedStats += OnChangedValue;
    }

    private void OnDisable()
    {
        playerManager.OnChangedStats -= OnChangedValue;
    }

    private void OnChangedValue()
    {
        hpSlider.fillAmount = (float)playerManager.HP / (float)playerManager.maxHP;
        mpSlider.fillAmount = (float)playerManager.MP / (float)playerManager.maxMP;
    }
    
}
