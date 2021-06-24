using MFrameWork;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Dictionary<int, ManagerBase> MapAllMgr { get { return _mapAllMgr; } }
    private Dictionary<int, ManagerBase> _mapAllMgr = new Dictionary<int, ManagerBase>();
    public void OnInit()
    {


    }
    public static T QGetOrAddMgr<T>() where T : ManagerBase
    {
        return GameManager.Instance.GetOrAddMgr<T>();
    }
    public static void QRemoveMgr<T>()
    {
        GameManager.Instance.RemoveMgr<T>();
    }

    public T AddMgr<T>(int strKey, T mgr) where T : ManagerBase
    {
        _mapAllMgr.Add(strKey, mgr);
        mgr.OnInit();
        return mgr;
    }
    public T GetOrAddMgr<T>() where T : ManagerBase
    {
        int strKey = GetMgrKey<T>();
        if (MapAllMgr.ContainsKey(strKey))
        {
            return MapAllMgr[strKey] as T;
        }
        return AddMgr(strKey, CreateMgr<T>());
    }

    private T CreateMgr<T>() where T : ManagerBase
    {
#if UNITY_EDITOR
        GameObject obj = new GameObject(typeof(T).Name);
        T target = obj.AddComponent<T>();
        target.transform.SetParent(TransparentNode.GetTransParent(EnumTransParent.Manager));
        return target;
#else
           return (T)Activator.CreateInstance(typeof(T));
#endif
    }

    public T GetMgr<T>() where T : ManagerBase
    {
        IManagerable data = GetMgr(GetMgrKey<T>());
        if (data != null)
        {
            return data as T;
        }
        return null;
    }

    public IManagerable GetMgr(int strKey)
    {
        if (MapAllMgr.ContainsKey(strKey))
        {
            return MapAllMgr[strKey];
        }
        return null;
    }

    public void RemoveMgr<T>()
    {
        RemoveMgr(GetMgrKey<T>());
    }

    public void RemoveMgr(int strKey)
    {
        if (MapAllMgr.ContainsKey(strKey))
        {
            if (_mapAllMgr[strKey] != null)
            {
                _mapAllMgr[strKey].OnRelease();
            }
            _mapAllMgr.Remove(strKey);
        }
    }
    /// <summary>
    /// 获取模块名称
    /// </summary>
    public static int GetMgrKey<T>()
    {
        return typeof(T).GetHashCode();
    }

    public void OnRelease()
    {
        List<int> list = new List<int>(MapAllMgr.Keys);
        for (int cnt = 0; cnt < list.Count; cnt++)
        {
            RemoveMgr(list[cnt]);
        }
    }

}
