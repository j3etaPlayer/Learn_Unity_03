using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieType 
{ 
    small,
    medium,
    big
}
/// <summary>
/// 스크립터블 오브젝트의 데이터를 저장하고 있는 적 클래스를 생성하는 클래스
/// zombie class를 spawn 하는 기능
/// </summary>
public class SpawnZombieData : MonoBehaviour
{
    [SerializeField] private List<ZombieData> zombieDatas;
    [SerializeField] GameObject zombiePrefab;

    private void Start()
    {
        for (int i = 0; i < zombieDatas.Count; i++)
        {
            var zombie = SpawnZombie((ZombieType)i);
            zombie.transform.parent = transform;
        }
    }

    public Zombie SpawnZombie(ZombieType zombieType)
    {
        Zombie newZombie = Instantiate(zombiePrefab).GetComponent<Zombie>();
        newZombie.zombieData = zombieDatas[(int)zombieType];
        newZombie.name = newZombie.zombieData.zombieName;
        newZombie.OnLoadComponents();

        return newZombie;
    }
}
