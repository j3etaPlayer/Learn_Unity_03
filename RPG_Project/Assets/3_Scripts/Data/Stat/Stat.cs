using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    HP, Mana, Attack, Defense
}

[System.Serializable]
public class Stat
{
    public StatType type;
    public Modifier value;
}

[System.Serializable]
public class Modifier
{
    [NonSerialized]private int baseValue;       // 초기데이터
    [SerializeField]private int modifiedValue;  // 추가된 데이터

    private event Action<Modifier> OnModifiuValue;
    private List<IModifier> modifiers = new List<IModifier>();      // modifier로 지정된 인터페이스 벨류를 전부 보관했다가 baseValue에적용시켜주는 리스트

    public int BaseValue
    {
        get => baseValue;
        set 
        {
            baseValue = value;
            UpdateModifiedValue();
        }
    }
    public int ModifiedValue
    {
        get => modifiedValue;
        set => modifiedValue = value;
    }

    public Modifier(Action<Modifier> method = null)
    {
        ModifiedValue = baseValue;
    }
    public void RegisterModifieEvent(Action<Modifier> method)
    {
        if (method != null)
        {
            OnModifiuValue += method;
        }
    }
    public void UnRegisterModifieEvent(Action<Modifier> method)
    {
        if (method != null)
        {
            OnModifiuValue -= method;
        }
    }
    /// <summary>
    /// 인터페이스 modifier를 상속하고 있는 데이터가 있다면, 해당 데이터 벨류를 stat에 변경해주고 갱신시킨다.
    /// </summary>
    private void UpdateModifiedValue()
    {
        int valueToAdd = 0;
        foreach(IModifier modifier in modifiers)
        {
            modifier.AddValue(ref valueToAdd);
        }
        modifiedValue = baseValue + valueToAdd;

        OnModifiuValue?.Invoke(this);
    }
    /// <summary>
    /// 아이템 장착 및 버프 획득시 Stat을 갱신시킨다.
    /// </summary>
    private void AddModifier(IModifier modifier)
    {
        modifiers.Add(modifier);
        UpdateModifiedValue();
    }
    /// <summary>
    /// 아이템 해제 및 버프 종료시 Stat을 갱신시킨다.
    /// </summary>
    private void RemoveModifier(IModifier modifier)
    {
        modifiers.Remove(modifier);
        UpdateModifiedValue();
    }
}