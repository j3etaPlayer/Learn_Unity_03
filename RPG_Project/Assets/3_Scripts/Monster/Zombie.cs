using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable ������Ʈ�� �����͸� �����ϱ� ���� Ŭ����
/// </summary>
public class Zombie : MonoBehaviour
{
    public ZombieData zombieData;

    [Header("������ �ɷ�ġ")]
    public string ZombieName;
    public int HP;
    public int Attack;
    public float AttackRange;

    public void OnLoadComponents()
    {
        ZombieName = zombieData.zombieName;
        HP = zombieData.HP;
        Attack = zombieData.attack;
        AttackRange = zombieData.attackRange;
    }
}
