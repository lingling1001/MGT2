using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GARolePatrol : GoapAction
{
    private long _nextMoveTime;
    private AssemblyRole _selfEntity;
    private AssemblyRoleMove _roleMove;
    private GADRolePatrol _dataPatrol;
    private bool _isInit;
    public override bool CheckProceduralPrecondition(object agent)
    {
        GAData data = agent as GAData;
        _dataPatrol = data.GetData<GADRolePatrol>(EnumGADType.Patrol);
        _selfEntity = data.GetData<GADEntity>(EnumGADType.SelfEntity).Entity;
        if (!_selfEntity.AssyEyeSensor.CheckTargetIsNull())
        {
            return false;
        }
        _roleMove = _selfEntity.AssyRoleMove;
        _dataPatrol.SetIdleTime(UnityEngine.Random.Range(2, 5));
        _dataPatrol.SetMovePos(GetRandomMovePosition(_selfEntity.AssyPosition.Position));
        target = agent;
        //Log.Debug("   CheckProceduralPrecondition  GARoleIdle");
        return true;
    }

    public override bool perform(object agent)
    {
        if (_selfEntity.AssyEyeSensor == null)
        {
            return true;
        }
        if (!_selfEntity.AssyEyeSensor.CheckTargetIsNull())//发现目标 停止移动
        {
            SetIsDone(true);
            _roleMove.StopFindPath();
            return true;
        }
        if (_nextMoveTime == -1)
        {
            _nextMoveTime = DateTime.Now.Ticks + _dataPatrol.IdleTime * TimeSpan.TicksPerSecond;
        }
        if (DateTime.Now.Ticks > _nextMoveTime)
        {
            if (!_isInit)
            {
                _roleMove.SetValue(_dataPatrol.MovePos);
                _isInit = true;
            }
            if (_roleMove.MoveState == EnumFindPathState.Finish)
            {
                SetIsDone(true);
                //Log.Info("--------------End");
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
        //Log.Debug(" reset  GAIdle ");
        _nextMoveTime = -1;
        _isInit = false;
    }

    private Vector3 GetRandomMovePosition(Vector3 posStart)
    {
        Vector3 posEnd = new Vector3(GetRangeNum(posStart.x, -20, 20), posStart.y, GetRangeNum(posStart.z, -20, 20));
        return posEnd;
    }

    private float GetRangeNum(float value, float min, float max)
    {
        return value + UnityEngine.Random.Range(min, max);
    }
}
