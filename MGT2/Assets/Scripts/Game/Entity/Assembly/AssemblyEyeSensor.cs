using MFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AssemblyEyeSensor : AssemblySelfRole, IUpdate
{
    public AssemblyRole Target { get; private set; }
    public int Priority => DefinePriority.NORMAL;

    public override void OnInit(EnumAssemblyType assemblyType, AssemblyEntityBase owner)
    {
        base.OnInit(assemblyType, owner);
        RegisterInterfaceManager.RegisteUpdate(this);
    }
    public bool CheckTargetIsNull()
    {
        return Target == null;
    }
    public void On_Update(float elapseSeconds, float realElapseSeconds)
    {
        if (SelfEntity == null)
        {
            return;
        }
        if (SelfEntity.AssyAttribute == null)
        {
            return;
        }
        int range = SelfEntity.AssyAttribute.GetValue(DefineAttributeId.GRUAR_RANGE);
        if (Target != null)
        {
            if (Target.Owner.ContainsKey(EnumAssemblyType.EntityDead))
            {
                Target = null;
            }
            else
            {
                if ((SelfEntity.Position - Target.Position).magnitude > range)//超出警戒距离 置空
                {
                    Target = null;
                }
                else
                {
                    return;
                }
            }
        }
        Target = EntityHelper.GetNearOtherCamp(SelfEntity, SelfEntity.EntityType, range);
        if (Target != null)
        {
            Log.Info(SelfEntity.ToString() + "  找到目标 - > " + Target.ToString());
        }
    }
    public override void OnRelease()
    {
        RegisterInterfaceManager.UnRegisteUpdate(this);
        base.OnRelease();
    }



}
