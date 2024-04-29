using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDialogue : SingletonMonoBehaviour<WorldDialogue>
{
    public string jsonName;

    // 외부 클래스에서 이 클래스의 데이터를 참조하기 위한 콜랙션 : Dictionary
    public Dictionary<int, List<DialogueSystem.DialogData>> dialogueDatabase
        = new Dictionary<int, List<DialogueSystem.DialogData>>();


    protected override void Awake()
    {
        base.Awake();       // singtonmonobehaviour의 awake함수 실행

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

            // 1. 현재 번호 0 == 인덱스 번호 0 은 서로 일치한다.
            if (indexID == int.Parse((string)jObj["indexNumber"]))
            {
                // database key값에 해당하는 list메모리가 할당되어 있지 않다면 할당해줘야 한다.
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
