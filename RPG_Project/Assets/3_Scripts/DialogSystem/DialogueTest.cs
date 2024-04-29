using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTest : MonoBehaviour
{
    // 2. json을 gamedata로 변환후 dialoguesystem의 dialogue data로 deserialize(역직렬화) 하여 사용해보기
    public Image textUI;
    public string jsonName;

    void Start()
    {
        TextAsset jsondata = Resources.Load<TextAsset>("JsonData/Dialogue");

        Sprite sprite = Resources.Load<Sprite>("UI/G02B_sword");
        textUI.sprite = sprite;

        LoadJson();
        // GameObject zombie = Resources.Load<GameObject>("Prefabs/Monster/Zombie");
        // if (null == zombie as GameObject) { Debug.LogError("Resources의 경로가 잘못되었습니다."); }
        // else { Instantiate(zombie); }
    }
    void Update()
    {
        
    }
    void LoadJson()
    {
        string jsonString = JsonLoader.LoadData(jsonName);

        JArray jArr = JArray.Parse(jsonString);

        int count = 0;
        foreach (JObject jObj in jArr)
        {
            DialogueSystem.DialogData dialogData = JsonConvert.DeserializeObject<DialogueSystem.DialogData>(jObj.ToString());
            count++;

            Debug.Log($"현재 카운트{count} : {dialogData.name}, {dialogData.dialogue}");
        }
    }

}