using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        SerializedProperty sp = NGUIEditorTools.DrawProperty("MapStarName:", serializedObject, "mMapStarName");
        if (sp != null)
        {
            _instance.SetMapName(sp.stringValue);
        }


        if (GUILayout.Button("RefreshMapInfo"))
        {
            _instance.RefreshMap();
        }
        if (GUILayout.Button("GeneraMapFile"))
        {
            SaveMapInfo(_instance.GetASNodes());
        }
        bool value = EditorGUILayout.Toggle("Gizmos : ", _instance.IsShowGizmos);
        if (_instance.IsShowGizmos != value)
        {
            _instance.SetIsShowGizmos(value);
        }



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
        string savePath = Application.dataPath + @"\_Res\Config\";
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        string strFileName = _instance.MapStarName;
        string fullName = savePath + strFileName;

        FileStream fileStream = new FileStream(fullName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        byte[] bytes = Encoding.Default.GetBytes(content);
        fileStream.Write(bytes, 0, bytes.Length);

        fileStream.Close();

        //if (!File.Exists(fullName))
        //{
        //    File.CreateText(fullName);
        //}
        //File.WriteAllText(fullName, content);

        AssetDatabase.Refresh();

    }


    private void CustomUpdate()
    {
        if (_instance != null)
        {

        }
    }



}
