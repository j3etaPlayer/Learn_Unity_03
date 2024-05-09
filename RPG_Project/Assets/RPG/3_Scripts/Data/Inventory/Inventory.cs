using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �κ��丮�� �����͸� ���ų�, �κ��丮 �ȿ� �����Ͱ� �ִ��� Ȯ���ϴ� Ŭ����
/// </summary>

[System.Serializable]
public class Inventory
{
    public InventorySlot[] slots = new InventorySlot[16];

    public void Clear() //�κ��丮 ���� ��� �����͸� ���� �Լ�
    {
        foreach (var slot in slots)
        {
            slot.RemoveItem();
        }
    }
    public bool IsContain(ItemObject itemObject)
    {
        return IsContain(itemObject.data.id);
    }
    public bool IsContain(int id)
    {
        return slots.FirstOrDefault(slot => slot.item.id == id) != null;
    }
}
