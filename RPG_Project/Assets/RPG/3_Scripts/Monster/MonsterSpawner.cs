using ObjectPoolTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public string monsterName;
    private string monsterPath = "Prefabs/Monster/";

    private ObjectPool objectPool;

    private void Awake()
    {
        GameObject asset = Resources.Load<GameObject>($"{monsterPath + monsterName}");
        objectPool = new ObjectPool(asset);

    }

    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newObject = objectPool.ActivatePoolItem();

            StartCoroutine(DeActivateMonster(newObject));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            objectPool.DeActivateAllPoolItem();
        }
    }
    IEnumerator DeActivateMonster(GameObject newObject)
    {
        yield return new WaitForSeconds(2f);

        objectPool.DeActivatePoolItem(newObject);
    }
}
