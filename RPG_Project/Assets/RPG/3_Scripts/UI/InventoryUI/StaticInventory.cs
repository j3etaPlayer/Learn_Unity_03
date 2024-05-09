using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInventory : InventoryUI
{
    public GameObject[] staticSlot = null;
    public override void CreateSlotUIs()
    {
        slotUIs = new Dictionary<GameObject, InventorySlot> ();
        for(int i = 0; i < inventoryObject.slots.Length; i++)
        {
            GameObject go = staticSlot[i];

            AddEvent(go, EventTriggerType.PointerEnter, delegate { OnEnter(go); });
            AddEvent(go, EventTriggerType.PointerExit, delegate { OnExit(go); });
            AddEvent(go, EventTriggerType.BeginDrag, delegate { OnStartDrag(go); });
            AddEvent(go, EventTriggerType.Drag, delegate { OnDrag(go); });
            AddEvent(go, EventTriggerType.EndDrag, delegate { OnEndDrag(go); });

            inventoryObject.slots[i].slotUI = go;
            slotUIs.Add(go, inventoryObject.slots[i]);
        }
    }
}
