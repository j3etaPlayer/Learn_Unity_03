using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public ItemType[] allowItems = new ItemType[0];

    [NonSerialized] public InventoryObject parent;
    [NonSerialized] public GameObject slotUI;

    public Action<InventorySlot> onPreUpdata;
    public Action<InventorySlot> onPostUpdata;

    public Item item;
    public int amount;

    public ItemObject itemObject
    {
        get
        {
            return item.id >= 0 ? parent.database.itemObjects[item.id] : null;
        }
    }

    #region 생성자
    public InventorySlot() => UpdateSlot(new Item(), 0);
    public InventorySlot(Item item, int amount) => UpdateSlot(item, amount); 
    #endregion

    public void AddItem(Item item, int amount) => UpdateSlot(item, amount);

    public void AddAmount(int value) => UpdateSlot(item, amount += value);
    public void RemoveItem() => UpdateSlot(new Item(), 0);
    void UpdateSlot(Item item, int amount)
    {
        onPostUpdata?.Invoke(this);
        this.item = item;
        this.amount = amount;
        onPostUpdata?.Invoke(this);
    }

    // slot에 아이템을 이동시킬수 있는지 판별하는 함수  true면 이동가능
    public bool CanPlaceInSlot(ItemObject itemObject)
    {
        if (allowItems.Length <= 0 || itemObject == null||itemObject.data.id<0)
        {
            return true;
        }
        foreach(ItemType type in allowItems)
        {
            if(itemObject.type== type) return true;
        }
        return false;
    }
}
