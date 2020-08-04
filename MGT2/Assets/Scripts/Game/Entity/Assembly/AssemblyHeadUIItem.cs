using com.ootii.Messages;
using UnityEngine;
public class AssemblyHeadUIItem : AssemblyBase, IObserverAssembly
{
    public UIHeadItem Value { get; private set; }
    private AssemblyTransHead assemblyTransHead;
    private AssemblyRole assemblyRole;
    public override void OnInit(EnumAssemblyType assemblyType, AssemblyEntityBase owner)
    {
        base.OnInit(assemblyType, owner);
        assemblyRole = owner.GetData<AssemblyRole>(EnumAssemblyType.Role);
        owner.RegisterObserver(this);
        SetValue(UIHeadManager.Instance.CreateHeadItem(assemblyRole.EntityId));

    }
    public void SetValue(UIHeadItem item)
    {
        Value = item;
        item.Initial(assemblyRole.EntityId);
        RefreshPosition();
    }
    private void RefreshPosition()
    {
        if (assemblyTransHead == null)
        {
            assemblyTransHead = Owner.GetData<AssemblyTransHead>(EnumAssemblyType.TransHead);
        }
        if (assemblyTransHead != null && assemblyTransHead.TransHead != null)
        {
            Value.SetPosition(assemblyTransHead.TransHead.position);
        }
    }

    public void UpdateAssembly(EnumAssemblyOperate operate, IAssembly data)
    {
        if (operate == EnumAssemblyOperate.Position)
        {
            RefreshPosition();
        }
        else if (operate == EnumAssemblyOperate.TransHead)
        {
            RefreshPosition();
        }
    }

    public override void OnRelease()
    {
        if (Value != null)
        {
            UIHeadManager.Instance.AssemblyRemove(this);
        }
        Owner.RemoveObserver(this);
        base.OnRelease();
    }
}
