using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamagable
{
    [Header("Status")]
    public int HP;
    public int maxHP;
    public int MP;
    public int maxMP;
    public int AttackPower;
    
    public Animator animator;

    public bool isAlive => HP > 0;

    protected virtual void Awake()
    {
        OnLoadComponents();
    }
    protected virtual void Start()
    {
        OnInitialize();
    }
    protected virtual void Update()
    {

    }
    private void OnInitialize() // 시작할때 데이터를 초기화하는 함수
    {
        HP = maxHP;
        MP = maxMP;

    }
    public virtual void OnLoadComponents()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void TakeDamage(int damage, Vector3 contactPos, GameObject hitEffectPrefabs = null)
    {
        HP -= damage;
    }
}
