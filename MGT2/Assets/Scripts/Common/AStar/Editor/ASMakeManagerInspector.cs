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
            SaveMapInfo(_instance.GetASNodes());
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
        string content = ASMapHelper.ConvertMapToTxt(array);
        System.IO.File.WriteAllText(Application.dataPath + @"\_Res\Config\Astar.txt", content);
        AssetDatabase.Refresh();

    }


    private void CustomUpdate()
    {
        if (_instance != null)
        {

        }
    }


}
