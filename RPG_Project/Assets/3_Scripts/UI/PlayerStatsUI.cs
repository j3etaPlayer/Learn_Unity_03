using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    public PlayerData playerData;
    public InventoryObject equipMent;

    public TextMeshProUGUI[] statText;             // 0: MaxHp, 1: MaxMp, 2: AttackPower, 3: DefensePower

    // ScriptableObject�� �ڵ带 �ۼ��� �� Reference�� �����͸� �����Ű�� �ʰ� �ϱ� ���ؼ� �����͸� Instance(Copy)�ؼ� ����Ѵ�.
    private void Awake()
    {
        //playerData = Instantiate(playerData) as PlayerData;

        if (equipMent == null || playerData == null)
        {
            Debug.LogError("�κ��丮 �Ǵ� �÷��̾� ������ ������Ʈ�� null���� Ȯ�����ּ���");
            return;
        }
        foreach (var slot in equipMent.slots)
        {
            slot.onPreUpdata += OnRemoveItem;
            slot.onPostUpdata += OnEquipItem;
        }
    }
    public void OnEquipItem(InventorySlot slot)
    {
        if (slot.itemObject == null) return;

        if (slot.parent.type == InterfaceType.Equipment)
        {
            foreach (var buff in slot.item.buffs)
            {
                foreach (var stat in playerData.stats)
                {
                    if (stat.type == buff.type)
                    {
                        stat.value.Addmodifier(buff);
                    }
                }
            }
        }

    }
    public void OnRemoveItem(InventorySlot slot)
    {
        if (slot.itemObject == null) return;

        if (slot.parent.type == InterfaceType.Equipment)
        {
            foreach (var buff in slot.item.buffs)
            {
                foreach (var stat in playerData.stats)
                {
                    if (stat.type == buff.type)
                    {
                        stat.value.RemoveModifier(buff);
                    }
                }
            }
        }
    }


    private void Update()
    {
       UpdateStatText();
    }

    private void OnEnable()
    {
        playerData.OnChangedStats += OnChangedStats;
    }


    private void UpdateStatText()
    {
        statText[0].text = playerData.GetModifiedValue(StatType.HP).ToString();
        statText[1].text = playerData.GetModifiedValue(StatType.Mana).ToString();
        statText[2].text = playerData.GetModifiedValue(StatType.Attack).ToString();
        statText[3].text = playerData.GetModifiedValue(StatType.Defense).ToString();
    }

    public void OnChangedStats(PlayerData playerData)
    {
        UpdateStatText();
    }

}   
