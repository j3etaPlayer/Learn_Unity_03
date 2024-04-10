using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 싱글톤 패턴을 구현해서 반복작업을 줄이기 위한 클래스 
/// 인스턴스를 생성하고 게임 실행중에 파괴되지 않도록 보
/// </summary>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T: Component
{
    private static T instance;
    public static T Instance => instance;

    public static T GetOrCreateInstance()
    {
        if (instance == null)
        {
            instance = FindAnyObjectByType(typeof(T)) as T; // Get

            if (instance == null)
            {
                GameObject newGameObject = new GameObject(typeof(T).Name, typeof(T));
                instance = newGameObject.AddComponent<T>();
            }

            return instance;
        }

        return instance;
    }

    protected virtual void Awake()
    {
        instance = this as T;

        if (Application.isPlaying == true)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}