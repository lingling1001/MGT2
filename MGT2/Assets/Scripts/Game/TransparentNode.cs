using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentNode : MonoSingleton<TransparentNode>
{

    private Dictionary<int, Transform> _mapParents = new Dictionary<int, Transform>();

    public Transform GetTransform(EnumTransParent type)
    {
        int key = (int)type;
        if (!_mapParents.ContainsKey(key))
        {
            GameObject obj = new GameObject(type.ToString());
            _mapParents.Add(key, obj.transform);
            obj.transform.SetParent(transform);
        }
        return _mapParents[key];
    }

    public static Transform GetTransParent(EnumTransParent type)
    {
        return Instance.GetTransform(type);
    }

}
public enum EnumTransParent : short
{
    Entities,
    Manager,
    Pool,
}