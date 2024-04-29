using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JsonLoader
{
    public static string LoadData(string jsonName)
    {
        TextAsset jText = Resources.Load<TextAsset>($"JsonData/{jsonName}");
        if (jText != null)
        {
            string jString = jText.ToString();
            return jString;
        }
        else
        {
            Debug.LogError("json ��ΰ� �߸��Ǿ����ϴ�.");
            return string.Empty;
        }
    }
}
