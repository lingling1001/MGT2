using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyViewName : AssemblyGetViewBase
{
    public string Value;
    public override void OnInit(EnumAssemblyType assemblyType, AssemblyEntityBase owner)
    {
        base.OnInit(assemblyType, owner);
        GetView();
    }
    public void SetValue(string value)
    {
        Value = value;
        RefreshView();
    }

    public void RefreshView()
    {
        if (assemblyView == null || assemblyView.ObjEntityIsNull())
        {
            return;
        }
        assemblyView.ObjEntity.name = Value;

    }
    public override void ViewLoadFinish()
    {
        RefreshView();
    }
}
