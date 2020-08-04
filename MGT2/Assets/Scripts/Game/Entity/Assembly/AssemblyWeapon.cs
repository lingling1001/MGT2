using MFrameWork;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyWeapon : AssemblyGetViewBase
{
    public int Value { get; private set; }
    public GameObject ObjWeapon { get { return _objWeapon; } }

    private PrototypeWeapon _dataWeapon;
    private PrototypeLoadWeapon _dataLoadInfo;
    private GameObject _objWeapon;

    /// <summary>
    /// 武器ID
    /// </summary>
    public void SetValue(int value)
    {
        PrototypeWeapon data = PrototypeManager<PrototypeWeapon>.Instance.GetPrototype(value);
        if (data == null)
        {
            return;
        }
        _dataLoadInfo = PrototypeManager<PrototypeLoadWeapon>.Instance.GetPrototype(data.Type);
        _dataWeapon = data;
        Value = value;
        RefreshView();
    }

    public override void ViewLoadFinish()
    {
        RefreshView();
    }
    public void RefreshView()
    {
        if (ViewObjIsNull() || _dataLoadInfo == null)
        {
            return;
        }
        if (string.IsNullOrEmpty(_dataWeapon.ModelPath))
        {
            _objWeapon = assemblyView.ObjEntity.transform.FindChildName<GameObject>(_dataLoadInfo.LoadParent);
            return;
        }
        ResLoadHelper.LoadAssetAsync<GameObject>(_dataWeapon.ModelPath, EventLoadFinish);

    }

    private void EventLoadFinish(GameObject prefab)
    {

        Transform parent = assemblyView.ObjEntity.transform.FindChildName(_dataLoadInfo.LoadParent);
        _objWeapon = NGUITools.AddChild(parent.gameObject, prefab);
        Vector3 angle = Vector3.zero;
        Utility.Xml.ParseString(_dataLoadInfo.Rotation, Utility.Xml.SplitComma, ref angle);
        _objWeapon.transform.localRotation = Quaternion.Euler(angle);

    }
}
