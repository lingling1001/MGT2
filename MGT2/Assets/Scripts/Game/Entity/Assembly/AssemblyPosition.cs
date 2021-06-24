using UnityEngine;
public class AssemblyPosition : AssemblyGetViewBase
{
    public Vector3 Position;
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
        base.ViewLoadFinish();
        RefreshViewPosition();
    }

    public void RefreshViewPosition()
    {
        assemblyView.Trans.localPosition = Position;
    }


    //public VInt3 NewPosition;

    //public void SetPosition(VInt3 pos)
    //{
    //    NewPosition = pos;
    //    if (!ViewObjIsNull())
    //    {
    //        RefreshViewPosition();
    //    }
    //    Owner.NotifyObserver(EnumAssemblyOperate.Position, this);
    //}
}
