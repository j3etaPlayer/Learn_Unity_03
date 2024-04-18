using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    public Stat[] stats;

    public int MaxHp;
    public int MaxMp;

    public int AttackPower;
    public int DefensePower;

    public Action<PlayerData> OnChangedStats;   // baseValue�� ������ �ִ� �Լ��� �ݹ����ִ� ��������Ʈ

    //public int GetHP
    //{
    //    get
    //    {
    //        foreach (Stat stat in stats)
    //        {
    //            if (stat.type == StatType.HP)
    //            {
    //                MaxHp = stat.value.ModifiedValue;
    //            }
    //        }
    //            return MaxHp;
    //    }
    //}

    public int GetBaseValue(StatType type)
    {
        foreach (Stat stat in stats)
        {
            if (stat.type == type)
            {
                return stat.value.BaseValue;
            }
        }

        Debug.LogError("������ statType�� �������� �ʽ��ϴ�.");
        return -1;
    }
    public int GetModifierdValue(StatType type)
    {
        foreach (Stat stat in stats)
        {
            if (stat.type == type)
            {
                return stat.value.ModifiedValue;
            }
        }

        Debug.LogError("������ statType�� �������� �ʽ��ϴ�.");
        return -1;
    }
    public void SetBaseValue(StatType type, int value)
    {
        foreach (Stat stat in stats)
        {
            if (stat.type == type)
            {
                stat.value.BaseValue = value;
            }
        }
    }
    private void OnEnable()
    {
        InitaizeStats();
    }
    private void InitaizeStats()
    {
        foreach (Stat stat in stats)
        {
            stat.value = new Modifier(OnModifiedValue);
        }
        // ��� �⺻���� basevalue�� �ʱ�ȭ
        SetBaseValue(StatType.HP, MaxHp);
        SetBaseValue(StatType.Mana, MaxMp);
        SetBaseValue(StatType.Attack, AttackPower);
        SetBaseValue(StatType.Defense, DefensePower);

        // �ʱ⿡ modify ����� �ؾ��ϴ� ���� �ʱ�ȭ
        MaxHp = GetModifierdValue(StatType.HP);
        MaxMp = GetModifierdValue(StatType.Mana);
        AttackPower = GetModifierdValue(StatType.Attack);
        DefensePower = GetModifierdValue(StatType.Defense);
            

    }
    private void OnModifiedValue(Modifier value)
    {
        OnChangedStats?.Invoke(this);
    }

}
