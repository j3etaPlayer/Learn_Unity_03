using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerActionManager : MonoBehaviour, ISaveManager
{
    PlayerManager player;

    public float maxDistance = 50f;

    [SerializeField] private InventoryObject equipment;
    [SerializeField] private InventoryObject inventory;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }
    private void Start()
    {
        inventory.onUseItem += OnUseItem;
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // 1. �浹 �˻�
        // ���ǹ� ����ؼ� GroundItem�� �˻��Ͽ�
        // Debug�� �ش� Ŭ������ ������Ʈ �̸��� ����ϵ��� �ۼ��غ�����.
        if (other.CompareTag("Item"))
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            interactable.Interact(gameObject);

            Destroy(other.gameObject);
        }

        if (other.CompareTag("Quest"))
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            interactable.Interact(gameObject);
        }

    }
    private void OnUseItem(ItemObject itemObject)
    {
        // ������ ���� ���
        if (itemObject.type == ItemType.Potion)
        {
            Debug.Log("���� ����");
            return;
        }
        // ���� ����� ����
        foreach (Buff buff in itemObject.data.buffs)
        {
            foreach (Stat stat in player.playerData.stats)
            {
                if (stat.type == buff.type)
                {
                    stat.value.Addmodifier(buff);
                }
            }
        }
    }
    public bool PickupItem(ItemObject itemObject, int amount = 1)
    {
        if (itemObject != null)
        {
            inventory.AddItem(new Item(itemObject), amount);
        }
        return false;
        
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.inventory = inventory.container;
    }

    public void LoadData(GameData gameData)
    {
        //inventory.container = gameData.inventory;
    }
}
