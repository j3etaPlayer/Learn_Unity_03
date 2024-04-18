using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCBattleUI : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;

    /// <summary>
    /// Slider�� ü�� �ٷ� ����ϱ� ���ؼ� �ּ�, �ִ�, ���� Value�� ��ȯ����� �Ѵ�.
    /// </summary>
    #region ������Ƽ
    public float MinValue
    {
        get => hpSlider.minValue;
        set => hpSlider.minValue = value;
    }

    public float MaxValue
    {
        get => hpSlider.maxValue;
        set => hpSlider.maxValue = value;
    }

    public float Value
    {
        get => hpSlider.value;
        set => hpSlider.value = value;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
