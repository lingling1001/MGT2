using UnityEngine;

public class AssemblyTransHead : AssemblyGetViewBase
{
    public int ModelType { get; private set; }
    public Transform TransHead { get; private set; }
    public void SetModelType(int type)
    {
        ModelType = type;
    }
    public override void ViewLoadFinish()
    {
        TransHead = assemblyView.ObjEntity.FindChild<Transform>("mount_Head_UI");
        Owner.NotifyObserver(EnumAssemblyOperate.TransHead, this);
    }
    public override void OnRelease()
    {
        TransHead = null;
        base.OnRelease();
    }
}
