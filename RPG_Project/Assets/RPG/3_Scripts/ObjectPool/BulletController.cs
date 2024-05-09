using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolTest
{

	public class BulletController : MonoBehaviour
	{
		[SerializeField] private GameObject projectile;

		public KeyCode bulletSpawnKey; // ���ϴ� Ű �Ҵ�
        private ObjectPool objectPool;
        
        private void Awake()
        {
            objectPool = new ObjectPool(projectile);    // 5 ���� ������ ������ ��Ȱ��ȭ
        }

        private void Update()
        {
            if (Input.GetKey(bulletSpawnKey))   // " " Ű �Է½�
            {
                // GameObject clone = Instantiate(projectile, transform.position, Quaternion.identity);

                // ������ �� AcitvatePoolItem�� �̿��ؼ� �̸� ������ ������Ʈ Ǯ ���
                GameObject clone = objectPool.ActivatePoolItem();
                

                clone.GetComponent<Projectile>().SetUp(objectPool);
            }

            if (Input.GetKey(KeyCode.A))    // AŰ�Է½� ��ü������Ʈ ��Ȱ��ȭ
            {
                objectPool.DeActivateAllPoolItem();
            }
        }
    }

}