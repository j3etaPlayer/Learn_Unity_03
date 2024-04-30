using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public int maxCount = int.MaxValue;         // 오브젝트 풀링의 최대치 설정
    private int allocateCount;                  // 허용(활성화) 가능한 숫자
    private int activeCount;                    // 현재 활성화 중인 숫자
    private int increaseCount = 5;              // 풀링할 오브젝트 수가 부족할 때 추가로 생성할 숫자

    private GameObject poolObject;
    private List<GameObject> poolObjList;       // 현재 활성화된 오브젝트
    // private Stack<GameObject> pushedObject;  // 비활성화된 오브젝트 모음

    private static Transform Container;         // 생성될 Instant를 보관할 컨테이너 게임오브젝트 위치

    public ObjectPool() { }                     // 기본 생성자

    public ObjectPool(GameObject prefab)
    {
        allocateCount = 0;
        activeCount = 0;
        poolObject = prefab;
        poolObjList = new List<GameObject>();

        if (Container == null)
        {
            GameObject container = new GameObject();

            container.name = "Container";
            Container = container.transform;
        }

        InstantiateObject();
    }

    private void InstantiateObject()    // increaseCount만큼 오브젝트를 생성
    {
        allocateCount += increaseCount;

        for (int i = 0; i < allocateCount; i++)
        {
            GameObject newObject = GameObject.Instantiate(poolObject);
            newObject.SetActive(false);
            poolObjList.Add(newObject);

            newObject.transform.parent = Container;
        }
    }
    public void DestroyAllObject() // List에 있는 모든 오브젝트를 파괴하고 리스트를 Clear
    {
        if (poolObjList == null)
        {
            Debug.Log("생성된 오브젝트 풀이 없습니다.");
            return;
        }

        for (int i = 0; i < poolObjList.Count; i++)
        {
            GameObject.Destroy(poolObjList[i].gameObject);
        }

        poolObjList.Clear();
    }
    public void DestroyObject()
    {
        // 5초동안 새로운 오브젝트가 생성되지 않으면....
        // 오브젝트를 삭제하여 최초의 오브젝트 갯수로 되돌린다...
    }

    public GameObject ActivatePoolItem()    // 비활성화된 오브젝트를 활성화 시켜준다.
    {
        if (poolObjList == null) return null;

        // 첫번째 조건 : 허용가능한 숫자와 활성화된 숫자가 같을때
        if (allocateCount == activeCount)
        {
            InstantiateObject();
        }

        // 오브젝트를 할당된 만큼 활성화 시켜준다.
        for (int i = 0; i < poolObjList.Count; i++)
        {
            GameObject newGameObject = poolObjList[i].gameObject;

            if (newGameObject.activeSelf == false)
            {
                activeCount++;
                newGameObject.SetActive(true);
                
                return newGameObject;
            }
        }
        return null;
    }



    public void DeActivatePoolItem(GameObject removeObject)    // 오브젝트가 파괴되는 대신 비활성화 해준다.
    {
        if(poolObject == null || removeObject == null) return;

        for(int i=0; i<poolObjList.Count;i++)
        {
            GameObject newGameObject = poolObjList[i].gameObject;

            if (newGameObject == removeObject)
            {
                activeCount--;
                newGameObject.SetActive(false);

                return;
            }
        }
    }

    #region Helper
    public void DeActivateAllPoolItem()                     // 모든 오브젝트를 비활성화 해주는 오브젝트
    {
        if (poolObject == null) return;
        
        for (int i = 0; i < poolObjList.Count; i++)
        {
            if (poolObjList[i] != null && poolObjList[i].activeSelf == true)
            {
                poolObjList[i].SetActive(false);
            }
        }
        activeCount = 0;
    }
    #endregion

}
