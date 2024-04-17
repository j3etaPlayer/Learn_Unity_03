using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class NPCBattleUI : MonoBehaviour
{
    [SerializeField]private Slider hpSlider;
    /// <summary>
    /// Slider �� �ٷ� ����ϱ� ���ؼ� �ּ� �ִ� ���� value�� ��ȯ������Ѵ�.
    /// </summary>
    #region ������Ƽ
    public float MinValue
    {
        get => hpSlider.minValue;
        set => hpSlider.minValue = value;
    }
    public float maxValue
    {
        get => hpSlider.maxValue;
        set => hpSlider.maxValue = value;
    }
    public float value
    {
        get => hpSlider.value;
        set => hpSlider.value = value;
    }
    #endregion

}
