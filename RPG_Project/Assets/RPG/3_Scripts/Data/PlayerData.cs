using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    public Stat[] stats;

    public int MaxHp;
    public int MaxMp;

    public int AttackPower;
    public int DefensePower;

    public Action<PlayerData> OnChangedStats; // BaseValue(PlayerData)�� ������ �ִ� �Լ��� �ݹ����ִ� ��������Ʈ

    public int GetBaseValue(StatType type)
    {
        foreach (Stat stat in stats)
        {
            if (stat.type == type)
            {
                return stat.value.BaseValue;
            }
        }

        Debug.LogError("������ StstType�� �������� �ʽ��ϴ�.");
        return -1;
    }

    public int GetModifiedValue(StatType type)
    {
        foreach (Stat stat in stats)
        {
            if (stat.type == type)
            {
                return stat.value.ModifiedValue;
            }
        }

        Debug.LogError("������ StstType�� �������� �ʽ��ϴ�.");
        return -1;
    }

    public void SetBaseValue(StatType type, int value)
    {
        foreach(Stat stat in stats)
        {
            if(stat.type == type)
            {
                stat.value.BaseValue = value;
            }
        }
    }

    private void OnEnable()
    {
        InitalizeStats();
    }

    private void InitalizeStats()
    {

        foreach(Stat stat in stats)
        {
            stat.value = new Modifier(OnModifiedValue);

        }

        // ��� �⺻ ���� baseValue�� �ʱ�ȭ
        SetBaseValue(StatType.HP, MaxHp);
        SetBaseValue(StatType.Mana, MaxMp);
        SetBaseValue(StatType.Attack, AttackPower);
        SetBaseValue(StatType.Defense, DefensePower);

        // �ʱ⿡ modify ����� ����� �ϴ� ���� �ʱ�ȭ
        MaxHp = GetModifiedValue(StatType.HP);
        MaxMp = GetModifiedValue(StatType.Mana);
        AttackPower = GetModifiedValue(StatType.Attack);
        DefensePower = GetModifiedValue(StatType.Defense);
    }

    private void OnModifiedValue(Modifier value)
    {
        OnChangedStats?.Invoke(this);
    }

}
