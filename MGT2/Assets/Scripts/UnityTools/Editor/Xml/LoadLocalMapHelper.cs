using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using MFrameWork;
/// <summary>
/// 本地配置Map读取， key|value  必须是这样的格式保存
/// </summary>
public class LoadLocalMapHelper
{

    public static void InitialMap(string path, ref Dictionary<string, string> map)
    {
        InitialMap(path, ref map, Encoding.UTF8);
    }
    /// <summary>
    /// 加载本地配置文件 到Map中，
    /// </summary>
    public static void InitialMap(string path, ref Dictionary<string, string> map, Encoding encoding)
    {
        if (map == null)
        {
            return;
        }
        if (!File.Exists(path))
        {
            map.Clear();
            return;
        }
        string[] lines = File.ReadAllLines(path, encoding);
        if (lines.Length == 0)
        {
            map.Clear();
        }
        for (int cnt = 0; cnt < lines.Length; cnt++)
        {
            string[] strs = Utility.Xml.ParseString<string>(lines[cnt], Utility.Xml.SplitVerticalBar);
            if (strs == null || strs.Length != 2)
            {
                return;
            }
            if (map.ContainsKey(strs[0]) || string.IsNullOrEmpty(strs[0]))
            {
                continue;
            }
            map.Add(strs[0], strs[1]);
        }
    }
    public static void SaveFile(string path, Dictionary<string, string> map)
    {
        SaveFile(path, map, Encoding.UTF8);
    }

    public static void SaveFile(string path, Dictionary<string, string> map, Encoding encoding)
    {
        string[] lines = new string[map.Count];
        int index = 0;
        foreach (var item in map)
        {
            lines[index] = item.Key + "|" + item.Value;
            index++;
        }
        string strPath = Path.GetDirectoryName(path);
        if (Directory.Exists(strPath))
        {
            Directory.CreateDirectory(strPath);
        }
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        System.IO.File.WriteAllLines(path, lines, encoding);
        AssetDatabase.Refresh();
    }

    public static void ClearHashData(string savePath)
    {
        SaveFile(savePath, new Dictionary<string, string>());
    }

}
