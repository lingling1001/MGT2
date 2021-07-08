using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentData
{
    private List<AgentDataBase> _datas = new List<AgentDataBase>();
    public void AddData(AgentDataBase data)
    {
        if (Contain(data.ActionType))
        {
            Log.Error("  Key Error " + data.ActionType);
            return;
        }
        _datas.Add(data);
    }
    public AgentDataBase GetData(string type)
    {
        return _datas.Find(item => item.ActionType == type);
    }
    public T GetData<T>(string type) where T : AgentDataBase
    {
        AgentDataBase data = GetData(type);
        if (data != null)
        {
            T t = data as T;
            if (t != null)
            {
                return t;
            }
            Log.Error("  As Type Is Null : " + type + "  TypeOf " + data.GetType());
        }
        return null;
    }
    public bool Contain(string type)
    {
        return GetData(type) != null;
    }

    public bool GetState(string strType)
    {
        AgentDataBase data = GetData(strType);
        if (data == null)
        {
            return false;
        }
        return data.State;
    }




}
