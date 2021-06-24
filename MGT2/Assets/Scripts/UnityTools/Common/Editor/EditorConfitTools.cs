using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class EditorConfitTools
{
    private static List<EditorConfigSettingData> _allDatas = new List<EditorConfigSettingData>();

    static EditorConfitTools()
    {
        InitialSetting();
        //Debug.Log("EditorConfitTools Initialize ");
    }
    public static List<EditorConfigSettingData> GetAllDatas()
    {
        return _allDatas;
    }
    public static void InitialSetting()
    {
        _allDatas.Clear();
        Dictionary<string, string> savDatas = new Dictionary<string, string>();
        Dictionary<string, string> allMaps = new Dictionary<string, string>();
        Array arrs = Enum.GetValues(typeof(EnumLoadSettingPath));
        for (int cnt = 0; cnt < arrs.Length; cnt++)
        {
            string strKey = ((int)(arrs.GetValue(cnt))).ToString();
            EditorConfigSettingData data = new EditorConfigSettingData(strKey, "");
            _allDatas.Add(data);
        }
        LoadLocalMapHelper.InitialMap(GetPathConfig(), ref savDatas);
        foreach (var item in savDatas)
        {
            EditorConfigSettingData data = _allDatas.Find(it => it.Key == item.Key);
            if (data != null)
            {
                data.Value = item.Value;
                data.ChangeValue = item.Value;
            }
        }

    }

    public static void SaveConfig()
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

    public static void SaveConfigByType(EnumLoadSettingPath type, string strValue)
    {
        string strKey = ((int)type).ToString();
        EditorConfigSettingData data = _allDatas.Find(it => it.Key == strKey);
        if (data != null)
        {
            data.Value = strValue;
        }
    }

    public static string GetSavePath(EnumLoadSettingPath type)
    {
        string strKey = ((int)type).ToString();
        EditorConfigSettingData data = _allDatas.Find(it => it.Key == strKey);
        if (data == null || string.IsNullOrEmpty(data.Value))
        {
            Debug.LogError(" 请设置路径  菜单： MGTools->设置->路径设置。" + type);
            return string.Empty;
        }
        return data.Value;
    }


    public static string GetPathConfig()
    {
        return Application.dataPath + "/__TMP/SaveData/ConfigData.txt";
    }

    /// <summary>
    /// 配置设置界面
    /// </summary>
    [MenuItem("MGTools/设置/路径配置")]
    public static void OpenExlImprotSetting()
    {
        //创建窗口
        EditorWindow ew = EditorWindow.GetWindow(typeof(EditorConfigSetting), false, "ExlImprotSetting", true);
        EditorConfigSetting myWindow = (EditorConfigSetting)ew;
        myWindow.Show();//展示

    }
}
