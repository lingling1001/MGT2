using System;
/// <summary>
/// 死亡
/// </summary>
public class AssemblyEntityDead : AssemblyBase
{
    private DateTime finishTime;
    public override void OnInit(EnumAssemblyType assemblyType, AssemblyEntityBase owner)
    {
        base.OnInit(assemblyType, owner);
        finishTime = DateTime.Now.AddMilliseconds(1500);
        AssemblyRole role = owner.GetData<AssemblyRole>(EnumAssemblyType.Role);
        role.AssyAnimator.SetValue(EnumAnimator.Die);
        Log.Error(" 角色 死亡 组件 附加 " + role.ToString());
    }
    public bool IsFinish()
    {
        return DateTime.Now > finishTime;
    }
}