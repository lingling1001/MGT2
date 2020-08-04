using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public static class UIEventHelper
{
    public static IDisposable RegistEvent(this Button btn, Action callback)
    {
        if (btn == null || callback == null)
        {
            return null;
        }
        return btn.OnClickAsObservable().Subscribe(x => { callback(); });
    }

    public static IDisposable RegistEvent(this Button btn, Action<Button> callback)
    {
        if (btn == null || callback == null)
        {
            return null;
        }
        return btn.OnClickAsObservable().Subscribe(x => { callback(btn); });
    }
}
