using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// 목표지점을 순찰하는 클래스, index가 최대가 되면 0으로 초기화 된다.
/// </summary>
public class SimplePatrol : MonoBehaviour
{
    [SerializeField] private Transform[] paths; // 인스펙터 창에서 연결해줘야한다.
    private int currentPath = 0;
    public float moveSpeed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (paths[currentPath].position - transform.position).normalized;

        transform.position = transform.position + direction * moveSpeed * Time.deltaTime;

        // if (Vector3.Distance(paths[currentPath].position, transform.position) < 0.1f)

        if ((paths[currentPath].position - transform.position).sqrMagnitude < 0.1f)     // Vector3.Distance()보다 빠르다.
        {
            if (currentPath < paths.Length - 1)
            {
                currentPath++;
            }
            else
                currentPath = 0;
        }
    }
}
