using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolTest
{

    public class Projectile : MonoBehaviour
    {
        private ObjectPool objectPool;

        private void Awake()
        {

        }
        public void SetUp(ObjectPool objectPool)
        {
            this.objectPool = objectPool;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("DestroyZone"))
            {
                // Destroy(gameObject)
                objectPool.DeActivatePoolItem(gameObject);
            }
        }
    }

}