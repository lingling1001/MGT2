using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAData
{
    private List<GADataBase> _datas = new List<GADataBase>();

    public void AddData(GADataBase data)
    {
        if (Contain(data.ActionType))
        {
            return;
        }
        _datas.Add(data);
    }
    public GADataBase GetData(EnumGADType type)
    {
        GADataBase data = _datas.Find(item => item.ActionType == type);
        if (data == null)
        {
            Log.Error("  Type Is Null  " + type);
        }
        return data;
    }
    public T GetData<T>(EnumGADType type) where T : GADataBase
    {
        GADataBase data = GetData(type);
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
    public bool Contain(EnumGADType type)
    {
        return _datas.Find(item => item.ActionType == type) != null;
    }



    public bool GetValue(EnumGADType type)
    {
        GADataBase data = GetData(type);
        if (data == null)
        {
            return false;
        }
        return data.Value;
    }
    public void SetValue(EnumGADType type, bool value)
    {
        GADataBase data = GetData(type);
        if (data == null)
        {
            return;
        }
        data.SetValue(value);
    }

    public override string ToString()
    {
        if (Contain(EnumGADType.SelfEntity))
        {
            return GetData<GADEntity>(EnumGADType.SelfEntity).Entity.ToString();
        }
        return base.ToString();
    }
}
