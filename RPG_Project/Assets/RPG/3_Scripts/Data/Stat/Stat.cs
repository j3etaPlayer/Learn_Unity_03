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
    [NonSerialized] private int baseValue;                     // �⺻ ������
    [SerializeField] private int modifiedValue;                // �߰��� ������

    private event Action<Modifier> OnModifyValue;
    private List<IModifier> modifiers = new List<IModifier>(); // modifier�� ������ �������̽� ������ ���� �����ߴٰ� baseValue�� ��������ִ� ����Ʈ

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
        // modifieValue�� �����Ű�� �̺�Ʈ�� ������ִ� �Լ� ȣ��
        RegisterModifierEvent(method);
    }

    public void RegisterModifierEvent(Action<Modifier> method)
    {
        if(method!= null)
        {
            OnModifyValue += method;
        }
    }

    public void UnRegisterModifierEvent(Action<Modifier> method)
    {
        if(method!= null)
        {
            OnModifyValue -= method;
        }
    }

    /// <summary>
    /// �������̽� Modifier�� ����ϰ� �ִ� �����Ͱ� �ִٸ�, �ش� ������ ������ Stat�� �������� �� ���Ž����ش�.
    /// </summary>
    private void UpdateModifiedValue()
    {
        int valueToAdd = 0;

        foreach(IModifier modifier in modifiers)
        {
            modifier.AddValue(ref valueToAdd);
        }

        ModifiedValue = baseValue + valueToAdd;

        OnModifyValue?.Invoke(this);
    }

    /// <summary>
    /// ������ ����, ���� ȹ�� �� Stat�� ���� ���ش�
    /// </summary>
    public void Addmodifier(IModifier modifier)
    {
        modifiers.Add(modifier);
        UpdateModifiedValue();
    }

    /// <summary>
    /// ������ ����, ���� ���� �� Stat ����
    /// </summary>
    public void RemoveModifier(IModifier modifier)
    {
        modifiers.Remove(modifier);
        UpdateModifiedValue();
    }
}
