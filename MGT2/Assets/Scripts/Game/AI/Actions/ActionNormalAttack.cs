using System.Collections.Generic;
using UnityEngine;

public class ActionNormalAttack : GoapAction
{
    private AgentData _dataAgent;
    private AgentDataPatrol _agentDataPatrol;
    private AssemblyCache _assemblyCache;
    private AssemblyCache _assemblyTarget;
    public override bool CheckProceduralPrecondition(object agent)
    {
        _dataAgent = agent as AgentData;
        AgentDataBase boolValue = _dataAgent.GetData<AgentDataBase>(AgentHelper.ACTION_ATTACK_NORMAL);
        if (boolValue == null)
        {
            return false;
        }
        if (!boolValue.State)
        {
            return false;
        }
        _agentDataPatrol = _dataAgent.GetData<AgentDataPatrol>(AgentHelper.ACTION_PATROL);
        AgentDataEntity data = _dataAgent.GetData<AgentDataEntity>(AgentHelper.AD_ENTITY);
        _assemblyCache = data.Entity.GetData<AssemblyCache>();
        setTarget(_dataAgent);
        return true;
    }
    public override bool perform(object agent)
    {
        if (_assemblyTarget != null)
        {
            if (!_assemblyCache.AssyEyeSensor.Contains(_assemblyTarget))
            {
                _assemblyTarget = null;
            }
        }
        if (_assemblyTarget == null)
        {
            _assemblyTarget = _assemblyCache.AssyEyeSensor.GetTarget();
        }
        if (_assemblyTarget == null)
        {
            SetIsDone(true);
            return true;
        }
        int attRange = (int)_assemblyCache.AssyAttribute.GetValue(DTAttribute.RangeAttack);
        //小于攻击范围
        if (Vector3.Distance(_assemblyCache.AssyPosition.Position, _assemblyTarget.AssyPosition.Position) < attRange)
        {
            if (!_assemblyCache.AssemblyAutoMove.IsOverFindPath())
            {
                _assemblyCache.AssemblyAutoMove.StopFindPath();
            }
            Vector3 dir = (_assemblyCache.AssyPosition.Position - _assemblyTarget.AssyPosition.Position).normalized;
            _assemblyCache.AssyDirection.SetValue(dir);
            _assemblyCache.AssyAnimator.SetValue(EnumAnimator.Attack);
        }
        else
        {
            //不能攻击到 先寻路到目标位置
            if (_assemblyCache.AssemblyAutoMove.MoveState == EnumFindPathState.None)
            {
                _assemblyCache.AssemblyAutoMove.SetValue(_assemblyTarget.Position);
            }
            else if (_assemblyCache.AssemblyAutoMove.IsOverFindPath())
            {
                _assemblyCache.AssemblyAutoMove.ResetFindPath();
            }
        }
        return true;
    }

    public override bool requiresInRange()
    {

        return false;
    }

    public override void reset()
    {
        _dataAgent = null;
        _assemblyCache = null;
    }

}
