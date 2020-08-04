using com.ootii.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIHead : BaseUI
{
    //public Dictionary<int, UIHeadItem> _mapItems = new Dictionary<int, UIHeadItem>();
    //private List<UIHeadItem> _listTemp = new List<UIHeadItem>();
    //private GameObject _prefabItem;
    //public override void OnInit()
    //{
    //    base.OnInit();
    //    GetBindComponents(ObjUI);

    //    MessageDispatcher.AddListener(DefineNotification.ASSEMBLY_ADD, EventEntityAssemblyAdd);
    //    MessageDispatcher.AddListener(DefineNotification.ASSEMBLY_REM, EventEntityAssemblyRemove);

    //}

    //private void EventEntityAssemblyAdd(IMessage rMessage)
    //{
    //    AssemblyHeadUIItem assemblyHeadUIItem = rMessage.Data as AssemblyHeadUIItem;
    //    if (assemblyHeadUIItem == null)
    //    {
    //        return;
    //    }
    //    AssemblyEntityBase data = rMessage.Sender as AssemblyEntityBase;
    //    AssemblyRole role = data.GetData<AssemblyRole>(EnumAssemblyType.Role);
    //    UIHeadItem headItem = GetItem(role.EntityId);
    //    if (headItem == null)
    //    {
    //        return;
    //    }
    //    //assemblyHeadUIItem.SetValue(headItem);

    //}
    //private void EventEntityAssemblyRemove(IMessage rMessage)
    //{
    //    AssemblyHeadUIItem assemblyHeadUIItem = rMessage.Data as AssemblyHeadUIItem;
    //    if (assemblyHeadUIItem == null)
    //    {
    //        return;
    //    }
    //    AssemblyEntityBase data = rMessage.Sender as AssemblyEntityBase;
    //    AddItemToTemp(assemblyHeadUIItem.Value);
    //}
    //private void AddItemToTemp(UIHeadItem item)
    //{
    //    _mapItems.Remove(item.EntityId);
    //    _listTemp.Add(item);
    //    UnityObjectExtension.SetActive(item, false);
    //}
    //private UIHeadItem GetItem(int id)
    //{
    //    if (_mapItems.ContainsKey(id))
    //    {
    //        return null;
    //    }
    //    UIHeadItem item;

    //    if (_listTemp.Count > 0)
    //    {
    //        item = _listTemp[0];
    //        _listTemp.RemoveAt(0);
    //        UnityObjectExtension.SetActive(item, true);
    //    }
    //    else
    //    {
    //        item = CreateHeadItem();
    //    }
    //    return item;
    //}
    //private UIHeadItem CreateHeadItem()
    //{
    //    if (_prefabItem == null)
    //    {
    //        _prefabItem = ResLoadHelper.LoadAsset<GameObject>(UIHelper.GetUIPath(EnumUIType.UIHeadItem));
    //    }
    //    if (_prefabItem != null)
    //    {
    //        GameObject obj = NGUITools.AddChild(m_Trans_Parents.gameObject, _prefabItem);
    //        UIHeadItem item = obj.GetOrAddComponent<UIHeadItem>();
    //        return item;
    //    }
    //    return null;
    //}
    //public override void OnRelease()
    //{
    //    base.OnRelease();
    //    MessageDispatcher.RemoveListener(DefineNotification.ASSEMBLY_ADD, EventEntityAssemblyAdd);
    //    MessageDispatcher.RemoveListener(DefineNotification.ASSEMBLY_REM, EventEntityAssemblyRemove);
    //}
}
