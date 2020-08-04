using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine;
using MFrameWork;

public class PrototypeManager<T> : Singleton<PrototypeManager<T>> where T : BasePrototype
{
    /// <summary>
    /// 以表为单位存储
    /// </summary>
    public Dictionary<Type, Dictionary<int, T>> dicAllTableData = new Dictionary<Type, Dictionary<int, T>>();

    public void Initial(Type refType, Dictionary<int, T> dicTempList)
    {
        //加入表集合中
        if (dicTempList.Count != 0)
        {
            if (dicAllTableData.ContainsKey(refType))
            {
                dicAllTableData[refType] = dicTempList;
            }
            else
            {
                dicAllTableData.Add(refType, dicTempList);
            }
        }

    }

    /// <summary>
    /// 提取配置属性
    /// </summary>
    /// <returns>The prototype.</returns>
    /// <param name="prototypeID">Prototype I.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public T GetPrototype(int prototypeID)
    {
        T basePrototype = default(T);

        Type refType = typeof(T);

        Dictionary<int, T> table = GetPrototypeTable();

        if (table != null)
        {
            if (table.TryGetValue(prototypeID, out basePrototype))
            {
                if (basePrototype == null)
                {
                    return default(T);
                }

                return basePrototype;
            }
            else
            {
#if UNITY_EDITOR
                Log.Error("此表中没有此ID GetPrototype = " + refType.Name + "   prototypeID = " + prototypeID);
#else
				Log.Warning("此表中没有此ID GetPrototype = " + refType.Name + "   prototypeID = " + prototypeID);
#endif
            }
        }

        return default(T);
    }

    private Dictionary<int, T> GetPrototypeTable()
    {
        Type refType = typeof(T);

        if (dicAllTableData.ContainsKey(refType))
        {
            return dicAllTableData[refType];
        }
        else
        {
            Log.Error("没有" + refType.Name + "表");
        }

        return null;
    }

    public Dictionary<int, T> GetTableDic()
    {
        Type refType = typeof(T);

        Dictionary<int, T> tempList = new Dictionary<int, T>();

        if (dicAllTableData.ContainsKey(refType))
        {
            Dictionary<int, T> temp = dicAllTableData[refType];

            foreach (KeyValuePair<int, T> kv in temp)
            {
                tempList.Add(kv.Key, kv.Value);
            }
        }

        return tempList;
    }

    public List<T> GetTableList()
    {
        Type refType = typeof(T);

        List<T> ls = new List<T>();

        if (dicAllTableData.ContainsKey(refType))
        {
            Dictionary<int, T> temp = dicAllTableData[refType];

            foreach (KeyValuePair<int, T> kv in temp)
            {
                ls.Add(kv.Value);
            }
        }
        return ls;
    }

    /// <summary>
    /// 获取当前表的个数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public int GetTableCount()
    {
        Type refType = typeof(T);
        int count = 0;
        if (dicAllTableData.ContainsKey(refType))
        {
            count = dicAllTableData[refType].Count;
        }

        return count;
    }


}
