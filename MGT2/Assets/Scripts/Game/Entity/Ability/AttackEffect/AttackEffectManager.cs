using MFrameWork;
using System;
using System.Collections.Generic;

[MonoSingletonPath("Attack Effect Manager")]
public class AttackEffectManager : MonoSingleton<AttackEffectManager>
{
    private List<AttackEffectBase> _listAttackEffect = new List<AttackEffectBase>();
    
    public void AddEffect(AttackEffectBase effect)
    {
        if (_listAttackEffect.Contains(effect))
        {
            return;
        }
        _listAttackEffect.Add(effect);
    }
    public void RemoveEffect(AttackEffectBase data)
    {
        if (_listAttackEffect.Contains(data))
        {
            data.Release();
            _listAttackEffect.Remove(data);
        }
    }


}
