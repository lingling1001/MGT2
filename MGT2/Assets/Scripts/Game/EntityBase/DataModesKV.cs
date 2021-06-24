using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 数据模组？ 作用封装保存T类型的数据 
/// </summary>
public class DataModesKV<K, V> where V : class
{
    private Dictionary<K, V> _mapAllDatas = new Dictionary<K, V>();
    public Dictionary<K, V> AllDatas { get { return _mapAllDatas; } }

    public V GetData(K key)
    {
        if (_mapAllDatas.ContainsKey(key))
        {
            return _mapAllDatas[key];
        }
        Log.Error("  GetData   Error " + key);
        return null;
    }

    public TK GetData<TK>(K key) where TK : class, V
    {
        V data = GetData(key);
        if (data != null)
        {
            TK t = data as TK;
            if (t != null)
            {
                return t;
            }
            Log.Error("  Conver  IAssembly Error " + key + "   " + data.ToString());
        }
        return null;
    }

    public List<V> GetListDatas()
    {
        return new List<V>(AllDatas.Values);
    }
    public virtual void Addition(K key, V data)
    {
        _mapAllDatas.Add(key, data);
    }

    public virtual void Remove(K key)
    {
        _mapAllDatas.Remove(key);
    }

    public bool ContainsKey(K key)
    {
        return _mapAllDatas.ContainsKey(key);
    }

    public void Clear()
    {
        _mapAllDatas.Clear();
    }
}
