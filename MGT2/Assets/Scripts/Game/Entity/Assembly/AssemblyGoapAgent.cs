using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyGoapAgent : AssemblyBase
{
    public GoapAgent Value;
    public IGoap Plan;

    public void SetValue(GoapAgent value, IGoap plan)
    {
        if (Value != null)
        {
            Value.OnRelease();
        }
        Value = value;
        Plan = plan;
    }


    protected override void OnRelease()
    {
        Value.OnRelease();
    }
}
