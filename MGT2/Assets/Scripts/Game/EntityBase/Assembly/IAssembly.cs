using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssembly
{
    void Init(EntityAssembly owner);
    void Release();
}

public abstract class AssemblyBase : IAssembly
{

    [Newtonsoft.Json.JsonIgnore] private EntityAssembly _owner;
    [Newtonsoft.Json.JsonIgnore] public EntityAssembly Owner { get { return _owner; } }

    public void Init(EntityAssembly owner)
    {
        _owner = owner;
        OnInit(owner);
    }
    public void Release()
    {
        OnRelease();
        _owner = null;
    }
    protected virtual void OnInit(EntityAssembly owner)
    {

    }
    /// <summary>
    /// 序列化数据完成
    /// </summary>
    public virtual void ReadDataFinish()
    {

    }

    protected virtual void OnRelease()
    {

    }


}

public class AssemblyType
{
    public const string RoleInfo = "AssemblyRoleInfo";

}