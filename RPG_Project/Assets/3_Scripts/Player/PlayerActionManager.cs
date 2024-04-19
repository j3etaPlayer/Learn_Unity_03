using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    PlayerManager player;
    public float maxDistace = 10f;

    public Camera camera;

    public LayerMask actionMask;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            foreach (Buff buff in item.itemObject.data.buffs)
            {
                foreach (Stat stat in player.playerData.stats)
                {
                    if (stat.type == buff.type)
                    {
                        stat.value.AddModifier(buff);
                    }

                }

            }


            Destroy(item.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;

        bool isHit = Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxDistace, actionMask);

        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(camera.transform.position, camera.transform.forward * hit.distance);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(camera.transform.position, camera.transform.forward * maxDistace);

        }
    }
}
