using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyCollider : AssemblyGetViewBase
{
    public Vector3 Value;
    public override void OnInit(EnumAssemblyType assemblyType, AssemblyEntityBase owner)
    {
        base.OnInit(assemblyType, owner);
    }
    public void SetValue(Vector3 value)
    {
        Value = value;
        RefreshViewCollider();
    }

    public void RefreshViewCollider()
    {
        if (ViewObjIsNull())
        {
            return;
        }
        SphereCollider collider = assemblyView.ObjEntity.GetOrAddComponent<SphereCollider>();
        collider.radius = Value.y;
        collider.center = new Vector3(0, Value.y / 2, 0);
    }

    public override void ViewLoadFinish()
    {
        RefreshViewCollider();
    }
}
