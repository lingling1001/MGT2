using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIWorldOperate : BaseUI
{
    private AssemblyCache _curRole;

    private IWorldNodeable _worldResMenuProcess = null;
    public override void OnInit()
    {
        GetBindComponents(ObjUI);

        AssemblyCache cache = UIParams[0] as AssemblyCache;
        _curRole = cache;

        SetCurrentProcess(cache);

    }

    /// <summary>
    /// …Ë÷√¥¶¿Ì∆˜
    /// </summary>
    public void SetCurrentProcess(AssemblyCache cache)
    {
        EnumWorldResNode nodeType = GetResNodeType(cache);
        if (_worldResMenuProcess != null)
        {
            if (_worldResMenuProcess.ResNodeType != nodeType)
            {
                ReleaseProcess();
            }
        }
        if (_worldResMenuProcess == null)
        {
            _worldResMenuProcess = CreateNodeByNodeType(nodeType);
        }
        if (_worldResMenuProcess != null)
        {
            _worldResMenuProcess.OnInit(nodeType, cache);
        }

    }

    private IWorldNodeable CreateNodeByNodeType(EnumWorldResNode type)
    {
        switch (type)
        {
            case EnumWorldResNode.Role:
                return ItemPoolMgr.CreateOrGetItem<UIWorldOperateRole>(AssetsName.UIWorldOperateRole, ObjUI.transform);

        }
        return null;
    }

    private EnumWorldResNode GetResNodeType(AssemblyCache cache)
    {
        if (cache.AssyRoleInfo != null)
        {
            return EnumWorldResNode.Role;
        }
        return EnumWorldResNode.None;
    }

    private void ReleaseProcess()
    {
        if (_worldResMenuProcess != null)
        {
            _worldResMenuProcess.OnRelease();
            MonoPoolItem item = _worldResMenuProcess as MonoPoolItem;
            if (item != null)
            {
                ItemPoolMgr.AddPool(item);
            }
        }
        _worldResMenuProcess = null;
    }

    public override void OnRelease()
    {
        ReleaseProcess();
    }



}
