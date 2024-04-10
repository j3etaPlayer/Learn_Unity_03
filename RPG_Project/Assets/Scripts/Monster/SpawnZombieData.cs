using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieType { small, meduim, big }

public class Zombie
{
    public int HP;
    public int attack;
    public float attackRange;

    public Zombie(ZombieData data)
    {
        HP = data.HP;
        attack = data.attack;
        attackRange = data.attackRange;
    }
}

public class SpawnZombieData : MonoBehaviour
{
    [SerializeField] private ZombieData zombieData;
    public ZombieType zombieType;
    private void Update()
    {
        SpawnRandomZombie(zombieData);
    }

    public void SpawnRandomZombie(ZombieData zombieData)
    {
        Zombie zombie = new Zombie(zombieData);
        Debug.Log("좀비의 체력 : " + zombie.HP);
        Debug.Log("좀비의 체력 : " + zombie.attack);
        Debug.Log("좀비의 체력 : " + zombie.attackRange);
    }
}
