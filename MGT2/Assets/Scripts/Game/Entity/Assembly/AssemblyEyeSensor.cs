using MFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AssemblyEyeSensor : AssemblyBase, IUpdate
{
    private List<AssemblyCache> _listTargets = new List<AssemblyCache>();
    private AssemblyCache _assemblySelf;
    private System.DateTime _tempTime;
    public int Priority => DefinePriority.NORMAL;

    protected override void OnInit(EntityAssembly owner)
    {
        _tempTime = System.DateTime.Now;
        _assemblySelf = owner.GetData<AssemblyCache>();
        base.OnInit(owner);
        RegisterInterfaceManager.RegisteUpdate(this);
    }
    public AssemblyCache GetTarget()
    {
        if (_listTargets.Count > 0)
        {
            return _listTargets[0];
        }
        return null;
    }
    /// <summary>
    /// 获取目标列表
    /// </summary>
    public List<AssemblyCache> GetTargets()
    {
        return _listTargets;
    }
    public bool Contains(AssemblyCache info)
    {
        return _listTargets.Contains(info);
    }
    public bool CheckTargetIsNull()
    {
        return GetTarget() == null;
    }
    public void On_Update(float elapseSeconds, float realElapseSeconds)
    {
        if (_tempTime > System.DateTime.Now)//每秒检测一次
        {
            return;
        }
        _tempTime = System.DateTime.Now.AddSeconds(1);
        _listTargets.Clear();
        if (_assemblySelf == null)
        {
            return;
        }
        if (_assemblySelf.AssyAttribute == null)
        {
            return;
        }
        long range = _assemblySelf.AssyAttribute.GetValue(DTAttribute.RangeGuard);
        List<AssemblyCache> listOthers = EntityHelper.GetNearOthersByCamp(_assemblySelf, range);
        if (listOthers.Count > 0)
        {
            _listTargets.AddRange(listOthers);
        }

    }
    protected override void OnRelease()
    {
        RegisterInterfaceManager.UnRegisteUpdate(this);
        base.OnRelease();
    }



}
