using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityKeyHelper
{
    private List<int> _listKeys = new List<int>();
    private List<int> _listRelease = new List<int>();

    public void Initial(List<int> list)
    {
        _listKeys.AddRange(list);
    }
    public int GetKey()
    {
        int key;
        if (_listRelease.Count > 0)
        {
            key = _listRelease[0];
            _listRelease.RemoveAt(0);
        }
        else
        {
            key = _listKeys.Count;
            while (_listKeys.Contains(key))
            {
                key++;
            }
        }
        AddKey(key);
        return key;
    }

    public void AddKey(int key)
    {
        _listKeys.Add(key);
    }
   
    public void RemoveKey(int key)
    {
        _listKeys.Remove(key);
        _listRelease.Add(key);
    }

    public void Release()
    {
        _listKeys.Clear();
        _listRelease.Clear();
    }
}
