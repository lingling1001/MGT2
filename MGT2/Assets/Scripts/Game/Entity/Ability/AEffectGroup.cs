using System;
using System.Collections.Generic;

public class AEffectGroup
{
    private List<AEffectEventBase> _listEvents = new List<AEffectEventBase>();

    public EnumAbilityState State { get { return _state; } }
    private EnumAbilityState _state;

    private int _indexEffect;

    public void Initial(List<AEffectEventBase> datas)
    {
        _listEvents = datas;
    }
    public void Execute(AssemblyRole target)
    {
        for (int cnt = 0; cnt < _listEvents.Count; cnt++)
        {
            _listEvents[cnt].SetIsFinish(false);
            _listEvents[cnt].SetTarget(target);
        }
        SetState(EnumAbilityState.Start);
        SetIndex(0);
    }
    public void UpdateEvent()
    {
        if (_state == EnumAbilityState.Start)//下轮事件
        {
            if (_listEvents.Count > _indexEffect)
            {
                SetState(EnumAbilityState.Execute);
                _listEvents[_indexEffect].Execute();
            }
            else
            {
                SetState(EnumAbilityState.End);
            }
        }//执行 
        else if (_state == EnumAbilityState.Execute)
        {
            if (_listEvents[_indexEffect].IsFinish)
            {
                SetIndex(_indexEffect + 1);
                SetState(EnumAbilityState.Start);
            }
            else
            {
                _listEvents[_indexEffect].UpdateEvent();
            }
        }

    }
    public bool IsFinish()
    {
        for (int cnt = 0; cnt < _listEvents.Count; cnt++)
        {
            if (!_listEvents[cnt].IsFinish)
            {
                return false;
            }
        }
        return true;
    }



    private void SetState(EnumAbilityState state)
    {
        _state = state;
    }
    private void SetIndex(int idx)
    {
        _indexEffect = idx;
    }
}

public enum EnumAEffectEvent
{
    /// <summary>
    /// 延迟执行
    /// </summary>
    Delay = 1,
    /// <summary>
    /// 动画
    /// </summary>
    Animator = 2,
    /// <summary>
    /// 攻击
    /// </summary>
    AttackEffect = 3,
    /// <summary>
    /// 朝向目标
    /// </summary>
    FaceToTarget = 4,

}