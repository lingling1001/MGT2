using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConfigLoadSetting : EditorWindow
{
    private static List<ConfigLoadSettingData> _allDatas = new List<ConfigLoadSettingData>();
    private void OnEnable()
    {
        InitialSetting();
    }
    private void OnFocus()
    {
        InitialSetting();
    }
    private void OnGUI()
    {
        for (int cnt = 0; cnt < _allDatas.Count; cnt++)
        {
            GUILayout.BeginHorizontal();
            ConfigLoadSettingData data = _allDatas[cnt];
            int key;
            if (!int.TryParse(data.Key, out key))
            {
                continue;
            }
            EnumLoadSettingPath type = (EnumLoadSettingPath)key;

            GUILayout.Label(type.GetDescriptionUIName() + " : ", GUILayout.Width(80));
            data.ChangeValue = GUILayout.TextField(data.ChangeValue);
            if (GUILayout.Button("保存"))
            {
                data.Value = data.ChangeValue;
                SaveSetting();
            }
            if (GUILayout.Button("重置"))
            {
                data.ChangeValue = data.Value;
            }
            GUILayout.EndHorizontal();
        }

    }

    private static void InitialSetting()
    {
        if (_allDatas.Count != 0)
        {
            return;
        }
        Dictionary<string, string> mapDatas = new Dictionary<string, string>();
        LoadLocalMapHelper.InitialMap(GetPathConfig(), ref mapDatas);
        if (mapDatas.Count == 0)
        {
            Array arrs = Enum.GetValues(typeof(EnumLoadSettingPath));
            for (int cnt = 0; cnt < arrs.Length; cnt++)
            {
                string strKey = ((int)(arrs.GetValue(cnt))).ToString();
                ConfigLoadSettingData data = new ConfigLoadSettingData(strKey, "");
                _allDatas.Add(data);
            }
        }
        else
        {
            foreach (var item in mapDatas)
            {
                ConfigLoadSettingData data = _allDatas.Find(it => it.Key == item.Key);
                if (data == null)
                {
                    data = new ConfigLoadSettingData(item.Key, item.Value);
                    _allDatas.Add(data);
                }
            }
        }
    }

    private void SaveSetting()
    {
        Dictionary<string, string> mapDatas = new Dictionary<string, string>();
        for (int cnt = 0; cnt < _allDatas.Count; cnt++)
        {
            if (!mapDatas.ContainsKey(_allDatas[cnt].Key))
            {
                mapDatas.Add(_allDatas[cnt].Key, _allDatas[cnt].Value);
            }
        }
        LoadLocalMapHelper.SaveFile(GetPathConfig(), mapDatas);
    }

    public static string GetSavePath(EnumLoadSettingPath type)
    {
        InitialSetting();

        string strKey = ((int)type).ToString();
        ConfigLoadSettingData data = _allDatas.Find(it => it.Key == strKey);
        if (data == null)
        {
            return string.Empty;
        }
        return data.Value;
    }

    public static string GetPathConfig()
    {
        return Application.dataPath + "/__TMP/SaveData/ConfigData.txt";
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

}
