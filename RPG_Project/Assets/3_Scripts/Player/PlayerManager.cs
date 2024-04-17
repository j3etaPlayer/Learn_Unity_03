using CameraSetting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Player, ISaveManager
{
    [Header("Common Player Data")]
    [HideInInspector] public CharacterController characterController;

    [Header("�÷��̾� ���� ����")]
    public bool isPerformingAction = false;
    public bool applyRootMotion = false;
    public bool canRotate = true;
    public bool canMove = true;
    public bool canCombo = false;
    public bool isDead = false;

    [Header("Player Manager Script")]
    [HideInInspector] public PlayerAnimationManager playerAnimationManager;
    [HideInInspector] public PlayerMovementManager playerMovementManager;
    [HideInInspector] public PlayerAudioManager playerAudioManager;
    public PlayerIngameUI playerIngameUI;
    protected override void Awake()
    {
        base.Awake();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
        playerAudioManager = GetComponent<PlayerAudioManager>();
        characterController = GetComponent<CharacterController>();
        playerIngameUI = GetComponent<PlayerIngameUI>();
    }

    protected override void Start()
    {
        base.Start();
        playerIngameUI.InitializeSlider();
    }
    public void SaveData(ref GameData gameData)   // GameData Ŭ������ �÷��̾��� ���� ��ǥ�� ����
    {
        gameData.x = transform.position.x;
        gameData.y = transform.position.y;
        gameData.z = transform.position.z;
    }

    public void LoadData(GameData gameData)      // GameData Ŭ������ ����� ������ �÷��̾� �����ͷ� ȣ��
    {
        Vector3 loadPlayerPos = new Vector3(gameData.x, gameData.y, gameData.z);

        transform.position = loadPlayerPos;
    }

    public override void TakeDamage(int damage, Vector3 contactPos, GameObject hitEffectPrefabs = null)
    {
        base.TakeDamage(damage, contactPos, hitEffectPrefabs);
        OnChangedStats?.Invoke();

        if (hitEffectPrefabs)   // �ǰ� ��ġ�� �ǰ� ����Ʈ ����
        {
            Instantiate(hitEffectPrefabs, Vector3.zero, Quaternion.identity);   // ������Ʈ Ǯ�� ��ü
        }

        if (isAlive)
        {
            playerAnimationManager.PlayerTargetActionAnimation("Hit", false);
            // vfs -HitEffect manager�� �� ����
        }
        else
        {
            isDead = true;
            animator.CrossFade("Dead", 0.2f);
        }
    }
}
