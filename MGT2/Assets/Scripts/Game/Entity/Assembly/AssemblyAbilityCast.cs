using System;
using System.Collections.Generic;
/// <summary>
/// 技能释放。 
/// </summary>
public class AssemblyAbilityCast : AssemblyBase, IObserverAssembly
{
    private AssemblyAbility _curCastAbility;
    private APreformDistance _preformDistance;
    private AssemblyRole _target;
    private AssemblyJoystick _joystick;
    public override void OnInit(EnumAssemblyType assemblyType, AssemblyEntityBase owner)
    {
        base.OnInit(assemblyType, owner);
        Owner.RegisterObserver(this);
    }
    public void SetAbility(AssemblyAbility abilityData)
    {
        _curCastAbility = abilityData;
        if (abilityData == null)
        {
            return;
        }
        _joystick = _curCastAbility.Owner.GetData<AssemblyJoystick>(EnumAssemblyType.Joystick);
        _target = abilityData.RoleInfo.AssyEyeSensor.Target;

        if (CastAbility())
        {
            return;
        }
        List<APreformBase> list = _curCastAbility.CheckExecute(_target);
        if (list.Count == 1 && list[0].Type == EnumAPreform.Distance)
        {
            //有距离限制 说明target!=null,寻路到目标点
            APreformDistance disData = list[0] as APreformDistance;
            abilityData.RoleInfo.AssyRoleMove.SetValue(_target.Position, disData.Distance);
            _preformDistance = disData;
        }

    }


    public void UpdateAssembly(EnumAssemblyOperate operate, IAssembly data)
    {
        if (_curCastAbility == null)
        {
            return;
        }
        if (operate == EnumAssemblyOperate.AbilityState)//技能状态变更 刷新技能
        {
            if (_curCastAbility.State == EnumAbilityState.End)
            {
                CasetFinish();
            }
            return;
        }
        if (_preformDistance == null)
        {
            return;
        }
        if (operate == EnumAssemblyOperate.JoystickMove)
        {
            _curCastAbility = null;
            _preformDistance = null;
            return;
        }
        if (operate != EnumAssemblyOperate.RoleMoveState)//寻路到目标点结束
        {
            return;
        }
        if (_curCastAbility.RoleInfo.AssyRoleMove.MoveState != EnumFindPathState.Finish)
        {
            return;
        }
        CastAbility();
    }

    private bool CastAbility()
    {
        //移动结束
        List<APreformBase> list = _curCastAbility.CheckExecute(_target);
        if (list == null || list.Count == 0)
        {
            if (_joystick != null)//释放技能先禁止摇杆操作
            {
                _joystick.SetIsEnable(false);
            }
            _curCastAbility.ExecuteLimit();
            _curCastAbility.ExecuteEffect(_target);
            return true;
        }
        return false;
    }
    private void CasetFinish()
    {
        if (_joystick != null)
        {
            _joystick.SetIsEnable(true);
        }
        _curCastAbility = null;
        _preformDistance = null;
        _joystick = null;
    }

    public override void OnRelease()
    {
        Owner.RemoveObserver(this);
        base.OnRelease();
    }
}
