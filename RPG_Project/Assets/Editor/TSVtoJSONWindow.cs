using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TSVtoJSONWindow : EditorWindow
{
    [MenuItem("Tools/JsonUtility/TSVtoJSONWindow")]
    public static void CreatWindow()
    {
        GetWindow<TSVtoJSONWindow>("TSV To JSON");
    }

    bool _parseNumber = false;

    string _savePath;

    string _tsvCopyValue;
    Vector2 _scrollPos;

    private void OnGUI()
    {
        _parseNumber = EditorGUILayout.Toggle("Parse Number", _parseNumber);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("TSV Text", EditorStyles.boldLabel);
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
        _tsvCopyValue = EditorGUILayout.TextArea(_tsvCopyValue, GUILayout.MinHeight(300));
        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Parse", GUILayout.Height(50)))
        {
            if (_tsvCopyValue.EndsWith(Environment.NewLine)) // ������ ���� ������ ���� ���ο� ������ �ִ°��
            {
                _tsvCopyValue = _tsvCopyValue.Substring(0, _tsvCopyValue.Length - 1);
            }


            Parse(_tsvCopyValue);
            _tsvCopyValue = "";
            Repaint();
        }
        
        void Parse(string tsvCopyValue)
        {
            string json = TSVtoJSONUtility.ParseTsv(_tsvCopyValue, _parseNumber);

            if(!string.IsNullOrEmpty(json))
            {
                _savePath = EditorUtility.SaveFilePanel("Save Json", Application.dataPath, "Default Name", "json");

                File.WriteAllText(_savePath, json);
                AssetDatabase.Refresh();

                EditorUtility.DisplayDialog("Complete", "��ȯ�� �Ϸ�Ǿ����ϴ�\n" + _savePath, "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Fail", "��ȯ�� �����Ͽ����ϴ�\n" + _savePath, "OK");
            }
        }
    }

}
