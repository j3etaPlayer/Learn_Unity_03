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
    [NonSerialized]private int baseValue;       // �ʱⵥ����
    [SerializeField]private int modifiedValue;  // �߰��� ������

    private event Action<Modifier> OnModifiuValue;
    private List<IModifier> modifiers = new List<IModifier>();      // modifier�� ������ �������̽� ������ ���� �����ߴٰ� baseValue����������ִ� ����Ʈ

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
    /// �������̽� modifier�� ����ϰ� �ִ� �����Ͱ� �ִٸ�, �ش� ������ ������ stat�� �������ְ� ���Ž�Ų��.
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
    /// ������ ���� �� ���� ȹ��� Stat�� ���Ž�Ų��.
    /// </summary>
    private void AddModifier(IModifier modifier)
    {
        modifiers.Add(modifier);
        UpdateModifiedValue();
    }
    /// <summary>
    /// ������ ���� �� ���� ����� Stat�� ���Ž�Ų��.
    /// </summary>
    private void RemoveModifier(IModifier modifier)
    {
        modifiers.Remove(modifier);
        UpdateModifiedValue();
    }
}