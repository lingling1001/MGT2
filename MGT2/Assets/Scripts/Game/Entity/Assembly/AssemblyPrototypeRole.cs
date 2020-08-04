using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyPrototypeRole : AssemblyBase
{
    public PrototypeRole Value { get; private set; }

    public override void OnInit(EnumAssemblyType assemblyType, AssemblyEntityBase owner)
    {
        base.OnInit(assemblyType, owner);

    }
    public void SetValue(PrototypeRole value)
    {
        Value = value;
    }
}
