using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamagable
{
    [Header("Status")]
    public int HP;
    public int AttackPower;
    
    public Animator animator;

    public bool isAlive => HP > 0;

    protected virtual void Awake()
    {
        OnLoadComponents();
    }
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {

    }

    public virtual void OnLoadComponents()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void TakeDamage(int damage, Vector3 contactPos, GameObject hitEffectPrefabs = null)
    {
        Debug.Log($"{damage}만큼 피해를 입었다");
        HP -= damage;
    }
}
