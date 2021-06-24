using com.ootii.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public static class EventHelper
{
    public static IDisposable RegistEvent(this Button btn, Action<Button> callback)
    {
        if (btn == null || callback == null)
        {
            return null;
        }
        return btn.OnClickAsObservable().Subscribe(x => { callback(btn); });
    }

    public static void SendMessage(string eventName)
    {
        MessageDispatcher.SendMessage(eventName);
    }
    public static void SendMessage(string eventName, float rDelay)
    {
        MessageDispatcher.SendMessage(eventName, rDelay);
    }
    public static void AddListener(string rMessageType, MessageHandler rHandler)
    {
        MessageDispatcher.AddListener(rMessageType, rHandler);
    }
    public static void RemoveListener(string rMessageType, MessageHandler rHandler)
    {
        MessageDispatcher.RemoveListener(rMessageType, rHandler);
    }

}
