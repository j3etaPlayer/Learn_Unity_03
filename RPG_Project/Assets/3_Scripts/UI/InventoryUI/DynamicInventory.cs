using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EventSystems;
using UnityEngine.EventSystems;

public class DynamicInventory : InventoryUI
{
    [SerializeField] protected GameObject slotPrefab;
    [Min(1), SerializeField] protected int numberofColumn = 4;
    public Transform slotSpawn;

    public override void CreateSlotUIs()
    {
        slotUIs = new Dictionary<GameObject, InventorySlot>();

        for(int i = 0; i<inventoryObject.slots.Length; i++)
        {
            GameObject go = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, transform);
            go.transform.SetParent(slotSpawn);

            inventoryObject.slots[i].slotUI = go;
            slotUIs.Add(go, inventoryObject.slots[i]);

            AddEvent(go, EventTriggerType.PointerEnter, delegate { OnEnter(go); });
            AddEvent(go, EventTriggerType.PointerExit, delegate { OnExit(go); });
            AddEvent(go, EventTriggerType.BeginDrag, delegate { OnStartDrag(go); });
            AddEvent(go, EventTriggerType.Drag, delegate { OnDrag(go); });
            AddEvent(go, EventTriggerType.EndDrag, delegate { OnEndDrag(go); });

            AddEvent(go, EventTriggerType.PointerClick, (data) => OnClick(go, (PointerEventData)data));

            go.name += ": " + i;
        }
    }
    protected override void OnRightClick(InventorySlot slot)
    {
        inventoryObject.UseItem(slot);
    }
    
}
