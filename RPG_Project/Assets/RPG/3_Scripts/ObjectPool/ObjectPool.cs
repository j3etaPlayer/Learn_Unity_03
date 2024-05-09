using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public int maxCount = int.MaxValue;         // ������Ʈ Ǯ���� �ִ�ġ ����
    private int allocateCount;                  // ���(Ȱ��ȭ) ������ ����
    private int activeCount;                    // ���� Ȱ��ȭ ���� ����
    private int increaseCount = 5;              // Ǯ���� ������Ʈ ���� ������ �� �߰��� ������ ����

    private GameObject poolObject;
    private List<GameObject> poolObjList;       // ���� Ȱ��ȭ�� ������Ʈ
    // private Stack<GameObject> pushedObject;  // ��Ȱ��ȭ�� ������Ʈ ����

    private static Transform Container;         // ������ Instant�� ������ �����̳� ���ӿ�����Ʈ ��ġ

    public ObjectPool() { }                     // �⺻ ������

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

    private void InstantiateObject()    // increaseCount��ŭ ������Ʈ�� ����
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
    public void DestroyAllObject() // List�� �ִ� ��� ������Ʈ�� �ı��ϰ� ����Ʈ�� Clear
    {
        if (poolObjList == null)
        {
            Debug.Log("������ ������Ʈ Ǯ�� �����ϴ�.");
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
        // 5�ʵ��� ���ο� ������Ʈ�� �������� ������....
        // ������Ʈ�� �����Ͽ� ������ ������Ʈ ������ �ǵ�����...
    }

    public GameObject ActivatePoolItem()    // ��Ȱ��ȭ�� ������Ʈ�� Ȱ��ȭ �����ش�.
    {
        if (poolObjList == null) return null;

        // ù��° ���� : ��밡���� ���ڿ� Ȱ��ȭ�� ���ڰ� ������
        if (allocateCount == activeCount)
        {
            InstantiateObject();
        }

        // ������Ʈ�� �Ҵ�� ��ŭ Ȱ��ȭ �����ش�.
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



    public void DeActivatePoolItem(GameObject removeObject)    // ������Ʈ�� �ı��Ǵ� ��� ��Ȱ��ȭ ���ش�.
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
    public void DeActivateAllPoolItem()                     // ��� ������Ʈ�� ��Ȱ��ȭ ���ִ� ������Ʈ
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
