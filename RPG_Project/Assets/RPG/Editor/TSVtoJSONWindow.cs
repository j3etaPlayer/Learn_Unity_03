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
            if (_tsvCopyValue.EndsWith(Environment.NewLine)) // 복사한 줄의 마지막 줄이 새로운 라인이 있는경우
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

                EditorUtility.DisplayDialog("Complete", "변환이 완료되었습니다\n" + _savePath, "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Fail", "변환이 실패하였습니다\n" + _savePath, "OK");
            }
        }
    }

}
