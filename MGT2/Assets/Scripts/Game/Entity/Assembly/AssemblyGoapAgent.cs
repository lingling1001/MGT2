using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AssemblyGoapAgent : AssemblyBase
{
    public GoapAgent Value { get; private set; }
    public IGoap Plan { get; private set; }

    public void SetValue(GoapAgent value, IGoap plan)
    {
        if (Value != null)
        {
            Value.OnRelease();
        }
        Value = value;
        Plan = plan;
    }

    public override void OnRelease()
    {
        Value.OnRelease();
        base.OnRelease();
    }
}
