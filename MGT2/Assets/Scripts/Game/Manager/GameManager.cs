using MFrameWork;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager<T> : Singleton<GameManager<T>> where T : ManagerBase
{
    public Dictionary<Type, T> MapDatas { get { return _mapDatas; } }
    private Dictionary<Type, T> _mapDatas = new Dictionary<Type, T>();
    public void OnInit()
    {


    }
    public T Addition(T mgr)
    {
        Type key = GetKey();
        if (_mapDatas.ContainsKey(key))
        {
            return _mapDatas[key];
        }
        _mapDatas.Add(key, mgr);
        mgr.OnInit();
        return mgr;
    }




    public T GetMgr()
    {
        Type key = GetKey();
        if (MapDatas.ContainsKey(key))
        {
            return MapDatas[key];
        }
        return null;
    }

    public void OnRelease()
    {
        List<Type> list = new List<Type>(MapDatas.Keys);
        for (int cnt = 0; cnt < list.Count; cnt++)
        {
            RemoveMgr(list[cnt]);
        }

    }

    public void RemoveMgr()
    {
        RemoveMgr(GetKey());

    }
    public void RemoveMgr(Type key)
    {
        if (InstanceIsNull())
        {
            return;
        }
        Log.Info(" Remove Manager {0} ", key);
        if (MapDatas.ContainsKey(key))
        {
            if (_mapDatas[key] != null)
            {
                _mapDatas[key].OnRelease();
            }
            _mapDatas.Remove(key);
        }
    }
    /// <summary>
    /// 获取模块名称
    /// </summary>
    public static Type GetKey()
    {
        return typeof(T);
    }

    public static void QRemoveMgr()
    {
        GameManager<T>.Instance.RemoveMgr();
    }

    public static T QGetOrAddMgr()
    {
        T mgr = GameManager<T>.Instance.GetMgr();
        if (mgr != null)
        {
            return mgr;
        }
        mgr = CreateMgr();
        GameManager<T>.Instance.Addition(mgr);
        return mgr;
    }


    private static T CreateMgr()
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

}

