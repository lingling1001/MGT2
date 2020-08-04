using MFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AssemblyAbility : AssemblyBase, IUpdate
{
    public AssemblyRole RoleInfo { get; private set; }
    public PrototypeAbility Data { get; private set; }
    public int Priority => DefinePriority.NORMAL;
    /// <summary>
    /// 执行限制
    /// </summary>
    private List<APreformBase> _abilityLimits;
    /// <summary>
    /// 执行效果
    /// </summary>
    private List<AEffectGroup> _abilityEffects;
    /// <summary>
    /// 执行状态
    /// </summary>
    public EnumAbilityState State { get { return _state; } }
    private EnumAbilityState _state;
    /// <summary>
    /// 当前不满足限制条件列表
    /// </summary>
    private List<APreformBase> _tempLimit = new List<APreformBase>();
    /// <summary>
    /// 临时变量  执行效果是否完成
    /// </summary>
    private bool _tempValue;
    public override void OnInit(EnumAssemblyType assemblyType, AssemblyEntityBase owner)
    {
        base.OnInit(assemblyType, owner);
        RoleInfo = owner.GetData<AssemblyRole>(EnumAssemblyType.Role);
        RegisterInterfaceManager.RegisteUpdate(this);
    }
    public void SetData(PrototypeAbility abData)
    {
        Data = abData;
        InitialEffect(FactoryAbility.CreateAbilityEffectsNew(abData.GetEffect(), RoleInfo));
        InitialLimit(FactoryAbility.CreateAbilityLimits(abData.GetLimit(), RoleInfo));
    }
    /// <summary>
    /// 设置效果数据
    /// </summary>
    public void InitialEffect(List<AEffectGroup> abilityEffect)
    {
        _abilityEffects = abilityEffect;
    }
    /// <summary>
    /// 初始化限制
    /// </summary>
    public void InitialLimit(List<APreformBase> abilityLimit)
    {
        _abilityLimits = abilityLimit;
    }

    public T GetAbilityLimit<T>(EnumAPreform type) where T : APreformBase
    {
        APreformBase data = _abilityLimits.Find(item => item.Type == type);
        if (data != null)
        {
            return data as T;
        }
        return null;
    }

    /// <summary>
    /// 检测效果是否可以执行
    /// </summary>
    public List<APreformBase> CheckExecute(AssemblyRole target)
    {
        _tempLimit.Clear();
        if (_abilityLimits == null)
        {
            return null;
        }
        for (int cnt = 0; cnt < _abilityLimits.Count; cnt++)
        {
            _abilityLimits[cnt].RefreshTarget(target);
            if (!_abilityLimits[cnt].OnCheckPreform())
            {
                _tempLimit.Add(_abilityLimits[cnt]);
            }
        }
        return _tempLimit;
    }
    public void ExecuteLimit()
    {
        if (_abilityLimits == null)
        {
            return;
        }
        for (int cnt = 0; cnt < _abilityLimits.Count; cnt++)
        {
            _abilityLimits[cnt].OnExecute();
        }

    }
    /// <summary>
    /// 执行效果
    /// </summary>
    public void ExecuteEffect()
    {
        if (_abilityEffects == null || _abilityEffects.Count == 0)
        {
            return;
        }
        for (int cnt = 0; cnt < _abilityEffects.Count; cnt++)
        {
            _abilityEffects[cnt].Execute();
        }
        SetState(EnumAbilityState.Execute);
    }

    public void On_Update(float elapseSeconds, float realElapseSeconds)
    {
        if (_state != EnumAbilityState.Execute)
        {
            return;
        }
        _tempValue = true;
        for (int cnt = 0; cnt < _abilityEffects.Count; cnt++)
        {
            if (!_abilityEffects[cnt].IsFinish())
            {
                _abilityEffects[cnt].UpdateEvent();
                _tempValue = false;
            }
        }
        if (_tempValue)
        {
            SetState(EnumAbilityState.None);
        }
    }

    private void SetState(EnumAbilityState state)
    {
        _state = state;
    }

    public override void OnRelease()
    {
        RegisterInterfaceManager.UnRegisteUpdate(this);
        base.OnRelease();
    }


}

public enum EnumAbilityState
{
    None,
    Start,
    Execute,
    End,
}