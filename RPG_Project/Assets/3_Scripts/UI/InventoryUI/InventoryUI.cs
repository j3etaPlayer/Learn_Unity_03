using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ui event�� �߻����� �� ���콺�� ������ �����ϴ� Ŭ����
/// </summary>
public static class MouseData
{
    public static InventoryUI interfaceMouseOver;
    public static GameObject tempItemDrag;          // �巡�� ���� ������ ������ �����ϴ� ������Ʈ
    public static GameObject slotHoveredOver;
}

public abstract class InventoryUI : MonoBehaviour
{
    public InventoryObject inventoryObject;

    public Dictionary<GameObject, InventorySlot> slotUIs = new Dictionary<GameObject, InventorySlot>();

    private void Awake()
    {
        CreateSlotUIs();
        for (int i = 0; i < inventoryObject.slots.Length; i++)
        {
            inventoryObject.slots[i].parent = inventoryObject;
            inventoryObject.slots[i].onPostUpdata += OnPostUpdate;
        }
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });

    }



    private void Start()
    {
        for (int i = 0; i < inventoryObject.slots.Length; i++)
        {
            inventoryObject.slots[i].AddItem(inventoryObject.slots[i].item, inventoryObject.slots[i].amount);
        }
    }

    public abstract void CreateSlotUIs();

    protected void AddEvent(GameObject gameObject, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
        if (!trigger)
        {
            Debug.LogWarning("�̺�Ʈ Ʈ���Ű� �����ϴ�");
            return;
        }
        EventTrigger.Entry eventTrigger = new EventTrigger.Entry { eventID = type };
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    
    // �κ��丮 panel ui �̺�Ʈ �Լ�
    private void OnEnterInterface(GameObject gameObject)
    {
        MouseData.interfaceMouseOver = gameObject.GetComponent<InventoryUI>();
    }
    private void OnExitInterface(GameObject gameObject)
    {
        MouseData.interfaceMouseOver = null;
    }

    // slot ui �̺�Ʈ �Լ�
    public void OnEnter(GameObject gameObject)
    {
        MouseData.slotHoveredOver = gameObject;
        MouseData.interfaceMouseOver = GetComponentInParent<InventoryUI>();
    }
    public void OnExit(GameObject gameObject)
    {
        MouseData.slotHoveredOver = null;
    }
    public void OnStartDrag(GameObject gameObject)
    {
        MouseData.tempItemDrag = CreatDragImage(gameObject);

    }
    private GameObject CreatDragImage(GameObject gameObject)
    {
        if (slotUIs[gameObject].item.id < 0) return null;
        GameObject dragImage = new GameObject();
        RectTransform rect = dragImage.AddComponent<RectTransform>();

        rect.sizeDelta = new Vector2(100/2,100/2); // �巡�� �Ҷ��� ���� ũ���� ���� ũ��� ����
        dragImage.transform.SetParent(transform.parent);
        Image image = dragImage.AddComponent<Image>();
        image.sprite = slotUIs[gameObject].itemObject.icon;
        image.raycastTarget = false;

        dragImage.name = "Drag Image";

        return dragImage;

    }
    public void OnDrag(GameObject go)
    {
        if(MouseData.tempItemDrag == null) return;

        MouseData.tempItemDrag.GetComponent<RectTransform>().position = Input.mousePosition;
    }
    public void OnEndDrag(GameObject go)
    {
        Destroy(MouseData.tempItemDrag);

        // �κ��丮 ui�� �ƴ� ������ �̽����� �巡�װ� ������ ���� �� ������ ����
        if (MouseData.interfaceMouseOver == null)
        {
            slotUIs[go].RemoveItem();
        }
        // �κ��丮 UI �ȿ��� �ٸ� ������ ��� �ش� ���԰� ��ü
        else if (MouseData.slotHoveredOver)
        {
            InventorySlot mouseSlotData = MouseData.interfaceMouseOver.slotUIs[MouseData.slotHoveredOver];

            inventoryObject.SwapItem(slotUIs[go], mouseSlotData);
        }
    }

    public void OnClick(GameObject go, PointerEventData data)
    {
        InventorySlot slot = slotUIs[go];
        if (slot == null)
        {
            Debug.Log("������ �����ϴ�");
            return;
        }
        if (data.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick(slot);
        }
        else if (data.button == PointerEventData.InputButton.Right)
        {
            OnRightClick(slot);
        }
    }
    protected virtual void OnRightClick(InventorySlot slot)
    {

    }
    protected virtual void OnLeftClick(InventorySlot slot)
    {

    }

    public void OnPostUpdate(InventorySlot slot)
    {
        if (slot == null || slot.slotUI == null) return;

        Image slotImage = slot.slotUI.transform.GetChild(0).GetComponent<Image>();

        slotImage.sprite = slot.item.id < 0 ? null : slot.itemObject.icon;

        slotImage.color = slot.item.id < 0 ? new Color(1,1,1,0) : new Color(1,1,1,1);

        slot.slotUI.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.id < 0 ? string.Empty : (slot.amount == 1 ? string.Empty : "x" + slot.amount.ToString("n0"));
    }
}
