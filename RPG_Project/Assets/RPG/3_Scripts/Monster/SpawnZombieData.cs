using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ZombieType { small, medium, big}

/// <summary>
/// ��ũ���ͺ� ������Ʈ�� �����͸� �����ϰ� �ִ� �� Ŭ������ �����ϴ� Ŭ����
/// zombie class�� spawn�ϴ� ���
/// </summary>
public class SpawnZombieData : MonoBehaviour
{
    [SerializeField] private List<ZombieData> zombieDatas;
    [SerializeField] GameObject zombiePrefab;
    
    private void Start()
    {
        for(int i =0; i< zombieDatas.Count; i++)
        {
            var zombie = SpawnZombie((ZombieType)i);
            zombie.transform.parent = transform;        // SpawnZombieData�� ������Ʈ�� ������ �ִ� ������Ʈ�� �ڽ����� ���� �����ȴ�.
        }
    }


    public Zombie SpawnZombie(ZombieType zombieType)
    {
        Zombie newZombie = Instantiate(zombiePrefab).GetComponent<Zombie>();
        newZombie.zombieData = zombieDatas[(int)zombieType];                 // ���� Ÿ�Կ� �´� ���� ������ �Ҵ�                           
        newZombie.name = newZombie.zombieData.zombieName;                    // ������ ���ӿ�����Ʈ �̸� ����
        newZombie.OnLoadComponents();                                        // ������ �ʱ�ȭ

        return newZombie;
    }
}
