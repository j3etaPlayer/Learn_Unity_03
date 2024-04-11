using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// ��ǥ������ �����ϴ� Ŭ����, index�� �ִ밡 �Ǹ� 0���� �ʱ�ȭ �ȴ�.
/// </summary>
public class SimplePatrol : MonoBehaviour
{
    [SerializeField] private Transform[] paths; // �ν����� â���� ����������Ѵ�.
    private int currentPath = 0;
    public float moveSpeed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (paths[currentPath].position - transform.position).normalized;

        transform.position = transform.position + direction * moveSpeed * Time.deltaTime;

        // if (Vector3.Distance(paths[currentPath].position, transform.position) < 0.1f)

        if ((paths[currentPath].position - transform.position).sqrMagnitude < 0.1f)     // Vector3.Distance()���� ������.
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
