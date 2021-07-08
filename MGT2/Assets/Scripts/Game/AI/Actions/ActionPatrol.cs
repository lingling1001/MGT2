using UnityEngine;
using System.Collections.Generic;

public class ActionPatrol : GoapAction
{
    private AgentData _dataAgent;
    private AgentDataPatrol _agentDataPatrol;
    private AssemblyCache _assemblyCache;

    public override bool CheckProceduralPrecondition(object agent)
    {
        _dataAgent = agent as AgentData;
        _agentDataPatrol = _dataAgent.GetData<AgentDataPatrol>(AgentHelper.ACTION_PATROL);
        if (_agentDataPatrol.State)
        {
            _assemblyCache = _dataAgent.GetData<AgentDataEntity>(AgentHelper.AD_ENTITY).GetEntityCache();
            setTarget(_dataAgent);
            return true;
        }
        return false;
    }
    public override bool perform(object agent)
    {
        if (_assemblyCache.AssemblyAutoMove == null)
        {
            return true;
        }
        if (_assemblyCache.AssyEyeSensor.GetTarget() != null)
        {
            SetIsDone(true);
            return true;
        }
        if (_assemblyCache.AssemblyAutoMove.MoveState == EnumFindPathState.None)
        {
            _assemblyCache.AssemblyAutoMove.SetValue(_agentDataPatrol.PatrolIndex);
        }
        else if (_assemblyCache.AssemblyAutoMove.IsOverFindPath())
        {
            _assemblyCache.AssemblyAutoMove.ResetFindPath();
            SetIsDone(true);
        }
        return true;
    }

    public override bool requiresInRange()
    {
        return false;
    }

    public override void reset()
    {
        _assemblyCache = null;
        _agentDataPatrol = null;
    }

}
