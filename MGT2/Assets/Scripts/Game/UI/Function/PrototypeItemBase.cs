using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeItemBase<T> : MonoBehaviour 
{
    private T _data;
    public T Data { get { return _data; } }
    public virtual void SetData(T data)
    {
        _data = data;

    }
}
