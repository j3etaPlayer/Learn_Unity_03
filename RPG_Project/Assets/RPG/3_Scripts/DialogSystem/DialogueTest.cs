using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTest : MonoBehaviour
{
    // 2. json�� gamedata�� ��ȯ�� dialoguesystem�� dialogue data�� deserialize(������ȭ) �Ͽ� ����غ���
    public Image textUI;
    public string jsonName;

    void Start()
    {
        TextAsset jsondata = Resources.Load<TextAsset>("JsonData/Dialogue");

        Sprite sprite = Resources.Load<Sprite>("UI/G02B_sword");
        textUI.sprite = sprite;

        LoadJson();
        // GameObject zombie = Resources.Load<GameObject>("Prefabs/Monster/Zombie");
        // if (null == zombie as GameObject) { Debug.LogError("Resources�� ��ΰ� �߸��Ǿ����ϴ�."); }
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

            Debug.Log($"���� ī��Ʈ{count} : {dialogData.name}, {dialogData.dialogue}");
        }
    }

}