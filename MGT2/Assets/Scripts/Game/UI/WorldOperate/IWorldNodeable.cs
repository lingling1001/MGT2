
using System.Collections.Generic;

public interface IWorldNodeable
{
    void OnInit(EnumWorldResNode type, AssemblyCache resInfo);
    void OnRelease();
    EnumWorldResNode ResNodeType { get; }
    void OnClickMenu(EnumWorldResTP type);
}

public enum EnumWorldResNode
{
    None,
    Role,

}
public enum EnumWorldResTP
{
    /// <summary>
    /// 攻击
    /// </summary>
    Attack = 21,
    /// <summary>
    /// 防守
    /// </summary>
    Guard = 56,

}