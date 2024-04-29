using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDialogue : SingletonMonoBehaviour<WorldDialogue>
{
    public string jsonName;

    // �ܺ� Ŭ�������� �� Ŭ������ �����͸� �����ϱ� ���� �ݷ��� : Dictionary
    public Dictionary<int, List<DialogueSystem.DialogData>> dialogueDatabase
        = new Dictionary<int, List<DialogueSystem.DialogData>>();


    protected override void Awake()
    {
        base.Awake();       // singtonmonobehaviour�� awake�Լ� ����

        LoadJson();
    }
    void LoadJson()
    {
        string jsonString = JsonLoader.LoadData(jsonName);

        JArray jArr = JArray.Parse(jsonString);

        int indexID = 0;
        foreach (JObject jObj in jArr)
        {
            DialogueSystem.DialogData dialogData
                = JsonConvert.DeserializeObject<DialogueSystem.DialogData>(jObj.ToString());

            // 1. ���� ��ȣ 0 == �ε��� ��ȣ 0 �� ���� ��ġ�Ѵ�.
            if (indexID == int.Parse((string)jObj["indexNumber"]))
            {
                // database key���� �ش��ϴ� list�޸𸮰� �Ҵ�Ǿ� ���� �ʴٸ� �Ҵ������ �Ѵ�.
                if (!dialogueDatabase.ContainsKey(indexID))
                {
                    dialogueDatabase[indexID] = new List<DialogueSystem.DialogData>();
                }
                dialogueDatabase[indexID].Add(dialogData);
            }
            else
            {
                indexID = int.Parse((string)jObj["indexNumber"]);
                dialogueDatabase[indexID] = new List<DialogueSystem.DialogData>() { dialogData };
            }
        }
    }
}
