using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Helmet = 0,
    Boots,
    Weapon,
    Default
}

[CreateAssetMenu(fileName = "New Item", menuName = "Data/New Item")]
public class ItemObject : ScriptableObject
{
    public ItemType type;           // �������� ����
    static bool stackable;          // ��ø ���� ����

    public Sprite icon;             // �κ��丮 ������ ������
    public GameObject modelPrefab;  // ������ �� ������ ������

    public Item data = new Item();
}
