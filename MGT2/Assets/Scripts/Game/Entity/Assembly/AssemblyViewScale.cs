using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyViewScale : AssemblyGetViewBase
{
    public Vector3 Value;

    public void SetValue(Vector3 value)
    {
        Value = value;
        RefreshViewScale();
    }
    public void RefreshViewScale()
    {
        if (ViewObjIsNull())
        {
            return;
        }
        assemblyView.ObjEntity.transform.localScale = Value;
    }

    public override void ViewLoadFinish()
    {
        RefreshViewScale();
    }
}
