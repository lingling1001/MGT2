using UnityEngine;
using System.Collections.Generic;

public class CacheGameObjectActiveMap
{
    private Dictionary<string, GameObject> _mapObjects = new Dictionary<string, GameObject>();
    /// <summary>
    /// 返回 ture 第一次加载
    /// </summary>
    public bool SetActive(GameObject objParent, string path, bool isActive)
    {
        if (string.IsNullOrEmpty(path))
        {
            return false;
        }
        GameObject obj = GetObject(path);
        if (!isActive)
        {
            NGUITools.SetActive(obj, false);
            return false;
        }
        if (obj == null && !_mapObjects.ContainsKey(path))
        {
            GameObject prefab = ResLoadHelper.LoadAsset<GameObject>(path);
            if (prefab == null)
            {
                return false;
            }
            obj = NGUITools.AddChild(objParent, prefab);
            Add(path, obj);
            NGUITools.SetActive(obj, true);
            return true;
        }
        NGUITools.SetActive(obj, true);
        return false;
    }
    public void SetActive(string path, bool isActive)
    {
        if (!_mapObjects.ContainsKey(path))
        {
            return;
        }
        NGUITools.SetActive(_mapObjects[path], isActive);
    }
    public void Add(string strKey, GameObject obj)
    {
        if (_mapObjects.ContainsKey(strKey))
        {
            return;
        }
        _mapObjects.Add(strKey, obj);
    }
    /// <summary>
    /// 获取物体
    /// </summary>
    public GameObject GetObject(string path)
    {
        if (_mapObjects.ContainsKey(path))
        {
            return _mapObjects[path];
        }
        return null;
    }
    public bool ContainsObject(string path)
    {
        return _mapObjects.ContainsKey(path);
    }
    /// <summary>
    /// 设置所有物体的状态
    /// </summary>
    public void SetActiveState(bool active, string ignore = "")
    {
        foreach (var item in _mapObjects)
        {
            if (item.Key != ignore)
            {
                NGUITools.SetActive(item.Value, active);
            }
        }
    }
    /// <summary>
    /// 获取物体组件
    /// </summary>
    public T GetObject<T>(string path) where T : Component
    {
        GameObject obj = GetObject(path);
        if (obj != null)
        {
            return obj.GetComponent<T>();
        }
        return null;
    }
}