using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrototypeItemClickBase<V, T> : PrototypeItemBase<T>
{
    private Action<V> _callback;
    [SerializeField] private Button btnEvent;

    private void Awake()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        SetEventButton(btnEvent);
    }
    public void SetEventButton(Button btn)
    {
        btnEvent = btn;
        if (btnEvent != null)
        {
            btnEvent.RegistEvent(EventClickThis);
        }
    }
    public void SetClickCallback(Action<V> callback)
    {
        _callback = callback;
    }
    private void EventClickThis()
    {
        _callback.InvokeGracefully(this);
    }
}
