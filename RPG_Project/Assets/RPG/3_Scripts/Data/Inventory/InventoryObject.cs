using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Search;
using UnityEngine;

public enum InterfaceType
{
    Equipment, Inventory, QuickSlot, 
}

[CreateAssetMenu(fileName = "new Inventory", menuName = "Data/Inventory")]
[System.Serializable]
public class InventoryObject : ScriptableObject
{
    public ItemDatabase database;
    public InterfaceType type;

    // equipment�� ���� ������ inventory �� ���� ������ ������ �ٸ��� ������ ������ ������ ��� container����
    [SerializeField] public Inventory container = new Inventory();

    public InventorySlot[] slots => container.slots;
    public Action<ItemObject> onUseItem;

    // ���� ������ ����ִ��� �ƴ��� �Ǵ��ؾ��Ѵ�.
    public int emptySlotCount
    {
        get
        {
            int count = 0;
            foreach (var slot in slots)
            {
                if (slot.item.id < 0) // id�� -1�̸� �����Ͱ� ���� ��
                {
                    count++;
                }
            }
            return count;
        }
    }

    public bool AddItem(Item item, int amount)
    {
        // ����ִ� ������ ������ AddItem Return������Ѵ�.
        if (emptySlotCount <= 0) return false;

        // ���� ���� �������� �ִ��� Ȯ���ϴ� ���
        InventorySlot slot = FindItemInventory(item);
        if (database.itemObjects[item.id].stackable || slot == null)
        {
            if (emptySlotCount <= 0) return false;
            // ����ִ� ������ ���Կ� ������ �� �߰��ϴ� ���
            GetEmptySlot().AddItem(item, amount);
        }
        else
        {
            //�����ۿ� ������ �׾��ִ� ���
            slot.AddAmount(amount);
        }
        return true;
    }
    // ���� Ŭ����, �κ��丮 ����� �����ִ� �Լ�
    private InventorySlot FindItemInventory(Item item)
    {
        return slots.FirstOrDefault(slot=>slot.item.id == item.id);
    }

    private InventorySlot GetEmptySlot()
    {
        return slots.FirstOrDefault(slot => slot.item.id < 0);
    }
    public bool IsContainItem(ItemObject itemObject)
    {
        return slots.FirstOrDefault(slot => slot.item.id == itemObject.data.id) != null;
    }
    public void SwapItem(InventorySlot slot1, InventorySlot slot2)
    {
        if (slot1 == slot2) return;
        if(slot2.CanPlaceInSlot(slot1.itemObject) && slot1.CanPlaceInSlot(slot2.itemObject))
        {
            InventorySlot tempSlot = new InventorySlot(slot2.item, slot2.amount);
            slot2.AddItem(slot1.item, slot1.amount);
            slot1.AddItem(tempSlot.item, tempSlot.amount); 
        }
    }
    public void UseItem(InventorySlot slotToUse)
    {
        // �κ��丮 ���Կ� �ִ� �������� ���
        if (slotToUse.itemObject == null || slotToUse.item.id < 0 || slotToUse.amount <= 0)
        {
            return;
        }
        ItemObject itemObject = slotToUse.itemObject;
        slotToUse.AddItem(slotToUse.item, slotToUse.amount - 1);

        onUseItem.Invoke(itemObject);
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        container.Clear();
    }

    public string savePath;
    [ContextMenu("Save")]

    public void SaveData()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(Application.persistentDataPath + "/" + savePath + ".txt", FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, container);
        stream.Close();
    }
    [ContextMenu("Load")]
    public void LoadData()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(Application.persistentDataPath + "/" + savePath + ".txt", FileMode.Open, FileAccess.Read);
        Inventory newContainer = formatter.Deserialize(stream) as Inventory;

        for(int i = 0; i<slots.Length; i++)
        {
            slots[i].AddItem(newContainer.slots[i].item, newContainer.slots[i].amount);
        }
        stream.Close();
    }
}
