using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssembly
{
    EnumAssemblyType AssemblyType { get; }
    void OnInit(EnumAssemblyType assemblyType, AssemblyEntityBase owner);
    void OnRelease();
}

public abstract class AssemblyBase : IAssembly
{
    private AssemblyEntityBase _owner;
    public AssemblyEntityBase Owner { get { return _owner; } }

    public EnumAssemblyType AssemblyType { get; private set; }


    public virtual void OnInit(EnumAssemblyType assemblyType, AssemblyEntityBase owner)
    {
        AssemblyType = assemblyType;
        _owner = owner;
    }
    public virtual void OnRelease()
    {
        //Log.Debug("  OnRelease  " + Owner.GetData<AssemblyEntity>(EnumAssemblyType.EntityId).EntityId);
        _owner = null;
    }
   
    public bool CheckParamLength(object[] param, int lenght)
    {
        if (param == null)
        {
            return false;
        }
        if (param.Length >= lenght)
        {
            return true;
        }
        return false;
    }
}
