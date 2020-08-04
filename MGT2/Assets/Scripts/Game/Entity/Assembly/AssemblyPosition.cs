using System;
using UnityEngine;

public class AssemblyPosition : AssemblyGetViewBase
{
    public Vector3 Position;
    public Vector3 PositionHeadPoint;
    public void SetPosition(Vector3 pos)
    {
        Position = pos;
        if (!ViewObjIsNull())
        {
            RefreshViewPosition();
        }
        Owner.NotifyObserver(EnumAssemblyOperate.Position, this);
    }
    public override void ViewLoadFinish()
    {
        RefreshViewPosition();
    }

    public void RefreshViewPosition()
    {
        assemblyView.Trans.position = Position;
    }


}
