/// <summary>
/// 角色控制
/// </summary>
public class AssemblyRoleControl : AssemblyBase
{
    public EnumRoleControl Type;
    public void SetRoleType(EnumRoleControl type)
    {
        Type = type;
    }
}
public enum EnumRoleControl
{
    None,
    /// <summary>
    /// 自己
    /// </summary>
    Self,
    /// <summary>
    /// 自己控制
    /// </summary>
    Control,
}