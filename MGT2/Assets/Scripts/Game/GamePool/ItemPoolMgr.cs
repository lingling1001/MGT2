using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对象池基础管理类， 只负责获取添加对象，创建对象暂时需要单独处理，
/// </summary>
public class ItemPoolMgr : MonoBehaviour
{
    private Object lockedObj = new Object();
    protected static ItemPoolMgr instance;
    public static bool InstanceIsNull()
    {
        if (instance == null || instance.Equals(null))
        {
            return true;
        }
        return false;
    }
    public static ItemPoolMgr Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = TransparentNode.GetTransParent(EnumTransParent.Pool).gameObject;
                instance = obj.AddComponent<ItemPoolMgr>();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    private Dictionary<string, Transform> _mapParents = new Dictionary<string, Transform>();
    public Dictionary<string, List<IMonoPool>> MapCache { get { return _mapCache; } }
    private Dictionary<string, List<IMonoPool>> _mapCache = new Dictionary<string, List<IMonoPool>>();

    /// <summary>
    /// 从对象池获取一个对象 没有返回空 需要自己处理并创建新的
    /// </summary>
    private T GetIPooltem<T>(string strKey) where T : class, IMonoPool
    {
        IMonoPool data = GetIPooltem(strKey);
        if (data != null)
        {
            return data as T;
        }
        return null;
    }
    /// <summary>
    /// 从对象池获取一个对象 没有返回空 需要自己处理并创建新的
    /// </summary>
    private IMonoPool GetIPooltem(string strKey)
    {
        lock (lockedObj)
        {
            List<IMonoPool> list = GetOrNewList(strKey);
            if (list.Count > 0)
            {
                IMonoPool item = list[0];
                list.RemoveAt(0);
                return item;
            }
            return null;
        }

    }

    /// <summary>
    /// 添加对象缓存
    /// </summary>
    /// <param name="item"></param>
    private void AddPoolItem(IMonoPool item)
    {
        if (item == null)
        {
            return;
        }
        List<IMonoPool> list = GetOrNewList(item.PoolKey);
        item.EnterPool();
        list.Add(item);
    }

    /// <summary>
    /// 取出缓存列表
    /// </summary>
    private List<IMonoPool> GetOrNewList(string strPath)
    {
        if (!_mapCache.ContainsKey(strPath))
        {
            _mapCache.Add(strPath, new List<IMonoPool>());
        }
        return _mapCache[strPath];
    }

    /// <summary>
    /// 添加物体到节点下
    /// </summary>
    private void AddChildToNode(string strKey, GameObject child)
    {
        if (!_mapParents.ContainsKey(strKey))
        {
            GameObject obj = new GameObject(strKey);
            obj.transform.SetParent(transform, false);
            _mapParents.Add(strKey, obj.transform);
        }
        child.transform.SetParent(_mapParents[strKey], false);
        if (child.activeSelf)
        {
            child.SetActive(false);
        }
    }

    public void ClearAllCache()
    {
        if (instance == null || instance.Equals(null))
        {
            return;
        }
        foreach (var item in _mapParents)
        {
            GameObject.DestroyImmediate(item.Value.gameObject);
        }
        _mapCache.Clear();
        _mapParents.Clear();
        instance = null;
        GameObject.DestroyImmediate(gameObject);
    }

    public static T CreateOrGetItem<T>(string strPath, Transform parent) where T : Component, IMonoPool
    {
        IMonoPool item = ItemPoolMgr.Instance.GetIPooltem(strPath);
        T target = null;
        if (item == null)
        {
            target = CreateItem<T>(strPath, parent);
        }
        else
        {
            target = item as T;
        }
        if (target == null)
        {
            Log.Error(" CreateOrGetItem Error {0}", strPath);
            return null;
        }
        target.transform.SetParent(parent, false);
        target.transform.localScale = Vector3.one;
        target.transform.localPosition = Vector3.zero;
        target.transform.localRotation = Quaternion.identity;
        NGUITools.SetActive(target, true);

        return target;
    }

    private static T CreateItem<T>(string strPath, Transform parent) where T : Component, IMonoPool
    {
        GameObject prefab = (GameObject)ResLoadHelper.LoadAsset<GameObject>(strPath);
        if (prefab == null || parent == null)
        {
            Debug.LogError(" prefab or parent is null.  prefab : " + prefab + "  parent : " + parent);
            return null;
        }
        GameObject obj = GameObject.Instantiate(prefab) as GameObject;
        T target = NGUITools.AddMissingComponent<T>(obj);
        target.PoolKey = strPath;
        return target;

    }


    public static T CreateOrGetObj<T>(string strKey) where T : class, IMonoPool, new()
    {
        T data = ItemPoolMgr.Instance.GetIPooltem<T>(strKey);
        if (data == null)
        {
            data = new T();
            data.PoolKey = strKey;
        }
        return data;
    }
    public static void AddPool(string strPath, GameObject obj)
    {
        if (ItemPoolMgr.InstanceIsNull() || obj == null)
        {
            return;
        }
        ItemPoolMgr.Instance.AddChildToNode(strPath, obj);
    }
    public static void AddPool(IMonoPool mono)
    {
        if (ItemPoolMgr.InstanceIsNull() || mono == null || mono.Equals(null))
        {
            return;
        }
        ItemPoolMgr.Instance.AddPoolItem(mono);
    }


}

