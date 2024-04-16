using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    bool isAlive {  get; }
    void TakeDamage(int damage, Vector3 contactPos, GameObject hitEffectPrefabs = null);
}
