using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorConfigSetting : EditorWindow
{
    private static List<EditorConfigSettingData> _allDatas = new List<EditorConfigSettingData>();

    private void OnGUI()
    {
        List<EditorConfigSettingData> allDatas = EditorConfitTools.GetAllDatas();
        for (int cnt = 0; cnt < allDatas.Count; cnt++)
        {
            EditorConfigSettingData data = allDatas[cnt];
            int key;
            if (!int.TryParse(data.Key, out key))
            {
                continue;
            }
            EnumLoadSettingPath type = (EnumLoadSettingPath)key;
            string strDes = type.GetDescriptionUIName();
            if (string.IsNullOrEmpty(strDes))
            {
                continue;
            }
            GUILayout.BeginHorizontal();
            GUILayout.Label(strDes + " : ", GUILayout.Width(80));
            data.ChangeValue = EditorGUILayout.TextField(data.ChangeValue);
            //if (GUILayout.Button("保存"))
            //{
            //    data.Value = data.ChangeValue;
            //    EditorConfitTools.SaveConfig();
            //}
            if (GUILayout.Button("重置"))
            {
                data.ChangeValue = data.Value;
            }
            GUILayout.EndHorizontal();
        }
        //EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("刷新"))
        {
            EditorConfitTools.InitialSetting();
        }
        if (GUILayout.Button("保存"))
        {
            for (int cnt = 0; cnt < allDatas.Count; cnt++)
            {
                allDatas[cnt].Value = allDatas[cnt].ChangeValue;
            }
            EditorConfitTools.SaveConfig();
        }
        //EditorGUILayout.EndHorizontal();
    }





}
public enum EnumLoadSettingPath
{
    /// <summary>
    /// xml 配置路径 外部
    /// </summary>
    [AttributeUIName("xml外部路径")]
    XmlExternal,
    /// <summary>
    /// xml 配置路径 内部
    /// </summary>
    [AttributeUIName("xml内部路径")]
    XmlInternal,
    /// <summary>
    /// svn 配置路径
    /// </summary>
    [AttributeUIName("svn 配置")]
    SVNConfigTools,
    /// <summary>
    /// svn 配置路径
    /// </summary>
    [AttributeUIName("临时配置")]
    SVNTempSave,
}
