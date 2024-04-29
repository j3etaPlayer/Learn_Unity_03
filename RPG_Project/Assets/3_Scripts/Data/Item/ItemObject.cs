using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// slot���� �������� ������ �� type�� �Ǻ��ϱ� ���� �뵵
/// </summary>
public enum ItemType
{
    Helmet = 0,
    Boots,
    Weapon,
    Shield,
    Potion,
    Default
}


[CreateAssetMenu(fileName = "New Item", menuName = "Data/New Item")]
public class ItemObject : ScriptableObject
{
    public ItemType type;             // ��� ������ ����
    public bool stackable;            // ��ø ���� ����

    public Sprite icon;               // �κ��丮 ������ ������
    public GameObject modelPrefab;    // �� ������ ������

    public Item data = new Item();
}
