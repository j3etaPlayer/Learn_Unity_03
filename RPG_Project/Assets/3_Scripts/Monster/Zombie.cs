using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable 오브젝트의 데이터를 저장하기 위한 클래스
/// </summary>
public class Zombie : MonoBehaviour
{
    public ZombieData zombieData;

    [Header("좀비의 능력치")]
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
