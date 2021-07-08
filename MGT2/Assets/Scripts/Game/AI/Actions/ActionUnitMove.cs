using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActionUnitMove : GoapAction
{

    private AgentData _agentData;
    private EntityAssembly _entity;
    private AssemblyAutoMove _autoMove;
    private AgentDataPosition _targetPos;
    public override bool CheckProceduralPrecondition(object agent)
    {
        _agentData = agent as AgentData;
        if (_agentData.Contain(AgentHelper.ACTION_UNIT_MOVE))
        {
            if (_agentData == null)
            {
                return false;
            }
            _targetPos = _agentData.GetData<AgentDataPosition>(AgentHelper.AD_POSITION);
            if (_targetPos == null)
            {
                return false;
            }
            AgentDataEntity entityData = _agentData.GetData<AgentDataEntity>(AgentHelper.AD_ENTITY);
            if (entityData == null)
            {
                return false;
            }
            _entity = entityData.Entity;
            if (_entity == null)
            {
                return false;
            }
            _autoMove = _entity.GetData<AssemblyAutoMove>();
            if (_autoMove == null)
            {
                return false;
            }
            setTarget(_agentData);
            return true;
        }
        return false;
    }

    public override bool perform(object agent)
    {
        if (_autoMove == null)
        {
            return true;
        }
        if (_autoMove.MoveState == EnumFindPathState.None)
        {
            _autoMove.SetValue(_targetPos.Value);
        }
        else if (_autoMove.IsOverFindPath())
        {
            SetIsDone(true);
            _autoMove.ResetFindPath();
        }
        return true;
    }

    public override bool requiresInRange()
    {
        return false;
    }

    public override void reset()
    {
        _agentData = null;
        _autoMove = null;
        _entity = null;

    }
}
