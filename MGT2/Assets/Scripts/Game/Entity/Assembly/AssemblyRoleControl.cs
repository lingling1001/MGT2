using UnityEngine;

public class AssemblyRoleControl : AssemblyBase, IObserverAssembly
{
    private AssemblyRole assemblyRole;
    private Vector3 offsetPos = new Vector3(0, 5, -5);

    public override void OnInit(EnumAssemblyType assemblyType, AssemblyEntityBase owner)
    {
        base.OnInit(assemblyType, owner);
        assemblyRole = owner.GetData<AssemblyRole>(EnumAssemblyType.Role);
        owner.RegisterObserver(this);
    }

    public void UpdateAssembly(EnumAssemblyOperate operate, IAssembly data)
    {
        if (operate != EnumAssemblyOperate.Position)
        {
            return;
        }
        CameraManager.Instance.RefreshTargetPos(assemblyRole.Position + offsetPos);
    }
    public override void OnRelease()
    {
        Owner.RemoveObserver(this);
        base.OnRelease();
    }
}
