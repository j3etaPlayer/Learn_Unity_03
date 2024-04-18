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

    public Action<PlayerData> OnChangedStats;   // baseValue에 영향을 주는 함수에 콜백해주는 델리게이트

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

        Debug.LogError("지정한 statType이 존재하지 않습니다.");
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

        Debug.LogError("지정한 statType이 존재하지 않습니다.");
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
        // 모든 기본스텟 basevalue로 초기화
        SetBaseValue(StatType.HP, MaxHp);
        SetBaseValue(StatType.Mana, MaxMp);
        SetBaseValue(StatType.Attack, AttackPower);
        SetBaseValue(StatType.Defense, DefensePower);

        // 초기에 modify 계산을 해야하는 스텟 초기화
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
