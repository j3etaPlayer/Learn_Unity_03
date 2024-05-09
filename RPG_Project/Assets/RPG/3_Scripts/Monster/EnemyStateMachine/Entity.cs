using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���͸� ��ӹ��� ���� �ƴ϶� �÷��̾ ����� �� �ְ�.
/// </summary>
public class Entity : MonoBehaviour, IDamagable
{
    [Header("Status")]
    public int HP;
    public int MAXHP;
    public int MP;
    public int MAXMP;
    public int AttackPower;

    public Animator animator;

    public bool IsAlive => HP > 0;

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

    private void OnInitialize() // ������ �� �����͸� �ʱ�ȭ ���ִ� �Լ�
    {
        HP = MAXHP;
        MP = MAXMP / 2;
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
