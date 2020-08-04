using System;
using System.Collections.Generic;

public class AssemblyAbilityCast : AssemblyBase, IObserverAssembly
{
    private AssemblyAbility _curCastAbility;
    private APreformDistance _preformDistance;
    private AssemblyRole _target;
    public override void OnInit(EnumAssemblyType assemblyType, AssemblyEntityBase owner)
    {
        base.OnInit(assemblyType, owner);
        Owner.RegisterObserver(this);
    }
    public void SetAbility(AssemblyAbility abilityData)
    {
        _curCastAbility = abilityData;
        _preformDistance = null;
        if (abilityData == null)
        {
            return;
        }
        AssemblyRole target = abilityData.RoleInfo.AssyEyeSensor.Target;
        List<APreformBase> list = _curCastAbility.CheckExecute(target);
        if (list == null || list.Count == 0)
        {
            abilityData.ExecuteLimit();
            abilityData.ExecuteEffect();
            return;
        }

        if (list.Count == 1 && list[0].Type == EnumAPreform.Distance)
        {
            APreformDistance disData = list[0] as APreformDistance;
            abilityData.RoleInfo.AssyRoleMove.SetValue(target.Position, disData.Distance);
            _preformDistance = disData;
        }


    }


    public void UpdateAssembly(EnumAssemblyOperate operate, IAssembly data)
    {
        if (_preformDistance == null)
        {
            return;
        }
        if (operate != EnumAssemblyOperate.RoleMoveState)
        {
            return;
        }
        if (_curCastAbility.RoleInfo.AssyRoleMove.MoveState != EnumFindPathState.Finish)
        {
            return;
        }
        List<APreformBase> list = _curCastAbility.CheckExecute(_target);
        if (list == null || list.Count == 0)
        {
            _curCastAbility.ExecuteLimit();
            _curCastAbility.ExecuteEffect();
            return;
        }
    }

    public override void OnRelease()
    {
        Owner.RemoveObserver(this);
        base.OnRelease();
    }
}
