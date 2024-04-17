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
    /// Slider 를 바로 사용하기 위해서 최소 최대 현재 value를 반환해줘야한다.
    /// </summary>
    #region 프로퍼티
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
