using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ASMakeManager))]
public class ASMakeManagerInspector : Editor
{

    private ASMakeManager _instance;

    private void OnEnable()
    {
        _instance = target as ASMakeManager;
        EditorApplication.update += CustomUpdate;
    }
    private void OnDisable()
    {
        EditorApplication.update -= CustomUpdate;
    }



    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("RefreshMapInfo"))
        {
            _instance.RefreshMap();
        }
        if (GUILayout.Button("GeneraMapFile"))
        {
            SaveMapInfo(_instance.Map);
        }
        _instance.IsShowGizmos = EditorGUILayout.Toggle("Gizmos : ", _instance.IsShowGizmos);



    }
    /// <summary>
    /// 后期文件大可以用二进制表示，暂时无需求
    /// </summary>
    private void SaveMapInfo(ASNode[,] array)
    {
        if (array == null)
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        List<string> lines = new List<string>();

        for (int i = 0; i < array.GetLength(0); i++)
        {
            sb.Remove(0, sb.Length);
            for (int j = 0; j < array.GetLength(1); j++)
            {
                sb.Append((array[i, j].CanWalk) ? "1" : "0");
            }
            lines.Add(sb.ToString());
        }

        System.IO.File.WriteAllLines(Application.dataPath + @"\_Res\Config\Astar.txt", lines);
        AssetDatabase.Refresh();

    }


    private void CustomUpdate()
    {
        if (_instance != null)
        {

        }
    }


}
