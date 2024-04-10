using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Security.Cryptography;

public class CSVtoSO
{
    public static string enemyDataCSVPath = "/4_Data/CSV/EnemyCSV.csv";
    public static string saveAssetPath = "Assets/4_Data/Monster";

    [MenuItem("Tools/CSV to ScriptableObject")]
    public static void GenerateEnemyScriptableObjects()
    {
        Debug.Log(Application.dataPath + enemyDataCSVPath);

        string[] allLines = File.ReadAllLines(Application.dataPath + enemyDataCSVPath);
        foreach(string line in allLines)
        {
            string[] splitData = line.Split(',');

            if (splitData.Length != 4)
            {
                Debug.Log("�������� ���� ��ġ���� �ʽ��ϴ�.");
                return;
            }

            ZombieData enemy = ScriptableObject.CreateInstance<ZombieData>();
            enemy.zombieName = splitData[0];
            enemy.HP = int.Parse(splitData[1]);
            enemy.attack = int.Parse(splitData[2]);
            enemy.attackRange = float.Parse(splitData[3]);

            AssetDatabase.CreateAsset(enemy, $"{saveAssetPath}/{enemy.zombieName}.asset");
        }
        AssetDatabase.SaveAssets();
    }
}
