using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Text;
using MFrameWork;

public class ConfigLoadExtension
{

    private static Dictionary<string, string> _mapSaveHash = new Dictionary<string, string>();

    /// <summary>
    /// 导入多张测试表， 自选路径
    /// </summary>
    public static void RefreshAllConfig()
    {
        string path = EditorConfitTools.GetSavePath(EnumLoadSettingPath.XmlExternal);
        string pathInternal = EditorConfitTools.GetSavePath(EnumLoadSettingPath.XmlInternal);
        if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(pathInternal))
        {
            return;
        }
        CheckSaveMap();

        LoadChangeData(new string[] { path }, pathInternal);
    }
    /// <summary>
    /// 导入多张测试表， 自选路径
    /// </summary>
    public static void ExlChangeImportMultiple()
    {
        ////桌面路径。
        //string strDesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        //string path = EditorUtility.OpenFolderPanel("SelectFolder", strDesktopPath, "new2");
        //if (string.IsNullOrEmpty(path))
        //{
        //    return;
        //}
        //LoadChangeData(new string[] { path });
    }


    /// <summary>
    /// 清空保存变化文件配置
    /// </summary>
    public static void ClearSaveChangeData()
    {
        _mapSaveHash.Clear();
        LoadLocalMapHelper.SaveFile(GetSavePath(), _mapSaveHash);
    }



    /// <summary>
    /// 加载更改文件
    /// </summary>
    /// <param name="dirs">目录列表</param>
    /// <param name="ignoreFileName">忽略文件的名字</param>
    public static void LoadChangeData(string[] dirs, string saveDir, params string[] ignoreFileName)
    {
        List<FileInfo> listFiles = new List<FileInfo>();
        for (int i = 0; i < dirs.Length; i++)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirs[i]);
            FileInfo[] fileList = dirInfo.GetFiles();
            for (int x = 0; x < fileList.Length; x++)
            {
                if (ignoreFileName != null)
                {
                    bool isContinue = false;
                    for (int cnt = 0; cnt < ignoreFileName.Length; cnt++)
                    {
                        if (fileList[x].FullName == ignoreFileName[cnt])
                        {
                            isContinue = true;
                            break;
                        }
                    }
                    if (isContinue)
                    {
                        continue;
                    }
                }
                listFiles.Add(fileList[x]);
            }
        }
        LoadChangeData(saveDir, listFiles.ToArray(), ignoreFileName);
    }

    public static void LoadChangeData(string saveDir, FileInfo[] fileList, params string[] ignoreFileName)
    {
        try
        {
            for (int i = 0; i < fileList.Length; i++)
            {
                EditorUtility.DisplayProgressBar("import all data", string.Format("import Num : {0} / {1}  ", i, fileList.Length) +
                    fileList[i].Name, (float)(i + 1) / (float)fileList.Length);
                if (fileList[i].Extension == ".xml")
                {
                    string strName = fileList[i].Name;
                    string filePath = saveDir + "/" + strName;
                    string hash = HashHelper.ComputeSHA1(fileList[i].FullName);
                    if (_mapSaveHash.ContainsKey(strName))
                    {
                        if (hash == _mapSaveHash[strName])
                        {
                            //Debug.Log("配置无变化 忽略 " + strName);
                            continue;
                        }
                        _mapSaveHash[strName] = hash;
                    }
                    else
                    {
                        _mapSaveHash.Add(strName, hash);
                    }
                    string strPath = fileList[i].FullName;
                    string strFileSave = saveDir + "\\" + fileList[i].Name;
                    string strContent = string.Empty;
                    File.Copy(strPath, strFileSave, true);
                    Debug.Log(" 刷新配置 " + strFileSave + "  strPath " + strPath);
                }
            }
            AssetDatabase.Refresh();
            EditorUtility.ClearProgressBar();
            LoadLocalMapHelper.SaveFile(GetSavePath(), _mapSaveHash);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }


    }

    private static void CheckSaveMap()
    {
        if (_mapSaveHash.Count != 0)
        {
            _mapSaveHash.Clear();
        }
        LoadLocalMapHelper.InitialMap(GetSavePath(), ref _mapSaveHash);
    }
    public static string GetSavePath()
    {
        return Application.dataPath + "/__TMP/SaveData/HashConfigData.txt";
    }
}
