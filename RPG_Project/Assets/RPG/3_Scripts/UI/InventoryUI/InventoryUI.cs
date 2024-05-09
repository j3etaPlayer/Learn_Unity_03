using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ui event가 발생했을 때 마우스의 정보를 저장하는 클래스
/// </summary>
public static class MouseData
{
    public static InventoryUI interfaceMouseOver;
    public static GameObject tempItemDrag;          // 드래그 중인 아이템 정보를 포함하는 오브젝트
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
            Debug.LogWarning("이벤트 트리거가 없습니다");
            return;
        }
        EventTrigger.Entry eventTrigger = new EventTrigger.Entry { eventID = type };
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    
    // 인벤토리 panel ui 이벤트 함수
    private void OnEnterInterface(GameObject gameObject)
    {
        MouseData.interfaceMouseOver = gameObject.GetComponent<InventoryUI>();
    }
    private void OnExitInterface(GameObject gameObject)
    {
        MouseData.interfaceMouseOver = null;
    }

    // slot ui 이벤트 함수
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

        rect.sizeDelta = new Vector2(100/2,100/2); // 드래그 할때는 슬롯 크기의 절반 크기로 설정
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

        // 인벤토리 ui가 아닌 인터페 이스에서 드래그가 끝날때 슬롯 내 아이템 제거
        if (MouseData.interfaceMouseOver == null)
        {
            slotUIs[go].RemoveItem();
        }
        // 인벤토리 UI 안에서 다른 슬롯일 경우 해당 슬롯과 교체
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
            Debug.Log("슬롯이 없습니다");
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
