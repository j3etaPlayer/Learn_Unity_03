using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class JSONtoSO
{
    public static string jsonName = "Monster";
    public static string saveAssetPath = "Assets/4_Data/Monster";

    [MenuItem("Tools/JSON to ScriptableObject")]
    public static void GenerateEnemySOFromJson()
    {
        string jsonString = JsonLoader.LoadData(jsonName);

        JArray jArr = JArray.Parse(jsonString);

        foreach (JObject jObj in jArr)
        {
            ZombieData enemy = ScriptableObject.CreateInstance<ZombieData>();
            enemy.zombieName = (string)jObj["zombieName"];
            enemy.HP = int.Parse((string)jObj["HP"]);
            enemy.Attack = int.Parse((string)jObj["Attack"]);
            enemy.attackRange = int.Parse((string)jObj["attackRange"]);

            AssetDatabase.CreateAsset(enemy, $"{saveAssetPath}/{enemy.zombieName}.asset");
        }
        AssetDatabase.SaveAssets();

        EditorUtility.DisplayDialog("Complete", "몬스터 SO Data 생성 완료\n" + saveAssetPath, "OK");
    }
}
