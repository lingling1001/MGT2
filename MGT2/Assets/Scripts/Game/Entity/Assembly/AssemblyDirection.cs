using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyDirection : AssemblyGetViewBase
{
    public Vector3 Value;
    public void SetValue(Vector3 value)
    {
        value.y = Value.y;
        Value = value;
        RefreshView();
    }
    public override void ViewLoadFinish()
    {
        RefreshView();
    }
    public void RefreshView()
    {
        if (ViewObjIsNull())
        {
            return;
        }
        if (Value == Vector3.zero)
        {
            return;
        }
        assemblyView.Trans.forward = Value;
        Log.Info("  Dir  " + Value);
    }
    public override void OnRelease()
    {
        base.OnRelease();
    }
}
