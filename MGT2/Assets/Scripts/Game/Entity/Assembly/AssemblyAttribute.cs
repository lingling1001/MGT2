using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyAttribute : AssemblyBase
{
    private Dictionary<int, AttributeData> _mapAttributes = new Dictionary<int, AttributeData>();
    private Dictionary<int, AttributeDataChange> _mapAttributeChange = new Dictionary<int, AttributeDataChange>();
    public Dictionary<int, AttributeData> MapAttributes { get { return _mapAttributes; } }
    public void Initial(List<AttributeData> listData)
    {
        for (int cnt = 0; cnt < listData.Count; cnt++)
        {
            AddValue(listData[cnt]);
        }
        _mapAttributeChange.Clear();
    }
    public void SetValue(int key, int value)
    {
        if (ContainsKey(key))
        {
            _mapAttributes[key].Value = value;
        }
    }
    public long GetValue(int key)
    {
        if (ContainsKey(key))
        {
            return _mapAttributes[key].Value;
        }
        return -1;
    }

    public bool ContainsKey(int key)
    {
        return _mapAttributes.ContainsKey(key);
    }

    private void AddValue(int key, int value)
    {
        AddValue(new AttributeData(key, value)); ;
    }
    private void AddValue(AttributeData attr)
    {
        if (_mapAttributes.ContainsKey(attr.Key))
        {
            _mapAttributes[attr.Key] = attr;
        }
        else
        {
            _mapAttributes.Add(attr.Key, attr);
        }
    }

    private void EventAttributeChange(int key, long curValue, long lastValue)
    {
        AddChangeAttribute(key, lastValue, curValue);

    }
    private void AddChangeAttribute(int key, long lastValue, long curValue)
    {
        AttributeDataChange data = new AttributeDataChange(key, lastValue, curValue);
        if (_mapAttributeChange.ContainsKey(key))
        {
            _mapAttributeChange[key] = data;
        }
        else
        {
            _mapAttributeChange.Add(key, data);
        }

    }

}

