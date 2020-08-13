using MFrameWork;
using UnityEngine;
using DG.Tweening;
using System;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class AttackEffectArrow : AttackEffectBase
{
    private GameObject _effectItem;
    private GameObject _prefabItem;
    private AssemblyWeapon _assemblyWeapon;
    private AEAttackEffect _aEAttackEffect;
    private Vector3[] _pathPoint;
    private int _idx = 0;

    public override void Initial(AEffectEventBase data)
    {
        base.Initial(data);
        data.SetIsFinish(true);
        _aEAttackEffect = data as AEAttackEffect;
        if (_prefabItem != null)
        {
            TryInstantiatePrefab(_prefabItem);
            return;
        }
        ResLoadHelper.LoadAssetAsync<GameObject>(_aEAttackEffect.EffectName, EventLoadFinish);
    }
    private void EventLoadFinish(GameObject obj)
    {
        TryInstantiatePrefab(obj);
    }
    private bool TryInstantiatePrefab(GameObject prefabItem)
    {
        if (prefabItem == null)
        {
            return false;
        }
        _prefabItem = prefabItem;
        _effectItem = GameObject.Instantiate(_prefabItem);
        Vector3 start = GetWeaponNode().ObjWeapon.transform.position;
        Vector3 end;
        if (Data.Target != null)
        {
            end = Data.Target.Position;
        }
        else
        {
            end = start + Data.Owner.AssemblyView.Trans.forward * 20;
        }
        end.y = start.y;
        Vector3 control = UnityMath.GetBetweenPoint(start, end, 0.5f);
        control.y = end.y + 3;
        _pathPoint = UnityMath.GetBeizerList(start, control, end, 10);
        _effectItem.transform.position = start;
        _idx = 1;
        StartPlayTween();
        return true;
    }

    private void StartPlayTween()
    {
        if (_pathPoint.Length == 0 || _idx >= _pathPoint.Length)
        {
            PlayTweenFinish();
            return;
        }
        Vector3 endPos = _pathPoint[_idx];
        TweenerCore<Vector3, Vector3, VectorOptions> tween = _effectItem.transform.DOMove(endPos, 0.1f);
        tween.onComplete = StartPlayTween;
        _idx++;
    }
    /// <summary>
    /// 动画播放完成 移除攻击效果
    /// </summary>
    private void PlayTweenFinish()
    {
        AttackEffectManager.Instance.RemoveEffect(this);


    }

    private AssemblyWeapon GetWeaponNode()
    {
        if (_assemblyWeapon == null)
        {
            _assemblyWeapon = Data.Owner.Owner.GetData<AssemblyWeapon>(EnumAssemblyType.Weapon);
        }
        return _assemblyWeapon;
    }

    public override void Release()
    {
        GameObject.Destroy(_effectItem);
        base.Release();
    }
}