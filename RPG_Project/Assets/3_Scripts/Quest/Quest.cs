using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public enum QuestStatus
{
    None, Accepted, Completed, Rewarded,
}
public enum QuestType
{
    KillEnemy, BringItem,
}
[System.Serializable]
public class QuestReward
{
    public int rewardEXP;
    public int rewardGold;
    public string rewardItemId;

    public QuestReward() { }

    public QuestReward(int rewardEXP, int rewardGold, string rewardItemId)
    {
        this.rewardEXP = rewardEXP;
        this.rewardGold = rewardGold;
        this.rewardItemId = rewardItemId;
    }
}

[System.Serializable]
public class Quest
{
    public int id;          // database�� ����� key��
    public int targetId;    // ��Ī��ų id�� ������ ����

    public int count;       // ����Ʈ ���� ��Ȳ
    public int targetCount; // ����Ʈ ��ǥ

    public QuestStatus status;
    public QuestType type;
    public QuestReward reward;

    [Header("UI")]
    public string title;
    public string description;


    public Quest() { }

    public Quest(int id, int targetId, int count, int targetCount, QuestStatus status, QuestType type, 
                QuestReward reward, string title, string description)
    {
        this.id = id;
        this.targetId = targetId;
        this.count = count;
        this.targetCount = targetCount;
        this.status = status;
        this.type = type;
        this.reward = reward;
        this.title = title;
        this.description = description;
    }
}
