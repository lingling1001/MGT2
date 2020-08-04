using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APreformBase
{
    public AssemblyRole Owner { get; private set; }
    public AssemblyRole Target { get; private set; }
    public string Param { get; private set; }
    public EnumAPreform Type { get; private set; }

    public virtual void OnInitial(EnumAPreform type, AssemblyRole owner, string param)
    {
        Owner = owner;
        Type = type;
        Param = param;
    }
    public void RefreshTarget(AssemblyRole target)
    {
        Target = target;
    }
    public virtual void OnExecute()
    {

    }
    public abstract bool OnCheckPreform();

    public virtual void OnRelease()
    {

    }

}
public enum EnumAPreform
{
    /// <summary>
    /// CD
    /// </summary>
    CD = 1,
    /// <summary>
    /// 距离
    /// </summary>
    Distance = 2,

}
