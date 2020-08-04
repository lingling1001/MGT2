using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class EditorCommonObject
{
    /// <summary>
    /// 获取路径下所有预设 递归
    /// </summary>
    public static List<GameObject> GetFloderPrefab(string dirPath)
    {
        List<string> paths = new List<string>();
        List<GameObject> objs = new List<GameObject>();
        GetObjectDirFiles(dirPath, paths, ".prefab");
        for (int i = 0; i < paths.Count; i++)
        {
            GameObject obj = AssetDatabase.LoadMainAssetAtPath(paths[i]) as GameObject;
            if (obj != null)
            {
                objs.Add(obj);
            }
        }
        return objs;
    }
    /// <summary>
    /// 获取路径下所有预设 递归
    /// </summary>
    public static List<T> GetFloderPrefab<T>(string dirPath) where T : UnityEngine.Object
    {
        List<string> paths = new List<string>();
        List<T> items = new List<T>();
        GetObjectDirFiles(dirPath, paths);
        for (int i = 0; i < paths.Count; i++)
        {
            T obj = AssetDatabase.LoadAssetAtPath(paths[i], typeof(T)) as T;
            if (obj != null)
            {
                items.Add(obj);
            }
        }
        return items;
    }


    /// <summary>
    /// 获取文件夹下所有的文件
    /// </summary>
    /// <param name="dirPath">路径</param>
    /// <param name="dirs">文件列表</param>
    /// <param name="extensions">后缀</param>
    public static void GetObjectDirFiles(string dirPath, List<string> dirs, params string[] extensions)
    {
        foreach (string path in Directory.GetFiles(dirPath))
        {
            if (extensions.Length == 0)
            {
                dirs.Add(path.Substring(path.IndexOf("Assets")));
            }
            //获取所有文件夹中包含后缀
            foreach (var item in extensions)
            {
                if (System.IO.Path.GetExtension(path) == item)//".prefab")
                {
                    dirs.Add(path.Substring(path.IndexOf("Assets")));
                }
            }
        }
        if (Directory.GetDirectories(dirPath).Length > 0)  //遍历所有文件夹
        {
            foreach (string path in Directory.GetDirectories(dirPath))
            {
                GetObjectDirFiles(path, dirs, extensions);
            }
        }
    }

}
