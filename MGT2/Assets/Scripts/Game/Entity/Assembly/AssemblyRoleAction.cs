/// <summary>
/// 当前行为。
/// </summary>
public class AssemblyRoleAction : AssemblyBase
{
    public EnumRoleAction ActionType = EnumRoleAction.Leisure;
    public void SetActionType(EnumRoleAction type)
    {
        ActionType = type;
        ReadDataFinish();
    }

    public override void ReadDataFinish()
    {
        if (ActionType == EnumRoleAction.Leisure)
        {
            return;
        }
        Owner.NotifyObserver(EnumAssemblyOperate.ActionChange, this);
    }

}

public enum EnumRoleAction
{
    /// <summary>
    /// 空闲
    /// </summary>
    Leisure,
    /// <summary>
    /// 攻击
    /// </summary>
    Attack,
    /// <summary>
    /// 巡逻
    /// </summary>
    Patrol,

}