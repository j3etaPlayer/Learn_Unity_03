using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : SingletonMonoBehaviour<QuestManager>
{
    public Quest[] questData;

    public string QuestJsonName = "Quest";

    public Dictionary<int, Quest> questDatabase = new Dictionary<int, Quest>();

    [Header("Quest UI")]
    public GameObject questGameObject;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    public Action<Quest> onCompleteQuest;

    protected override void Awake()
    {
        base.Awake();
        LoadJson();
    }

    private void Start()
    {
        questData = new Quest[questDatabase.Count];
        questDatabase.Values.CopyTo(questData, 0);

        LoadQuestUI(questDatabase[100], false);
    }


    public void LoadJson()
    {
        string jstring = JsonLoader.LoadData(QuestJsonName);

        JArray jArray = JArray.Parse(jstring);

        int curentId = 0;

        foreach (JObject jObj in jArray)
        {
            int id = int.Parse((string)jObj["id"]);
            int targetId = int.Parse((string)jObj["targetId"]);
            int count = int.Parse((string)jObj["count"]);
            int targetCount = int.Parse((string)jObj["targetCount"]);

            QuestStatus status = EnumUtil<QuestStatus>.Parse((string)jObj["status"]);
            QuestType type = EnumUtil<QuestType>.Parse((string)jObj["type"]);

            int rewardEXP = int.Parse((string)jObj["rewardEXP"]);
            int rewardGold = int.Parse((string)jObj["rewardGold"]);
            string rewardItemId = (string)jObj["rewardItemId"];

            QuestReward reward = new QuestReward(rewardEXP, rewardGold, rewardItemId);
            
            int startIndexNumber = int.Parse((string)jObj["startIndexNumber"]);
            int completeIndexNumber = int.Parse((string)jObj["completeIndexNumber"]);
            int endIndexNumber = int.Parse((string)jObj["endIndexNumber"]);

            string title = (string)jObj["title"];
            string description = (string)jObj["description"];


            Quest newQuest = new Quest(id, targetId, count, targetCount, status, type, reward, title, description, startIndexNumber, completeIndexNumber, endIndexNumber);
            
            if (curentId != int.Parse((string)jObj["id"]))
            {
                curentId = int.Parse((string)jObj["id"]);
            }
            questDatabase[curentId] = newQuest;

        }


    }
    public void LoadQuestUI(Quest quest, bool complete)
    {
        if(questGameObject.activeSelf == false) questGameObject.SetActive(true);

        titleText.text = quest.title;
        descriptionText.text = quest.description;

        if(complete)
        {
            descriptionText.text = "<color=red><s>" + descriptionText.text + "</s></color>";
        }

    }

    public void ProcessQuest(QuestType type, int targetId)
    {
        foreach (Quest quest in questData)
        {
            if (quest.status == QuestStatus.Accepted && quest.type == type && quest.targetId == targetId)
            {
                quest.count++;

                if (quest.count >= quest.targetCount)
                {
                    quest.status = QuestStatus.Completed;

                    onCompleteQuest?.Invoke(quest);
                }
            }
        }
    }
}
