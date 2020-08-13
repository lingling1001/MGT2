using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyAnimator : AssemblyGetViewBase
{
    public int Type;
    public EnumAnimator Value;
    private Animator _animator;
    public string AniValue;
    public void SetType(int type)
    {
        Type = type;
    }
    public void SetValue(EnumAnimator type)
    {
        Value = type;
        AniValue = DefineAnimator.GetAnimatorName(Value, Type);
        RefreshView();
    }
    public void PlayAnimator(string strValue)
    {
        AniValue = strValue;
        RefreshView();
    }
    public void RefreshView()
    {
        if (_animator == null)
        {
            if (ViewObjIsNull())
            {
                return;
            }
            _animator = assemblyView.ObjEntity.GetComponent<Animator>();
            if (_animator == null)
            {
                _animator = assemblyView.ObjEntity.GetComponentInChildren<Animator>();
            }
        }
        if (_animator != null)
        {
            _animator.Play(AniValue);
        }
        Log.Info("  Pay Animator  " + AniValue);
    }

    public override void ViewLoadFinish()
    {
        RefreshView();
    }
    public override void OnRelease()
    {
        base.OnRelease();
        _animator = null;
    }


}
